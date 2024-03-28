using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action<string> EndGame;

    public static int money = 0;
    public static bool isEndGame = false;

    [SerializeField] GameObject ui, endGame;

    private void Awake()
    {
        Money.Collected += OnMoneyCollected;
        Card.Collected += OnCardCollected;
        PlayerStateManager.Death += OnPlayerDeath;
    }

    private void Start()
    {
        isEndGame = false;
        money = 0;
    }

    public void ReloadGame()
    {
        ui.SetActive(true);
        endGame.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void OnMoneyCollected()
    {
        money += 1;
    }

    private void OnCardCollected()
    {
        DisplayEndGame();
        isEndGame = true;
        EndGame.Invoke("Level Complete");
    }

    private void OnPlayerDeath()
    {
        DisplayEndGame();
        isEndGame = true;
        EndGame.Invoke("Game Over");
    }

    private void DisplayEndGame()
    {
        ui.SetActive(false);
        endGame.SetActive(true);
    }

    private void OnDestroy()
    {
        Card.Collected -= OnCardCollected;
        PlayerStateManager.Death -= OnPlayerDeath;
    }
}
