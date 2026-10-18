using Fusion;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Wp.Items {
    [type: DisallowMultipleComponent]
    public class WpItem : NetworkBehaviour {
        public WpItemInfo itemInfo;

        IInteractable m_interactable;

        void Awake() {
            m_interactable = GetComponent<IInteractable>();
        }

        public void Use(IInteractable.WpItemContext context) {
            m_interactable.Use(context);
        }
    }
}