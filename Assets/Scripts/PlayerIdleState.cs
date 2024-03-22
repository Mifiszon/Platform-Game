using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Transform player)
    {
    }

    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("Idle");
        manager.rg.velocity = Vector2.zero;
    }

    public override void FixUpdate(PlayerStateManager manager) { }

    public override void Update(PlayerStateManager manager)
    {
        if (manager.grounded && manager.frameInput.Move != Vector2.zero)
        {
            manager.SwitchState(manager.RunState);
        }

        if (manager.grounded && manager.frameInput.JumpDown)
        {
            manager.SwitchState(manager.JumpState);
        }
    }

    public override void OnCollisionEnter(PlayerStateManager manager, Collision2D other) { }

}