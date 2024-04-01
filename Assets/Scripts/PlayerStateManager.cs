using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer render;

    public IPlayerState currentState;
    public IPlayerState IdleState;

    public FrameInput frameInput;

    private void Awake()
    {
        IdleState = new PlayerIdleState();
    }

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        SwitchState(IdleState);
    }

    private void FixedUpdate()
    {
        currentState.FixUpdate(this);
    }

    private void Update()
    {
        frameInput = new FrameInput
        {
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        currentState.Update(this);
    }

    public void UpdateSpriteFacing() => render.flipX = frameInput.Move.x < 0 || (frameInput.Move.x <= 0 && render.flipX);

    public void SwitchState(IPlayerState state)
    {
        currentState = state;
        currentState.Start(this);
    }

    public struct FrameInput
    {
        public Vector2 Move;
    }
}