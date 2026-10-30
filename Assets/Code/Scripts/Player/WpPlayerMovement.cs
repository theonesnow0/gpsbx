using Fusion;
using UnityEngine;
using Wp.Cinematic;

namespace Wp.Player {
    [type: RequireComponent(typeof(CharacterController)), DisallowMultipleComponent]
    public class WpPlayerMovement : NetworkBehaviour {
        [field: SerializeField] private CharacterController m_CharacterController;
        [field: SerializeField] private WpPlayer m_PlayerController;
        [field: SerializeField] private Transform m_Head, m_Torso;

        public float playerSpeed => 16F;
        public float playerJumpHeight => 30F;

        private readonly float SprintRate = 1.3F;
        private readonly float AccelerationRate = 100F;
        private readonly float DecelerationRate = 130F;

        /// <summary>
        /// <c>WpCamera</c> the character is using right now
        /// </summary>
        public WpCamera currentCamera { get; private set; }

        private Vector3 m_BodyVelocity, m_MotionVelocity;

        public override void Spawned() {
            if (!HasStateAuthority)
                return;

            m_CharacterController.enabled = true; // physics engine conflicts with network runner and doesn't let it override Transform

            currentCamera = Camera.main.GetComponent<WpCamera>();
            currentCamera.trackingTarget = m_Head;
        }

        public override void FixedUpdateNetwork() {
            // apply gravity to player or let him stick to the ground
            if (!m_CharacterController.isGrounded)
                m_BodyVelocity += Physics.gravity * Runner.DeltaTime;
            else
                m_BodyVelocity.y = 0;

            // handle player input
            if (m_CharacterController.isGrounded && m_PlayerController.playerState.isJumping)
                m_ApplyJumpImpulse();

            var speed = playerSpeed * (m_PlayerController.playerState.isSprinting ? SprintRate : 1F);
            var acceleration = m_PlayerController.playerState.moveDirection != Vector2.zero ? AccelerationRate : DecelerationRate;

            var direction = new Vector3(m_PlayerController.playerState.moveDirection.x, 0, m_PlayerController.playerState.moveDirection.y);

            // apply accel/decel
            m_MotionVelocity = Vector3.MoveTowards(
                current:          m_MotionVelocity,
                target:           direction * speed,
                maxDistanceDelta: acceleration * Runner.DeltaTime
            );

            // rotate character body
            m_Torso.localEulerAngles = new(0, currentCamera.transform.localEulerAngles.y);

            _ = m_CharacterController.Move((m_BodyVelocity + (Quaternion.AngleAxis(currentCamera.transform.localEulerAngles.y, Vector3.up) * m_MotionVelocity)) * Runner.DeltaTime);
        }

        private void m_ApplyJumpImpulse() {
            var results = new RaycastHit[1];
            var raycastHits = Physics.RaycastNonAlloc(         // we don't want to allocate any garbage during the raycast
                ray:         new(m_Head.position, Vector3.up), // the performance is more important
                results:     results,
                maxDistance: playerJumpHeight,
                layerMask:   Physics.DefaultRaycastLayers,

                queryTriggerInteraction: QueryTriggerInteraction.Ignore
            );

            m_BodyVelocity.y = raycastHits > 0 ? Vector3.Distance(m_Head.position, results[0].point) : playerJumpHeight;
        }
    }
}