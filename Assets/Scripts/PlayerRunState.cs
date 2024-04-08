using UnityEngine;

public class PlayerRunState : IPlayerState
{
    public void Start(PlayerStateManager manager) 
    {
        manager.animator.Play("Run");
    }

    public void FixUpdate(PlayerStateManager manager) 
    { 
        float moveSpeed = manager.frameInput.Move.x * manager.speed;

        manager.rig.velocity = new Vector2(moveSpeed, manager.rig.velocity.y);

    }

    public void Update(PlayerStateManager manager) 
    {
        if (manager.frameInput.Move == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
        }
        if(manager.grounded && manager.frameInput.Jump)
        {
            manager.SwitchState(manager.JumpState);
        }
     }
}