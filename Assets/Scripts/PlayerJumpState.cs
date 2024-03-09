using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private Animator animator;
    private Rigidbody2D rig;
    private SpriteRenderer renderer;

    public PlayerJumpState(Transform player)
    {
        animator = player.GetComponent<Animator>();
        rig = player.GetComponent<Rigidbody2D>();
        renderer = player.GetComponent<SpriteRenderer>();
    }

    public override void Start(PlayerStateManager manager)
    {
        animator.Play("Jump");
        rig.AddForce(Vector2.up * manager.jumpForce, ForceMode2D.Impulse);
    }

    public override void Update(PlayerStateManager manager)
    {
        var direction = manager.GetAxis();

        renderer.flipX = direction.x < 0 || (direction.x <= 0 && renderer.flipX);

        rig.velocity = new Vector2(direction.x * manager.speed, rig.velocity.y);
    }

    public override void OnCollisionEnter(PlayerStateManager manager, Collision2D other)
    {
        manager.SwitchState(manager.IdleState);
    }
}