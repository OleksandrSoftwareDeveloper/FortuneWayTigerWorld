using UnityEngine;
using UnityEngine.UI;

public class SoundsButton : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(AudioListener.volume == 1);
        transform.GetChild(1).gameObject.SetActive(AudioListener.volume != 1);
        GetComponent<Button>().onClick.AddListener(ChangeSoundState);
    }

    private void ChangeSoundState()
    {
        AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
        transform.GetChild(0).gameObject.SetActive(AudioListener.volume == 1);
        transform.GetChild(1).gameObject.SetActive(AudioListener.volume != 1);
    }
}