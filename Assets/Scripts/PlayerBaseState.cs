using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void Start(PlayerStateManager manager);
    public abstract void Update(PlayerStateManager manager);
    public abstract void OnCollisionEnter(PlayerStateManager manager, Collision2D other);
}