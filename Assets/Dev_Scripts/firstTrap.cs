using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstTrap : MonoBehaviour
{
    private Animator animator; // Animator do obsługi animacji
    private BoxCollider2D boxCollider; // Box Collider 2D do obsługi collidera

    private void Start()
    {
        // Pobierz komponenty Animator i BoxCollider2D
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Sprawdź, czy oba komponenty są dostępne
        if (animator == null || boxCollider == null)
        {
            Debug.LogError("Animator lub BoxCollider2D nie zostały znalezione na tym obiekcie.");
        }
    }

    private void Update()
    {
        // Pobierz aktualny rozmiar obiektu z animacji
        Vector3 currentSize = transform.localScale;

        // Aktualizuj rozmiar box collidera na podstawie aktualnego rozmiaru obiektu
        boxCollider.size = new Vector2(currentSize.x, currentSize.y);

        // Tutaj dodaj kod do obsługi innych rzeczy związanych z animacją lub innymi akcjami
        // ...

        // Jeśli animacja jest ukończona, możesz wykonać dodatkowe czynności
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("NazwaAnimacji")) // Zastąp "NazwaAnimacji" nazwą twojej animacji
        {
            // Dodatkowe czynności po zakończeniu animacji
            // ...
        }
    }
}
