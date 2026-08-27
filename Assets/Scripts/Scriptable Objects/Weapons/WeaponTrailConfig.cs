using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Trail Config", menuName = "Weapons/Weapon Trail Config", order = 3)]
public class WeaponTrailConfig : ScriptableObject {
    [Header("Trail Config")]
    public Material material;
    public AnimationCurve animationCurve;
    public float duration = .5f;
    public float minVertexDistance = .1f;
    public Gradient color;

    [Space]
    [Header("Trail Simulation")]
    public float trailMissDistance = 100f;
    public float trailSimulationSpeed = 100f;
}