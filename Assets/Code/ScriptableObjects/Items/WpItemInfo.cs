using Fusion;
using UnityEngine;
using UnityEngine.UIElements;

namespace Wp.Items {
    [type: CreateAssetMenu]
    public class WpItemInfo : ScriptableObject {
        public string itemName;
        [field: TextArea] public string itemDescription;
        
        public float itemCooldown = 1f;
    }
}
