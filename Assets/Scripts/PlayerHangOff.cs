using UnityEngine;

public class PlayerHangOff : PlayerBaseState
{
    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("HangOff");
        manager.rg.velocity = Vector2.zero;
        manager.rg.simulated = false;

        manager.render.flipX = manager.rightWallCollision;
    }

    public override void FixUpdate(PlayerStateManager manager)
    {
    }

    public override void Update(PlayerStateManager manager)
    {
        if (manager.frameInput.JumpDown)
            SwitchToJumpState(manager);
    }

    private void SwitchToJumpState(PlayerStateManager manager)
    {
        manager.rg.simulated = true;
        manager.SwitchState(manager.JumpState);
    }
}