using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

namespace Wp.Items {
    public class WpGrenade : NetworkBehaviour, IUsable {
        [field: SerializeField] GameObject m_grenadePrefab;
        
        public void Use(WpItemUseContext context) {
            // _ = Runner.Spawn(m_grenadePrefab, context.transform.position, Quaternion.identity);
        }
    }
}