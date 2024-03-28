using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static event Action Damage;

    public float speed;
    public float jumpForce;

    [HideInInspector] public Rigidbody2D rg;
    [HideInInspector] public CapsuleCollider2D col;
    [HideInInspector] public Animator anim;
    [HideInInspector] public SpriteRenderer render;

    public PlayerBaseState currentState;
    public PlayerBaseState IdleState, RunState, JumpState, DoubleJump, HangOff;

    public FrameInput frameInput;

    public bool grounded, leftWallCollision, rightWallCollision;

    private void Awake()
    {
        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        JumpState = new PlayerJumpState();
        DoubleJump = new PlayerDoubleJumpState();
        HangOff = new PlayerHangOff();
    }

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        SwitchState(IdleState);
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        currentState.FixUpdate(this);
    }

    private void Update()
    {
        frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        frameInput.Move.x = Mathf.Abs(frameInput.Move.x) < 0.1f ? 0 : Mathf.Sign(frameInput.Move.x);
        frameInput.Move.y = Mathf.Abs(frameInput.Move.y) < 0.3f ? 0 : Mathf.Sign(frameInput.Move.y);

        currentState.Update(this);
    }

    public void CheckJumpState() { if (grounded && frameInput.JumpDown) SwitchState(JumpState); }
    public void CheckDoubleJumpState() { if (!grounded && frameInput.JumpDown) SwitchState(DoubleJump); }
    public void CheckIdleState() { if (grounded && frameInput.Move == Vector2.zero) SwitchState(IdleState); }
    public void CheckRunState() { if (grounded && frameInput.Move.x != 0) SwitchState(RunState); }
    public void CheckHangOffState() { if (!grounded && (leftWallCollision || rightWallCollision)) SwitchState(HangOff); }

    public void Movement() => rg.velocity = new Vector2(frameInput.Move.x * speed, rg.velocity.y);

    public void UpdateSpriteFacing() => render.flipX = frameInput.Move.x < 0 || (frameInput.Move.x <= 0 && render.flipX);

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;
        grounded = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.down, 0.05f);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.Start(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Trap"))
        {
            Damage.Invoke();
        }
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }
}