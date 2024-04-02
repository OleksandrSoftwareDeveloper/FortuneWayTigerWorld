using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private int Price = 5;
    [SerializeField] private string KeyForPlayerPrefs;
    [SerializeField] private GameObject NotEnoughMoneyScreen;

    private Money Money;

    private void Awake()
    {
        Money = FindFirstObjectByType<Money>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if(Money.CurrentQuantity >= Price)
        {
            PlayerPrefs.SetInt(KeyForPlayerPrefs, PlayerPrefs.GetInt(KeyForPlayerPrefs) + 1);
            Money.ChangeMoneyAmnount(-Price);
        }
        else
        {
            NotEnoughMoneyScreen.SetActive(true);
        }
    }
}