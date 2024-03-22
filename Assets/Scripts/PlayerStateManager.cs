using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public Vector2 frameVelocity;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D rg;
    public CapsuleCollider2D col;
    public Animator anim;
    public SpriteRenderer render;

    public PlayerBaseState currentState;
    public PlayerBaseState IdleState, RunState, JumpState, DoubleJump;

    public FrameInput frameInput;

    public Vector2 direction;
    public bool grounded;

    private void Awake()
    {
        IdleState = new PlayerIdleState(transform);
        RunState = new PlayerRunState(transform);
        JumpState = new PlayerJumpState(transform);
        DoubleJump = new PlayerDoubleJumpState(transform);
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

        direction = GetAxis();
        currentState.Update(this);

    }

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.down, 0.05f);
        bool ceilingHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.up, 0.05f);

        if (ceilingHit) frameVelocity.y = Mathf.Min(0, frameVelocity.y);

        if (!grounded && groundHit) frameVelocity.y = 0;

        grounded = groundHit;
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.Start(this);
    }

    public Vector2 GetAxis()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter(this, other);
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }
}