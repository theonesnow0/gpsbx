using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Wp.Items;

namespace Wp.Player {
    [type: DisallowMultipleComponent]
    public class WpPlayerInventory : NetworkBehaviour {
        [field: SerializeField] WpItem[] m_items;
        public WpItem activeItem { get; private set; } // runtime-filled
    }
}