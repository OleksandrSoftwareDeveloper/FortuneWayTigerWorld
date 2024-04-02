using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ThisRigidbody;
    [SerializeField] private float StartSpeed = 5;
    [SerializeField] private float Acceleration = 0.003f;
    [SerializeField] private float RotationSpeed = 0.01f;
    [SerializeField] private PlanetWithGravity[] PlanetsWithGravity;

    [Header("Money")]
    [SerializeField] private Money Money;

    [Header("Road generator")]
    [SerializeField] private GameObject PlanetsSet;
    [SerializeField] private float DistanceForNextPlanetsSet = 120;
    [SerializeField] private GameObject Background;
    [SerializeField] private float DistanceForNextBackground = 400;

    [Header("In-game UI")]
    [SerializeField] private Text TextWithScoreInGame;
    [SerializeField] private Text TextWithCurrentCoins;

    [Header("Audio")]
    [SerializeField] private AudioSource CoinSound;
    [SerializeField] private AudioSource DefeatSound;

    [Header("Boosters")]
    [SerializeField] private Button GravityDisablerButton;
    [SerializeField] private Button MoneyDoublerButton;
    [SerializeField] private Button ColliderDisablerButton;
    [SerializeField] private Button ScoreIncreaserButton;

    [Header("DefeatUI")]
    [SerializeField] private GameObject DefeatUI;
    [SerializeField] private Text TextWithScore;
    [SerializeField] private Text TextWithFinalCoins;

    private int CurrentPlanetsSet = 0;
    private int CurrentBackground = 0;
    private int Score;
    private float StartPlayerPosition;
    private float Speed;
    private int CurrentQuantityOfNewCoins = 0;
    private bool IsGravityDisabled = false;
    private bool IsMoneyDoubled = false;
    private bool IsColliderDisabled = false;
    private int AddedScore = 0;

    private void Awake()
    {
        Time.timeScale = 0;
        StartPlayerPosition = transform.position.y;
        Speed = StartSpeed;
        ThisRigidbody = GetComponent<Rigidbody2D>();
        PlanetsWithGravity = FindObjectsOfType<PlanetWithGravity>();
        TextWithCurrentCoins.text = $"{CurrentQuantityOfNewCoins}";

        GravityDisablerButton.onClick.AddListener(DisableGravityButton);
        GravityDisablerButton.GetComponentInChildren<Text>().text = $"{PlayerPrefs.GetInt("GravityDisabler")}";

        MoneyDoublerButton.onClick.AddListener(DoubleCoinsButton);
        MoneyDoublerButton.GetComponentInChildren<Text>().text = $"{PlayerPrefs.GetInt("MoneyDoubler")}";

        ColliderDisablerButton.onClick.AddListener(DisableColliderButton);
        ColliderDisablerButton.GetComponentInChildren<Text>().text = $"{PlayerPrefs.GetInt("ColliderDisabler")}";

        ScoreIncreaserButton.onClick.AddListener(IncreaseMoneyButton);
        ScoreIncreaserButton.GetComponentInChildren<Text>().text = $"{PlayerPrefs.GetInt("ScoreIncreaser")}";
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch FirstTouch = Input.GetTouch(0);
            float ScreenWidth = Screen.width;
            if (FirstTouch.position.x < ScreenWidth / 2)
            {
                transform.rotation *= Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.forward);
            }
            else
            {
                transform.rotation *= Quaternion.AngleAxis(-RotationSpeed * Time.deltaTime, Vector3.forward);
            }
        }

        Vector3 Velocity = (Speed) * transform.up;
        Speed += Acceleration;

        if (IsGravityDisabled == false)
        {
            for (int i = 0; i < PlanetsWithGravity.Length; i++)
            {
                Vector3 DistanceToPlanet = -transform.position + PlanetsWithGravity[i].Planet.position;
                Velocity += (((Speed / StartSpeed) * PlanetsWithGravity[i].GravityForce / Mathf.Pow(DistanceToPlanet.magnitude, 2)) * DistanceToPlanet.normalized);
            }
        }

        ThisRigidbody.velocity = Velocity;


        if(CurrentPlanetsSet * DistanceForNextPlanetsSet - transform.position.y < DistanceForNextPlanetsSet / 6)
        {
            CurrentPlanetsSet++;
            Instantiate(PlanetsSet, new Vector3(PlanetsSet.transform.position.x, CurrentPlanetsSet * DistanceForNextPlanetsSet, 0), Quaternion.identity);
        }
        if(CurrentBackground * DistanceForNextBackground - transform.position.y < DistanceForNextBackground / 6)
        {
            CurrentBackground++;
            Instantiate(Background, new Vector3(Background.transform.position.x, CurrentBackground * DistanceForNextBackground, 0), Quaternion.identity);
        }

        Score = (int)(transform.position.y - StartPlayerPosition) + AddedScore;
        TextWithScoreInGame.text = $"{Score}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Defeat"))
        {
            DefeatSound.Play();
            Time.timeScale = 0;
            DefeatUI.SetActive(true);
            TextWithScore.text += Score.ToString();
            TextWithFinalCoins.text = $"{Money.CurrentQuantity + CurrentQuantityOfNewCoins}";
            Money.ChangeMoneyAmnount(CurrentQuantityOfNewCoins);
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            CoinSound.Play();
            CurrentQuantityOfNewCoins = IsMoneyDoubled ? CurrentQuantityOfNewCoins + 2 : CurrentQuantityOfNewCoins + 1;
            TextWithCurrentCoins.text = $"{CurrentQuantityOfNewCoins}";
            Destroy(collision.gameObject);
        }
    }

    private void DisableGravityButton()
    {
        int QuantityOfTimes = PlayerPrefs.GetInt("GravityDisabler");
        if (IsGravityDisabled || QuantityOfTimes == 0)
        {
            return;
        }
        PlayerPrefs.SetInt("GravityDisabler", QuantityOfTimes - 1);
        GravityDisablerButton.GetComponentInChildren<Text>().text = $"{QuantityOfTimes - 1}";
        StartCoroutine(CoroutineForDisablingGravity());
    }
    private IEnumerator CoroutineForDisablingGravity()
    {
        IsGravityDisabled = true;
        yield return new WaitForSecondsRealtime(5);
        IsGravityDisabled = false;
    }

    private void DoubleCoinsButton()
    {
        int QuantityOfTimes = PlayerPrefs.GetInt("MoneyDoubler");
        if (IsMoneyDoubled || QuantityOfTimes == 0)
        {
            return;
        }
        PlayerPrefs.SetInt("MoneyDoubler", QuantityOfTimes - 1);
        MoneyDoublerButton.GetComponentInChildren<Text>().text = $"{QuantityOfTimes - 1}";
        StartCoroutine(CoroutineForDoublingCoins());
    }
    private IEnumerator CoroutineForDoublingCoins()
    {
        IsMoneyDoubled = true;
        yield return new WaitForSecondsRealtime(5);
        IsMoneyDoubled = false;
    }

    private void DisableColliderButton()
    {
        int QuantityOfTimes = PlayerPrefs.GetInt("ColliderDisabler");
        if (IsColliderDisabled || QuantityOfTimes == 0)
        {
            return;
        }
        PlayerPrefs.SetInt("ColliderDisabler", QuantityOfTimes - 1);
        ColliderDisablerButton.GetComponentInChildren<Text>().text = $"{QuantityOfTimes - 1}";
        StartCoroutine(CoroutineForDisablingCollider());
    }
    private IEnumerator CoroutineForDisablingCollider()
    {
        IsColliderDisabled = true;
        IsGravityDisabled = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(5);
        IsColliderDisabled = false;
        IsGravityDisabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    private void IncreaseMoneyButton()
    {
        int QuantityOfTimes = PlayerPrefs.GetInt("ScoreIncreaser");
        if (QuantityOfTimes == 0)
        {
            return;
        }
        PlayerPrefs.SetInt("ScoreIncreaser", QuantityOfTimes - 1);
        ScoreIncreaserButton.GetComponentInChildren<Text>().text = $"{QuantityOfTimes - 1}";
        AddedScore += 100;
    }

    public void Pause() => Time.timeScale = 0;
    public void Continue() => Time.timeScale = 1;
}