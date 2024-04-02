using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image ProgressBar;
    [SerializeField] private string NameOfSceneToBeOpneed = "MainMenu";

    private float OpeningTime;
    private float TimerCurrentValue = 0;

    private void Awake()
    {
        OpeningTime = Random.Range(2f, 4f);
    }

    private void Update()
    {
        if (TimerCurrentValue < OpeningTime)
        {
            TimerCurrentValue += Time.deltaTime;
            ProgressBar.fillAmount = Mathf.Lerp(0, 1, TimerCurrentValue / OpeningTime);
        }
        else
        {
            SceneManager.LoadScene(NameOfSceneToBeOpneed);
        }
    }
}