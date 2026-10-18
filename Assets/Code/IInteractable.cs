using UnityEngine;
using UnityEngine.InputSystem;

namespace Wp.Items {
    public interface IInteractable {
        /// <summary>
        /// Describes how the item was used
        /// </summary>
        public struct WpItemContext {
            public Transform transform;
            public InputActionPhase requestedFire;
        }

        public void Use(WpItemContext context);
    }
}
