using UnityEngine;

namespace Wp.Misc {
    public class WpFps : MonoBehaviour {
        [field: SerializeField] private WpFpsMetric m_FpsMetric;

        private void LateUpdate() {
            m_FpsMetric.framesPerSecond = 1F / Time.unscaledDeltaTime;
        }
    }
}
