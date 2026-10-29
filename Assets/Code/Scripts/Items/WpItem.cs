using Fusion;
using UnityEngine;

namespace Wp.Items {
    public class WpItem : NetworkBehaviour {
        public WpItemInfo itemInfo;
        private IUsable m_Usable;

        private void Awake() => m_Usable = GetComponent<IUsable>();

        public void Use(WpItemUseContext context) => m_Usable.Use(context);
    }
}