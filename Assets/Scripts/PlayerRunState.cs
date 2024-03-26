using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("Run");
    }

    public override void FixUpdate(PlayerStateManager manager)
    {
        manager.Movement();
    }

    public override void Update(PlayerStateManager manager)
    {
        manager.UpdateSpriteFacing();

        manager.CheckJumpState();

        manager.CheckIdleState();
    }
}