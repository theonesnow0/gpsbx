using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAction : MonoBehaviour {
    [SerializeField] WeaponSelector m_weaponSelector;

    public void UseWeapon() => m_weaponSelector.activeWeapon.Fire();
}