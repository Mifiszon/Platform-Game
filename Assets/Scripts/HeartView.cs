using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeartView : MonoBehaviour
{
    [SerializeField] Image[] _hearts;

    private void Awake()
    {
        PlayerStateManager.Damage += OnPlayerGetDamage;
    }

    private void OnPlayerGetDamage()
    {
        foreach (var heart in _hearts.Reverse())
        {
            if (heart.enabled)
            {
                heart.enabled = false;
                break;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerStateManager.Damage -= OnPlayerGetDamage;
    }
}
