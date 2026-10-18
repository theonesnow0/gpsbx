using Fusion;
using UnityEngine;
using UnityEngine.UIElements;

namespace Wp.Items {
    [CreateAssetMenu]
    public class WpItemInfo : ScriptableObject {
        public string itemName;
        [TextArea] public string itemDescription;
        
        public float itemCooldown = 1f;
    }
}
