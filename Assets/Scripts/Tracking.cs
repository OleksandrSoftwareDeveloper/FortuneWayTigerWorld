using UnityEngine;

public class Tracking : MonoBehaviour
{
    [SerializeField] private Transform ObjectWhichWillBeTracked;
    [SerializeField] private Vector3 Offset;

    private void LateUpdate()
    {
        transform.position = ObjectWhichWillBeTracked.transform.position + Offset;
    }
}