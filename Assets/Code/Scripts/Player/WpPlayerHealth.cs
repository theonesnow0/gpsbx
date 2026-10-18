using Fusion;
using UnityEngine;

namespace Wp.Player {
    [type: DisallowMultipleComponent]
    public class WpPlayerHealth : NetworkBehaviour, IDamageable {
        [property: Networked, OnChangedRender(nameof(m_OnHealthChanged))]
        public float health { get; set; } = 100f;
        public float maxHealth => 100f;

        void m_OnHealthChanged() {
            Debug.Log($"Took damage. New health={health}f");
        }

        [method: Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_TakeDamage(float damageAmount) {
            health -= Mathf.Lerp(damageAmount, 0, health);
        }

        /* RPC_TakeDamage wrapper */
        public void TakeDamage(float damageAmount) {
            RPC_TakeDamage(damageAmount);
        }
    }
}
