using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    private int _money = 0;

    private void Awake()
    {
        Money.Collected += OnMoneyCollected;
    }

    private void OnMoneyCollected()
    {
        _text.SetText((++_money).ToString());
    }

    private void OnDestroy()
    {
        Card.Collected -= OnMoneyCollected;
    }
}
