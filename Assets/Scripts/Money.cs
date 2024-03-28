using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static event Action Collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Collected.Invoke();
            Destroy(gameObject);
        }
    }
}
