using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public PlayerBaseState currentState;
    public PlayerBaseState IdleState, RunState, JumpState;

    private void Awake()
    {
        IdleState = new PlayerIdleState(transform);
        RunState = new PlayerRunState(transform);
        JumpState = new PlayerJumpState(transform);
    }

    private void Start()
    {
        SwitchState(IdleState);
    }

    private void Update()
    {
        currentState.Update(this);
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

        x = x > 0 ? 1 : (x < 0 ? -1 : 0);
        y = y > 0 ? 1 : (y < 0 ? -1 : 0);

        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnter(this, other);
    }
}