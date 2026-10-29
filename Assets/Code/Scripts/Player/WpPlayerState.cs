using System;
using UnityEngine;

namespace Wp.Player {
    /// <summary>
    /// Structure for observing player states
    /// </summary>
    [type: Serializable]
    public struct WpPlayerState {
        /// <summary>
        /// In which direction player character is moving?
        /// </summary>
        public Vector3 moveDirection { get; set; }

        /// <summary>
        /// Does the player character wants to jump?
        /// </summary>
        public bool isJumping { get; set; }

        /// <summary>
        /// Is the player character crouching?
        /// </summary>
        public bool isCrouching { get; set; }

        /// <summary>
        /// Is the player character sprinting?
        /// </summary>
        public bool isSprinting { get; set; }
    }
}