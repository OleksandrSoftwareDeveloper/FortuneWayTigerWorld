using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private int PagesQuantity = 5;
    [SerializeField] private Button NextButton;
    [SerializeField] private Button PreviousButton;

    private List<GameObject> Pages = new();
    private int CurrentIndex;

    private void Awake()
    {
        for(int i = 0; i < PagesQuantity; i++)
        {
            Pages.Add(transform.GetChild(i).gameObject);
        }
        NextButton.gameObject.SetActive(CurrentIndex != PagesQuantity - 1);
        PreviousButton.gameObject.SetActive(CurrentIndex != 0);
        NextButton.onClick.AddListener(() =>
        {
            for(int i = 0; i < PagesQuantity; i++)
            {
                Pages[i].SetActive(i == CurrentIndex + 1);
            }
            CurrentIndex++;
            NextButton.gameObject.SetActive(CurrentIndex != PagesQuantity - 1);
            PreviousButton.gameObject.SetActive(CurrentIndex != 0);
        });
        PreviousButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < PagesQuantity; i++)
            {
                Pages[i].SetActive(i == CurrentIndex - 1);
            }
            CurrentIndex--;
            NextButton.gameObject.SetActive(CurrentIndex != PagesQuantity - 1);
            PreviousButton.gameObject.SetActive(CurrentIndex != 0);
        });
    }
}