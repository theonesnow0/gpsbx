using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Wp.Cinematic;

namespace Wp.Player {
    public class WpPlayer : MonoBehaviour, PlayerInputActions.ICharacterActions {
        [field: SerializeField] private WpPlayerMovement m_PlayerMovement;

        private PlayerInputActions m_Actions;
        private PlayerInputActions.CharacterActions m_Character;

        /// <summary>
        /// Current player state (e.g. jumping, crouching, going in some direction etc.)
        /// </summary>
        public WpPlayerState playerState => m_PlayerStateInner;
        private WpPlayerState m_PlayerStateInner = new();

        private void Awake() {
            m_Actions = new();

            m_Character = m_Actions.Character;
            m_Character.AddCallbacks(this);
        }

        private void OnDestroy() => m_Actions.Dispose();

        private void OnEnable() => m_Character.Enable();

        private void OnDisable() => m_Character.Disable();

        void PlayerInputActions.ICharacterActions.OnMove(InputAction.CallbackContext context) => m_PlayerStateInner.moveDirection = context.ReadValue<Vector2>();

        void PlayerInputActions.ICharacterActions.OnJump(InputAction.CallbackContext context) => m_PlayerStateInner.isJumping = context.performed;

        void PlayerInputActions.ICharacterActions.OnSprint(InputAction.CallbackContext context) => m_PlayerStateInner.isSprinting = context.performed;

        void PlayerInputActions.ICharacterActions.OnCrouch(InputAction.CallbackContext context) => throw new NotImplementedException();

        void PlayerInputActions.ICharacterActions.OnFire(InputAction.CallbackContext context) => throw new NotImplementedException();

        void PlayerInputActions.ICharacterActions.OnInteract(InputAction.CallbackContext context) => throw new NotImplementedException();

        void PlayerInputActions.ICharacterActions.OnLook(InputAction.CallbackContext context) => m_PlayerMovement.currentCamera.Look(context.ReadValue<Vector2>());

        void PlayerInputActions.ICharacterActions.OnZoom(InputAction.CallbackContext context) => m_PlayerMovement.currentCamera.Zoom(context.ReadValue<float>());

        void PlayerInputActions.ICharacterActions.OnPerspective(InputAction.CallbackContext context) {
            var value = m_PlayerMovement.currentCamera.perspectiveMode;
            m_PlayerMovement.currentCamera.perspectiveMode = (WpCameraPerspective)(((int)value + 1) % 2); // tf
        }
    }
}