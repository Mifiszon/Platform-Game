using TMPro;
using UnityEngine;

public class CardCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _cardText;
    private int _cards = 0;

    private void Awake()
    {
        Card.Collected += OnCardCollected;
    }

    private void OnCardCollected()
    {
        _cardText.SetText((++_cards).ToString());
    }

    private void OnDestroy()
    {
        Card.Collected -= OnCardCollected;
    }
}
