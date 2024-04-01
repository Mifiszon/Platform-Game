using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // Komponent używany do renderowania grafiki gracza
    [HideInInspector] public SpriteRenderer render;
    // aktualny stan gracza
    public IPlayerState currentState;
    // stany z których gracz korzysta
    public IPlayerState IdleState;
    // wejście wprowadzone przez gracza z klawiatury
    public FrameInput frameInput;

    // Tutaj definiujemy Stany gracza
    private void Awake()
    {
        IdleState = new PlayerIdleState();
    }

    // Tutaj pobieramy komponenty na których będziemy operować
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        SwitchState(IdleState);
    }

    // Tutaj wykonywana jest logika fizyki
    private void FixedUpdate()
    {
        currentState.FixUpdate(this);
    }

    // Tutaj sprawdzane są wciśnięte klawisze oraz przejście gracza do innego stanu
    private void Update()
    {
        frameInput = new FrameInput
        {
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        UpdateSpriteFacing();

        currentState.Update(this);
    }

    // Zmienia obrócenie obrazu gracza w zależności od wciśniętego kierunku (left arrow oraz right arrow )
    public void UpdateSpriteFacing() => render.flipX = frameInput.Move.x < 0 || (frameInput.Move.x <= 0 && render.flipX);

    // Służy do definiowania aktualnego stanu gracza
    public void SwitchState(IPlayerState state)
    {
        currentState = state;
        currentState.Start(this);
    }

    // służy do przechowywania informacji wprwadzonych z klawiatury
    public struct FrameInput
    {
        public Vector2 Move;
    }
}