using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public void Start(PlayerStateManager manager) 
    { 
        manager.rig.velocity = Vector2.zero;
        manager.animator.Play("Idle");
    }

    public void FixUpdate(PlayerStateManager manager) { }

    public void Update(PlayerStateManager manager) 
    {
        if (manager.frameInput.Move != Vector2.zero)
        {
            manager.SwitchState(manager.RunState);
        }
        if(manager.grounded && manager.frameInput.Jump)
        {
            manager.SwitchState(manager.JumpState);
        }
    }
}