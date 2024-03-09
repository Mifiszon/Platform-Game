using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private Animator animator;

    public PlayerIdleState(Transform player)
    {
        animator = player.GetComponent<Animator>();
    }

    public override void Start(PlayerStateManager manager)
    {
        animator.Play("Idle");
    }

    public override void Update(PlayerStateManager manager)
    {
        var direction = manager.GetAxis();

        if (direction != Vector2.zero)
        {
            manager.SwitchState(manager.RunState);
        }

        if (Input.GetAxis("Jump") != 0)
        {
            manager.SwitchState(manager.JumpState);
        }
    }

    public override void OnCollisionEnter(PlayerStateManager manager, Collision2D other)
    {
        return;
    }
}