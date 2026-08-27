using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public enum WeaponType {
    None,
    Revolver,
    LazerGun
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Scriptable Object", order = 1)]
public class WeaponData : ScriptableObject {
#region Weapon Main Data
    public WeaponType weaponType;
    public string weaponName;
    public GameObject modelPrefab;
#endregion

#region Gun Visuals
    public WeaponShootConfig weaponShootConfig;
    public WeaponTrailConfig weaponTrailConfig;
#endregion

#region Weapon Logic
    MonoBehaviour m_activeMonoBehaviour;
    GameObject m_model;
    float m_lastShootTime = 0f;
    ParticleSystem m_particleSystem;
    ObjectPool<TrailRenderer> m_trailPool;

    public void Spawn(Transform parent, MonoBehaviour monoBehaviour) {
        m_activeMonoBehaviour = monoBehaviour;
        m_trailPool = new(m_CreateTrail);

        m_model = Instantiate(modelPrefab, parent);
        // m_model.transform.SetLocalPositionAndRotation(spawnPosition, Quaternion.Euler(spawnRotation));
        m_particleSystem = m_model.GetComponentInChildren<ParticleSystem>();
    }

    public void Fire() {
        if (weaponType is WeaponType.None) return;
        
        m_particleSystem.Play();

        var shootOrigin = m_particleSystem.transform.position;
        var shootDirection = m_particleSystem.transform.forward.normalized;

        var raycastResult = Physics.Raycast(shootOrigin, shootDirection, out RaycastHit hitInfo, weaponShootConfig.maxDistance, weaponShootConfig.hitMask);
        var endPoint = raycastResult
            ? hitInfo.point
            : shootOrigin + (shootDirection * weaponTrailConfig.trailMissDistance);

        m_activeMonoBehaviour.StartCoroutine(PlayTrail(shootOrigin, endPoint, hitInfo));
    }

    public IEnumerator PlayTrail(Vector3 start, Vector3 end, RaycastHit hitInfo) {
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
            remainingDistance -= weaponTrailConfig.trailSimulationSpeed * Time.deltaTime;

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
#endregion
}