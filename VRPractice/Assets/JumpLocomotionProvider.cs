using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

[RequireComponent(typeof(CharacterController))]
public class JumpLocomotionProvider : LocomotionProvider
{
    public InputActionReference jump;           // Jump 액션(Press Only 권장)
    public float jumpHeight = 1.2f;             // 원하는 점프 높이(m)
    public float gravity = -9.81f;              // 중력
    public float groundedStickForce = 0.5f;     // 땅에 살짝 붙여두는 값
    public float coyoteTime = 0.1f;             // 떨어진 직후 여유 시간
    public float jumpBufferTime = 0.1f;         // 누른 걸 버퍼링

    CharacterController _cc;
    float _yVel;
    float _sinceUngrounded;
    float _jumpRequestedAt = -999f;

    protected override void Awake()
    {
        base.Awake();
        _cc = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        if (jump != null)
        {
            jump.action.performed += OnJumpPerformed;
            jump.action.Enable();
        }
    }
    void OnDisable()
    {
        if (jump != null)
            jump.action.performed -= OnJumpPerformed;
    }

    void OnJumpPerformed(InputAction.CallbackContext _)
    {
        _jumpRequestedAt = Time.time; // 눌렀다는 사실만 기록(버퍼링)
    }

    void Update()
    {
        if (_cc == null) return;

        bool grounded = _cc.isGrounded;
        if (grounded)
        {
            _sinceUngrounded = 0f;
            if (_yVel < 0f) _yVel = -groundedStickForce; // 착지 안정화
        }
        else
        {
            _sinceUngrounded += Time.deltaTime;
            _yVel += gravity * Time.deltaTime;
        }

        bool canCoyote = _sinceUngrounded <= coyoteTime;
        bool buffered = Time.time - _jumpRequestedAt <= jumpBufferTime;

        if (buffered && (grounded || canCoyote))
        {
            _jumpRequestedAt = -999f;
            float v0 = Mathf.Sqrt(2f * Mathf.Abs(gravity) * Mathf.Max(0.01f, jumpHeight));
            _yVel = v0;
        }

        Vector3 delta = Vector3.up * _yVel * Time.deltaTime;
        if (delta.sqrMagnitude > 0f && system != null && CanBeginLocomotion())
        {
            BeginLocomotion();
            _cc.Move(delta);     // 수직만 적용(수평은 기존 Move Provider가 처리)
            EndLocomotion();
        }
    }
}