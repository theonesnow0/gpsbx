using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Trail Config", menuName = "Weapons/Weapon Trail Config", order = 3)]
public class WeaponTrailConfig : ScriptableObject {
    public Material material;
    public AnimationCurve animationCurve;
    public float duration = .5f;
    public float minVertexDistance = .1f;
    public float trailSimulationSpeed = 100f;
    public Gradient color;
}