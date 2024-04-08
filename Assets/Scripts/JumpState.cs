using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public void Start(PlayerStateManager manager) 
    { 
        _timer = 0;
    }

    public void FixUpdate(PlayerStateManager manager) 
    {
        if (manager.grounded)
        {
            manager.rig.AddForce(Vector2.up * manager.jumpForce,
            ForceMode2D.Impulse);
        }

    }

    public void Update(PlayerStateManager manager) 
    {

        _timer += Time.deltaTime;
        if (_timer < _jumpTime) return;

        float moveSpeed = manager.frameInput.Move.x * manager.speed;
        manager.rig.velocity = new Vector2(moveSpeed, manager.rig.velocity.y);

        if (manager.frameInput.Move != Vector2.zero)
        {
            manager.SwitchState(manager.RunState);
        }
        if (manager.frameInput.Move == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
        }
    }
}