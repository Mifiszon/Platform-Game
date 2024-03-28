using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private float _jumpTime = 0.1f;
    private float _timeToHangOff = 0.02f;
    private float _timer = 0;

    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("Jump");

        if (manager.grounded) manager.rg.AddForce(Vector2.up * manager.jumpForce, ForceMode2D.Impulse);

        _timer = 0;
    }

    public override void FixUpdate(PlayerStateManager manager)
    {
        manager.Movement();
    }

    public override void Update(PlayerStateManager manager)
    {
        _timer += Time.deltaTime;

        manager.UpdateSpriteFacing();

        if (_timer < _jumpTime) return;

        manager.CheckDoubleJumpState();

        manager.CheckIdleState();

        manager.CheckRunState();
    }
}