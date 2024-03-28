using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _money;
    [SerializeField] TextMeshProUGUI _text;

    private void Awake()
    {
        GameManager.EndGame += OnEndGame;
    }

    private void OnEndGame(string text)
    {
        _text.text = text;
        _money.text = GameManager.money.ToString();
    }

    private void OnDestroy()
    {
        GameManager.EndGame -= OnEndGame;
    }
}
