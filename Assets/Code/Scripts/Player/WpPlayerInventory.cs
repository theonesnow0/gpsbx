using Fusion;
using UnityEngine;
using Wp.Items;

namespace Wp.Player {
    public class WpPlayerInventory : NetworkBehaviour {
        /// <summary>
        /// Item that the player is currently holding
        /// </summary>
        public WpItem activeItem { get; private set; }
    }
}