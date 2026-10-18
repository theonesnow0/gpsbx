using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

namespace Wp.Items {
    public class WpGrenade : NetworkBehaviour, IInteractable {
        [field: SerializeField] GameObject m_grenadePrefab;
        
        public void Use(IInteractable.WpItemContext context) {
            _ = Runner.Spawn(m_grenadePrefab, context.transform.position, Quaternion.identity);
        }
    }
}