using UnityEngine;

public class PlayerDoubleJumpState : PlayerBaseState
{
    private float _jumpTime = 0.5f;
    private float _timer = 0;
    private bool _isPlayJumpAnim = false;

    public PlayerDoubleJumpState(Transform player) { }

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
        manager.rg.velocity = new Vector2(manager.direction.x * manager.speed, manager.rg.velocity.y);
    }

    public override void Update(PlayerStateManager manager)
    {
        _timer += Time.deltaTime;

        if (_timer < _jumpTime) return;

        if (!_isPlayJumpAnim && _timer > _jumpTime)
        {
            manager.anim.Play("Jump");
            _isPlayJumpAnim = true;
        }

        if (manager.grounded && manager.frameInput.Move == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
        }

        if (manager.grounded && manager.frameInput.Move != Vector2.zero)
        {
            manager.SwitchState(manager.RunState);
        }
    }


    public override void OnCollisionEnter(PlayerStateManager manager, Collision2D other)
    {
    }


}