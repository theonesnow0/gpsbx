#nullable disable

using UnityEngine;

/// <summary>
/// Interface for entities with health
/// </summary>
public interface IDamageable {
    void TakeDamage(float amount);
}
