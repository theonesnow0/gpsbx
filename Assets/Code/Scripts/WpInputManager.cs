using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wp.Network {
    [type: DisallowMultipleComponent]
    public class WpInputManager : MonoBehaviour {
        [type: Serializable]
        public struct WpPlayerInput {
            public Vector2 requestedMove, requestedLook;
            public bool requestedJump, requestedSprint;
        }

        public WpPlayerInput playerInput;

        #region Singleton
        public static WpInputManager instance;

        void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this);

            DontDestroyOnLoad(this);
        }
        #endregion

        public void OnMove(InputAction.CallbackContext context) {
            playerInput.requestedMove = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context) {
            playerInput.requestedLook = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context) {
            playerInput.requestedJump = context.performed;
        }

        public void OnSprint(InputAction.CallbackContext context) {
            playerInput.requestedSprint = context.performed;
        }
    }
}