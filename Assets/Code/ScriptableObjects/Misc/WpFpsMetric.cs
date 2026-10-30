using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Wp.Misc {
    [type: CreateAssetMenu]
    public class WpFpsMetric : ScriptableObject {
        [field: Header("FPS Metric Binding"), CreateProperty]
        public float framesPerSecond = 0;
    }
}