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
    public IPlayerState RunState;
    public IPlayerState JumpState;

    // wejście wprowadzone przez gracza z klawiatury
    public FrameInput frameInput;

    //zmienne
    public float speed;
    public bool grounded;
    public float jumpForce;
    
    //komponenty
    public Rigidbody2D rig;
    public Animator animator;
    public CapsuleCollider2D col;

    // Tutaj definiujemy Stany gracza
    private void Awake()
    {
        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        JumpState = new PlayerJumpState();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Tutaj pobieramy komponenty na których będziemy operować
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        SwitchState(IdleState);
    }

    // Tutaj wykonywana jest logika fizyki
    private void FixedUpdate()
    {
        currentState.FixUpdate(this);
        CheckCollisions();
        
    }

    // Tutaj sprawdzane są wciśnięte klawisze oraz przejście gracza do innego stanu
    private void Update()
    {

        frameInput = new FrameInput
        {
            Jump = Input.GetButtonDown("Jump"),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        frameInput.Move.x = Mathf.Abs(frameInput.Move.x) < 0.1f ? 0 :
        Mathf.Sign(frameInput.Move.x);

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
        public bool Jump;
    }

    private void CheckCollisions()
    {
        //zapobiega kolizji z wkasnym koliderem
        Physics2D.queriesStartInColliders = false;

        //uzywamy kopii kolidera gracza skierowanego o 0.05
        //jednostki w dół by zmierzyc kolizje
        grounded = Physics2D.CapsuleCast(col.bounds.center,
        col.size, col.direction, 0, Vector2.down, 0.05f);
    }

}