using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Shoot Config", menuName = "Weapons/Weapon Shoot Config", order = 2)]
public class WeaponShootConfig : ScriptableObject {
    public LayerMask hitMask;
    public Vector3 spread = new(1f, 1f, 1f);
    public float fireRate = .25f;
    public float maxDistance = 100f;
}