using System;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    Animator animator;
    Rigidbody2D rig;
    SpriteRenderer renderer;

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

    public override void Update(PlayerStateManager manager)
    {
        var direction = manager.GetAxis();

        renderer.flipX = direction.x < 0 || (direction.x <= 0 && renderer.flipX);

        rig.velocity = new Vector2(direction.x * manager.speed, rig.velocity.y);

        if (direction == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
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