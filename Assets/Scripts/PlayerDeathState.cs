using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("Death");
    }

    public override void FixUpdate(PlayerStateManager manager)
    {

    }

    public override void Update(PlayerStateManager manager)
    {

    }
}