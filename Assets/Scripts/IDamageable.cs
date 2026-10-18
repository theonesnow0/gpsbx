using UnityEngine;

public interface IDamageable {
    public float health { get; set; }
    public float maxHealth => 100f;

    public void TakeDamage(float damageAmount);
}