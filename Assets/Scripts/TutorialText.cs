using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private List<string> Content;
    [SerializeField] private Button NextButton;
    [SerializeField] private Button PreviousButton;

    private int CurrentIndex;

    private void Awake()
    {
        NextButton.gameObject.SetActive(CurrentIndex != Content.Count - 1);
        PreviousButton.gameObject.SetActive(CurrentIndex != 0);
        NextButton.onClick.AddListener(() =>
        {
            GetComponent<Text>().text = Content[CurrentIndex + 1];
            CurrentIndex++;
            NextButton.gameObject.SetActive(CurrentIndex != Content.Count - 1);
            PreviousButton.gameObject.SetActive(CurrentIndex != 0);
        });
        PreviousButton.onClick.AddListener(() =>
        {
            GetComponent<Text>().text = Content[CurrentIndex - 1];
            CurrentIndex--;
            NextButton.gameObject.SetActive(CurrentIndex != Content.Count - 1);
            PreviousButton.gameObject.SetActive(CurrentIndex != 0);
        });
    }
}