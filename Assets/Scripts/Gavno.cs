using UnityEngine;

public class Gavno : MonoBehaviour, IDamageable
{
    private float m_health = 100f;

    public void TakeDamage(float amount) => Debug.Log($"{m_health -= amount}");
}
