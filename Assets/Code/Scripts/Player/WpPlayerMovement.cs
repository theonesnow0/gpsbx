using Fusion;
using UnityEngine;
using Wp.Cinematic;

namespace Wp.Player {
    [type: RequireComponent(typeof(CharacterController)), DisallowMultipleComponent]
    public class WpPlayerMovement : NetworkBehaviour {
        [field: SerializeField] private CharacterController m_CharacterController;
        [field: SerializeField] private WpPlayer m_Player;
        [field: SerializeField] private Transform m_Head;

        public float playerSpeed => 16F;
        private readonly float m_AccelerationRate = 70F, m_DecelerationRate = 90F;

        public float playerJumpHeight => 30F;

        /// <summary>
        /// <c>WpCamera</c> the character is using right now
        /// </summary>
        public WpCamera currentCamera { get; private set; }

        private WpPlayerState m_PlayerState;
        private Vector3 m_BodyVelocity, m_MotionVelocity;

        public override void Spawned() {
            if (!HasStateAuthority)
                return;

            m_CharacterController.enabled = true; // physx conflicts with network transform and doesn't let it overwrite Transform

            currentCamera = Camera.main.GetComponent<WpCamera>();
            currentCamera.trackingTarget = m_Head;
        }

        public override void FixedUpdateNetwork() {
            // apply gravity to player
            if (!m_CharacterController.isGrounded)
                m_BodyVelocity += Physics.gravity * Runner.DeltaTime; // m/c^2
            else
                m_BodyVelocity.y = 0;

            // handle jump
            if (m_CharacterController.isGrounded && m_PlayerState.isJumping)
                m_ApplyJumpImpulse();

            // rotate character towards camera direction
            transform.localEulerAngles = new(0, currentCamera.transform.eulerAngles.y); // c# doesn't allow direct change of property structures ;(

            // add acceleration/deceleration to character movement
            m_MotionVelocity = Vector3.MoveTowards(
                current:          m_MotionVelocity,
                target:           m_PlayerState.moveDirection * playerSpeed,
                maxDistanceDelta: (m_PlayerState.moveDirection != Vector3.zero ? m_AccelerationRate : m_DecelerationRate) * Runner.DeltaTime
            );

            _ = m_CharacterController.Move((m_BodyVelocity + Quaternion.AngleAxis(currentCamera.transform.eulerAngles.y, Vector3.up) * m_MotionVelocity) * Runner.DeltaTime);
        }

        /// <summary>
        /// Moves character by a physics tick
        /// </summary>
        /// <param name="motion">Motion 2D vector</param>
        public void Move(Vector2 motion) => m_PlayerState.moveDirection = new(motion.x, 0, motion.y);

        /// <summary>
        /// Forces character to crouch
        /// </summary>
        /// <param name="actionPerformed">Was the Crouch action performed by user?</param>
        public void Crouch(bool actionPerformed) => m_PlayerState.isCrouching = actionPerformed;

        /// <summary>
        /// Forces character to jump
        /// </summary>
        /// <param name="actionPerformed">Was the Jump action performed by user?</param>
        public void Jump(bool actionPerformed) => m_PlayerState.isJumping = actionPerformed;

        /// <summary>
        /// Forces character to sprint
        /// </summary>
        /// <param name="actionPerformed"></param>
        public void Sprint(bool actionPerformed) => m_PlayerState.isSprinting = actionPerformed;

        private void m_ApplyJumpImpulse() {
            var results = new RaycastHit[1];
            var raycastHits = Physics.RaycastNonAlloc(
                ray:         new(m_Head.position, Vector3.up),
                results:     results,
                maxDistance: playerJumpHeight,
                layerMask:   Physics.DefaultRaycastLayers,

                queryTriggerInteraction: QueryTriggerInteraction.Ignore
            );

            m_BodyVelocity.y = raycastHits > 0 ? Vector3.Distance(m_Head.position, results[0].point) : playerJumpHeight;
        }
    }
}