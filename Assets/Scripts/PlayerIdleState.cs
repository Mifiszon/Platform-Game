using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("Idle");
        manager.rg.velocity = Vector2.zero;
    }

    public override void FixUpdate(PlayerStateManager manager) { }

    public override void Update(PlayerStateManager manager)
    {
        manager.CheckRunState();

        manager.CheckJumpState();
    }
}