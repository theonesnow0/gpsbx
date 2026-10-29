using Fusion;
using UnityEngine;

namespace Wp.Player {
    public class WpPlayerHealth : NetworkBehaviour {
        [property: Networked, OnChangedRender(nameof(m_OnHealthChanged))]
        public float health { get; set; } = 100F;
        public float maxHealth => 100F;

        void m_OnHealthChanged() {
            Debug.Log($"Took damage. New health={health}f");
        }

        [method: Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_TakeDamage(float damageAmount) {
            health -= Mathf.Lerp(damageAmount, 0, health);
        }

        public void TakeDamage(float damageAmount) {
            RPC_TakeDamage(damageAmount);
        }
    }
}
