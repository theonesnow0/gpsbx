using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerAction))]
public class WpAvatar : MonoBehaviour, PlayerInputActions.ICharacterActions {
    [SerializeField] float m_moveSpeed = 12.5f;
    [SerializeField] float m_jumpHeight = 10f;

    [SerializeField] float m_characterMinHealth = 0f;
    [SerializeField] float m_characterMaxHealth = 100f;

    PlayerInputActions m_inputActions;
    PlayerInputActions.CharacterActions m_characterActions;

    CharacterController m_characterController;
    Vector3 m_verticalVelocity = Vector3.zero;
    Vector2 m_requestedMove, m_requestedLook;
    bool m_requestedJump;

    PlayerAction m_playerAction;

    void Awake() {
        m_inputActions = new PlayerInputActions();
        m_characterActions = m_inputActions.Character;
        m_characterActions.AddCallbacks(this);

        m_characterController = GetComponent<CharacterController>();
        m_playerAction = GetComponent<PlayerAction>();
    }

    void OnDestroy() => m_inputActions.Dispose();

    void OnEnable() => m_characterActions.Enable();

    void OnDisable() => m_characterActions.Disable();

    void Update() => transform.Rotate(Vector3.up, m_requestedLook.x * 90f * Time.deltaTime);

    void FixedUpdate() {
        var moveDirection = transform.TransformDirection(
            Vector3.ClampMagnitude(
                new(m_requestedMove.x, 0f, m_requestedMove.y),
                1f
            )
        ) * m_moveSpeed;
        
        if (m_characterController.isGrounded)
            m_verticalVelocity = m_requestedJump
                ? Vector3.up * m_jumpHeight
                : Vector3.zero;
        else
            m_verticalVelocity += Physics.gravity * Time.fixedDeltaTime;

        var finalMove = moveDirection + m_verticalVelocity;
        m_characterController.Move(finalMove * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx) => m_requestedMove = ctx.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext ctx) => m_requestedLook = ctx.ReadValue<Vector2>();

    public void OnJump(InputAction.CallbackContext ctx) => m_requestedJump = ctx.performed;

    public void OnFire(InputAction.CallbackContext ctx) {
        if (ctx.started)
            m_playerAction.UseWeapon();
    }
}
