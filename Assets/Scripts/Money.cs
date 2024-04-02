using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] private Text[] TextsWithMoney;

    private const string PLAYER_PREFS_KEY = "MONEY";

    public int CurrentQuantity { get; private set; }

    private void Awake()
    {
        if(PlayerPrefs.HasKey(PLAYER_PREFS_KEY))
        {
            CurrentQuantity = PlayerPrefs.GetInt(PLAYER_PREFS_KEY);
        }
        else
        {
            CurrentQuantity = 0;
            PlayerPrefs.SetInt(PLAYER_PREFS_KEY, 0);
        }
        if (TextsWithMoney != null)
        {
            foreach (Text oneTextWithMoney in TextsWithMoney)
            {
                oneTextWithMoney.text = CurrentQuantity.ToString();
            }
        }
    }

    public void ChangeMoneyAmnount(int deltaMoneyAmount)
    {
        CurrentQuantity += deltaMoneyAmount;
        if (TextsWithMoney != null)
        {
            foreach (Text oneTextWithMoney in TextsWithMoney)
            {
                oneTextWithMoney.text = CurrentQuantity.ToString();
            }
        }
        PlayerPrefs.SetInt(PLAYER_PREFS_KEY, CurrentQuantity);
    }
}