using System;
using UnityEngine;
using Wp.Network;

namespace Wp.Cinematic {
    [type: DisallowMultipleComponent]
    public class WpCamera : MonoBehaviour {
        public enum CameraMode {
            FirstPerson,
            ThirdPerson
        }

        [field: Header(@"Camera Settings")] public CameraMode _cameraMode = CameraMode.FirstPerson;
        [field: SerializeField, Range(.001f, 1f)] float m_lookSensitivity = .03f;
        [field: SerializeField, Range(1f, 100f)] float m_lookDampingAmount = 12f;

        public Transform trackingTarget;
        Vector2 m_eulerRotation;

        void LateUpdate() {
            if (trackingTarget == null)
                return;

            transform.position = trackingTarget.position;

            m_UpdateRotation();
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(m_eulerRotation), m_lookDampingAmount * Time.deltaTime);
        }

        void m_UpdateRotation() {
            m_eulerRotation += new Vector2(-WpInputManager.instance.playerInput.requestedLook.y, WpInputManager.instance.playerInput.requestedLook.x) * m_lookSensitivity;
            m_eulerRotation.x = Mathf.Clamp(m_eulerRotation.x, -75f, 75f);
        }
    }
}