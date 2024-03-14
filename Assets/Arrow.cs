using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float movementSpeed = 5f; // Prędkość przesuwania obiektu

    void Update()
    {
       
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        gameObject.SetActive(false);
        
    }
}
