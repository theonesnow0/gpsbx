using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "Weapon Damage Config", menuName = "Weapons/Weapon Damage Config", order = 4)]
public class WeaponDamageConfig : ScriptableObject {
    public MinMaxCurve damageCurve;

    public int EvaluateDamage(float distance = 0f) => Mathf.CeilToInt(damageCurve.Evaluate(distance, Random.value));
}