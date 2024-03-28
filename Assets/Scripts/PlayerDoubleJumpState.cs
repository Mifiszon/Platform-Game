using UnityEngine;

public class PlayerDoubleJumpState : PlayerBaseState
{
    private float _jumpTime = 0.5f;
    private float _timer = 0;
    private bool _isPlayJumpAnim = false;

    public override void Start(PlayerStateManager manager)
    {
        manager.anim.Play("DoubleJump");
        manager.rg.velocity = Vector2.zero;
        manager.rg.AddForce(Vector2.up * manager.jumpForce, ForceMode2D.Impulse);
        _timer = 0;
        _isPlayJumpAnim = false;
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

        if (!_isPlayJumpAnim && _timer > _jumpTime)
        {
            manager.anim.Play("Jump");
            _isPlayJumpAnim = true;
        }

        manager.CheckIdleState();

        manager.CheckRunState();
    }
}