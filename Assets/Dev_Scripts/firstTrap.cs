using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstTrap : MonoBehaviour
{
    public float zakresPoruszania = 5f; // Zakres poruszania obiektu
    public float predkoscPoruszania = 2f; // Prędkość poruszania obiektu

    private bool idzieWGore = true; // Flaga określająca kierunek ruchu obiektu

    void Update()
    {
        // Sprawdzamy kierunek ruchu i aktualizujemy pozycję obiektu
        if (idzieWGore)
            transform.Translate(Vector3.up * predkoscPoruszania * Time.deltaTime);
        else
            transform.Translate(Vector3.down * predkoscPoruszania * Time.deltaTime);

        // Jeżeli obiekt przekroczy górny zakres, zmieniamy kierunek na dół
        if (transform.position.y > zakresPoruszania / 2)
            idzieWGore = false;

        // Jeżeli obiekt przekroczy dolny zakres, zmieniamy kierunek na górę
        if (transform.position.y < -zakresPoruszania / 2)
            idzieWGore = true;
    }
}
