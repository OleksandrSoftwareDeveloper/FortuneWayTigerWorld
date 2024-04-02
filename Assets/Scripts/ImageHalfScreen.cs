using UnityEngine;
using UnityEngine.UI;

public class ImageHalfScreen : MonoBehaviour
{
    [SerializeField] private bool LeftPart;

    private void OnEnable()
    {
        RectTransform ThisRectTransform = GetComponent<RectTransform>();
        RectTransform CanvasRectTransform = (RectTransform)(ThisRectTransform.root);
        CanvasScaler CanvasScalerOfTheRoot = CanvasRectTransform.GetComponent<CanvasScaler>();
        int Sign = LeftPart ? -1 : 1;
        float ScreenSidesRatio = CanvasRectTransform.sizeDelta.x / CanvasRectTransform.sizeDelta.y;
        ThisRectTransform.sizeDelta = new Vector2(ScreenSidesRatio * CanvasScalerOfTheRoot.referenceResolution.y / 2, CanvasScalerOfTheRoot.referenceResolution.y);
        ThisRectTransform.anchoredPosition = new Vector2(ScreenSidesRatio * CanvasScalerOfTheRoot.referenceResolution.y / 4 * Sign, 0);
    }
}