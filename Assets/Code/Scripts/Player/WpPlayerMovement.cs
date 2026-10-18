using Fusion;
using UnityEngine;
using Wp.Cinematic;
using Wp.Network;

namespace Wp.Player {
    [type: RequireComponent(typeof(CharacterController)), DisallowMultipleComponent]
    public class WpPlayerMovement : NetworkBehaviour {
        [field: SerializeField, Header(@"Dependencies")] CharacterController m_characterController;

        [field: Header(@"Movement Settings")] public float playerSpeed = 10f;
        const float SPRINT_SPEED_MULTIPLIER = 1.2f;

        public float playerJumpHeight = 25f;
        
        public Transform headPivot;

        WpCamera currentCamera;
        Vector3 m_bodyVelocity;

        public override void Spawned() {
            if (!HasStateAuthority)
                return;

            currentCamera = Camera.main.GetComponent<WpCamera>();
            currentCamera.trackingTarget = headPivot;
        }

        public override void FixedUpdateNetwork() {
            if (!m_characterController.isGrounded) // add gravity if player is not standing on surface
                m_bodyVelocity += Physics.gravity * Runner.DeltaTime;
            else
                m_bodyVelocity.y = WpInputManager.instance.playerInput.requestedJump ? playerJumpHeight : -1f; // or make him stick to the ground by doing this trick

            var moveSpeed = playerSpeed * (WpInputManager.instance.playerInput.requestedSprint ? SPRINT_SPEED_MULTIPLIER : 1f);

            var inputDirection = new Vector3(WpInputManager.instance.playerInput.requestedMove.x, 0, WpInputManager.instance.playerInput.requestedMove.y);
            var direction = Quaternion.AngleAxis(currentCamera.transform.eulerAngles.y, Vector3.up) * inputDirection * moveSpeed;

            m_bodyVelocity.x = direction.x; m_bodyVelocity.z = direction.z;

            m_characterController.Move(m_bodyVelocity * Runner.DeltaTime);
        }
    }
}