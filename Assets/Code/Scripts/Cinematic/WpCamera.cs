using UnityEngine;

namespace Wp.Cinematic {
    /// <summary>
    /// Main camera component. You can add more features in editor
    /// </summary>
    public class WpCamera : MonoBehaviour {
        /// <summary>
        /// What target <c>Transform</c> to track?
        /// </summary>
        public Transform trackingTarget { get; set; }

        /// <summary>
        /// What perspective mode to use?
        /// </summary>
        public WpCameraPerspective perspectiveMode { get; set; } = WpCameraPerspective.ThirdPerson;

        private Vector2 m_EulerRotationInner, m_MotionVelocity;

        private readonly float m_CameraSensitivity = .17F, m_ArmOffset = 2.75F;
        private float m_DistanceToCharacter = 4F;

        private void Awake() => Cursor.lockState = CursorLockMode.Locked;

        private void LateUpdate() {
            if (trackingTarget == null)
                return;

            m_EulerRotationInner += new Vector2(-m_MotionVelocity.y, m_MotionVelocity.x) * m_CameraSensitivity;
            m_EulerRotationInner.x = Mathf.Clamp(m_EulerRotationInner.x, -75F, 75F); // limit x axis to a specific angle
            
            // set camera position depending on perspective mode
            transform.position = perspectiveMode switch {
                WpCameraPerspective.FirstPerson => trackingTarget.position,
                WpCameraPerspective.ThirdPerson => m_CalculateCameraPosition(),
                _ => Vector3.zero
            };

            // set camera rotation
            transform.localEulerAngles = m_EulerRotationInner;
        }

        /// <summary>
        /// Rotate camera by <c>motion</c>
        /// </summary>
        /// <param name="motion">Motion 2D vector</param>
        public void Look(Vector2 motion) => m_MotionVelocity = motion;

        /// <summary>
        /// Change camera distance to character by <c>delta</c>
        /// </summary>
        /// <param name="delta">The amount</param>
        public void Zoom(float delta) => m_DistanceToCharacter += delta;

        private Vector3 m_CalculateCameraPosition() => trackingTarget.position + (Quaternion.Euler(m_EulerRotationInner) * (Vector3.back * m_DistanceToCharacter + Vector3.right * m_ArmOffset));
    }
}