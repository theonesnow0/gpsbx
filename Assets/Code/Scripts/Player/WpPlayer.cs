using Fusion;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wp.Player {
    public class WpPlayer : NetworkBehaviour, PlayerInputActions.ICharacterActions {
        [field: SerializeField] private WpPlayerMovement m_PlayerMovement;
        [field: SerializeField] private WpPlayerInventory m_PlayerInventory;

        private PlayerInputActions m_PlayerActions;
        private PlayerInputActions.CharacterActions m_CharacterActions;

        private void Awake() {
            m_PlayerActions = new();

            m_CharacterActions = m_PlayerActions.Character;
            m_CharacterActions.AddCallbacks(this);
        }

        private void OnDestroy() => m_PlayerActions.Dispose();

        private void OnEnable() => m_CharacterActions.Enable();

        private void OnDisable() => m_CharacterActions.Disable();

        public void OnMove(InputAction.CallbackContext context) => m_PlayerMovement.Move(context.ReadValue<Vector2>());

        public void OnLook(InputAction.CallbackContext context) => m_PlayerMovement.currentCamera.Look(context.ReadValue<Vector2>());

        public void OnZoom(InputAction.CallbackContext context) => m_PlayerMovement.currentCamera.Zoom(context.ReadValue<float>());

        public void OnFire(InputAction.CallbackContext _) => throw new NotImplementedException();

        public void OnInteract(InputAction.CallbackContext _) => throw new NotImplementedException();

        public void OnCrouch(InputAction.CallbackContext context) => m_PlayerMovement.Crouch(context.performed);

        public void OnJump(InputAction.CallbackContext context) => m_PlayerMovement.Jump(context.performed);

        public void OnSprint(InputAction.CallbackContext context) => m_PlayerMovement.Sprint(context.performed);
    }
}