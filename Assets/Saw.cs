using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public List<Transform> pointsForSaw;
    public float movementSpeed = 5f;

    private int indeks = 0;

    void Start()
    {
        if (pointsForSaw.Count > 0)
        {
            
            transform.position = pointsForSaw[0].position;
        }
    }

    void Update()
    {
        if (pointsForSaw.Count == 0)
        {
            Debug.LogWarning("Lista punktów poruszania jest pusta!");
            return;
        }

       
        Vector3 kierunek = pointsForSaw[indeks].position - transform.position;
        transform.Translate(kierunek.normalized * movementSpeed * Time.deltaTime, Space.World);

        
        if (Vector3.Distance(transform.position, pointsForSaw[indeks].position) < 0.1f)
        {
            
            indeks = (indeks + 1) % pointsForSaw.Count;
        }
    }
}
