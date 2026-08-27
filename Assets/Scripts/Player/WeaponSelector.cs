using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WeaponSelector : MonoBehaviour {
    [SerializeField] WeaponType m_weaponType;
    [SerializeField] List<WeaponData> m_weapons = new();
    [SerializeField] Transform m_parent;

    [Space]
    [Header("Runtime-filled Variables")]
    public WeaponData activeWeapon;

    void Start() {
        var weapon = m_weapons.Find(weapon => weapon.weaponType == m_weaponType);

        activeWeapon = weapon;
        activeWeapon.Spawn(m_parent, this);
    }
}