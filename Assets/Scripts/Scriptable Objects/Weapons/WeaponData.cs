using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public enum WeaponType {
    None,
    Revolver,
    LazerGun
}

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapons/Weapon Scriptable Object", order = 1)]
public class WeaponData : ScriptableObject {
    public WeaponType weaponType;
    public string weaponName;
    public GameObject modelPrefab;

    public WeaponDamageConfig weaponDamageConfig;
    public WeaponShootConfig weaponShootConfig;
    public WeaponTrailConfig weaponTrailConfig;
    const float TRAIL_SIM_SPEED = 400f;

    MonoBehaviour m_activeMonoBehaviour;
    GameObject m_model;
    float m_lastFireTime = 0f;
    ParticleSystem m_particleSystem;
    ObjectPool<TrailRenderer> m_trailPool;

    public void Spawn(Transform parent, MonoBehaviour monoBehaviour) {
        m_activeMonoBehaviour = monoBehaviour;
        m_trailPool = new(m_CreateTrail);

        m_model = Instantiate(modelPrefab, parent);
        m_particleSystem = m_model.GetComponentInChildren<ParticleSystem>();
    }

    public void Fire(Ray ray) {
        /* return unless weapon is not none */
        if (weaponType is WeaponType.None) return;

        // if (Time.time > weaponShootConfig.fireRate + m_lastFireTime) {
            // m_lastFireTime = Time.time;

            var hitPoint = Physics.Raycast(ray, out var hitInfo, weaponShootConfig.maxDistance, weaponShootConfig.hitMask)
                ? hitInfo.point
                : ray.origin + (ray.direction * weaponShootConfig.maxDistance);
            var collider = hitInfo.collider;

            if (collider != null)
                if (collider.TryGetComponent<IDamageable>(out var damageable))
                    damageable.TakeDamage(weaponDamageConfig.EvaluateDamage(Vector3.Distance(ray.origin, hitInfo.point)));

            m_particleSystem.Play();
            m_activeMonoBehaviour.StartCoroutine(PlayTrail(m_particleSystem.transform.position, hitPoint));
        // }
    }

    public IEnumerator PlayTrail(Vector3 start, Vector3 end) {
        var instance = m_trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = start;
        yield return null;

        instance.emitting = true;

        var distance = Vector3.Distance(start, end);
        var remainingDistance = distance;
        while (remainingDistance > 0) {
            instance.transform.position = Vector3.Lerp(
                start,
                end,
                Mathf.Clamp01(1f - (remainingDistance / distance))
            );
            remainingDistance -= TRAIL_SIM_SPEED * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = end;

        yield return new WaitForSeconds(weaponTrailConfig.duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        m_trailPool.Release(instance);
    }

    TrailRenderer m_CreateTrail() {
        var instance = new GameObject("BulletTrail");
        var trailRenderer = instance.AddComponent<TrailRenderer>();
        trailRenderer.colorGradient = weaponTrailConfig.color;
        trailRenderer.material = weaponTrailConfig.material;
        trailRenderer.widthCurve = weaponTrailConfig.animationCurve;
        trailRenderer.time = weaponTrailConfig.duration;
        trailRenderer.minVertexDistance = weaponTrailConfig.minVertexDistance;

        trailRenderer.emitting = false;
        trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trailRenderer;
    }
}