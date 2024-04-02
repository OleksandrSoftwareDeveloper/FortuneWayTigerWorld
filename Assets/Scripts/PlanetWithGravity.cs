using UnityEngine;

public class PlanetWithGravity : MonoBehaviour
{
    [SerializeField] private Transform planet;
    [SerializeField] private float gravityForce = 500;

    public Transform Planet { get => planet; }
    public float GravityForce { get => gravityForce; }
}
