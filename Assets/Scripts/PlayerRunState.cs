using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private Animator animator;
    private Rigidbody2D rig;
    private SpriteRenderer renderer;

    public PlayerRunState(Transform player)
    {
        animator = player.GetComponent<Animator>();
        rig = player.GetComponent<Rigidbody2D>();
        renderer = player.GetComponent<SpriteRenderer>();
    }

    public override void Start(PlayerStateManager manager)
    {
        animator.Play("Run");
    }

    public override void FixUpdate(PlayerStateManager manager)
    {
        rig.velocity = new Vector2(manager.direction.x * manager.speed, rig.velocity.y);
    }

    public override void Update(PlayerStateManager manager)
    {
        renderer.flipX = manager.direction.x < 0 || (manager.direction.x <= 0 && renderer.flipX);

        if (manager.grounded && manager.frameInput.Move == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
        }

        if (manager.grounded && manager.frameInput.JumpDown)
        {
            manager.SwitchState(manager.JumpState);
        }
    }


    public override void OnCollisionEnter(PlayerStateManager manager, Collision2D other)
    {
        return;
    }
}