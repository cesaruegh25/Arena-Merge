using TMPro;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject UIGameplay;
    public Slider UIVida;
    public TextMeshProUGUI UITextVida;
    public Slider UIShield;
    public TextMeshProUGUI UITextShield;
    public GameObject UITienda;
    public TextMeshProUGUI UITiempo;
    public TextMeshProUGUI UIScore;

    [Header("Gameplay")]
    public int score;
    public int MaxHealth = 100;
    public int health;
    public int maxShield = 100;
    public int shield;
    public float speed = 7f;
    public float time;
    public bool isGame;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (BattleManager.selectedBattle != null)
        {
            time = BattleManager.selectedBattle.timeLimit;
        }
        score = 0;
        health = MaxHealth;
        shield = 0;
        UIShield.maxValue = MaxHealth;
        UIVida.value = health;
        UITextVida.text = health.ToString() + " / " + MaxHealth.ToString();
        UIShield.value = shield;
        UITextShield.text = shield.ToString() + " / " + maxShield.ToString();
        UIScore.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Time.timeScale = 0; // Detiene el juego
            isGame = false;
            BattleManager.score = 0;
            SceneManager.LoadScene("Menu");
        }
        if (isGame)
        {
            time -= Time.deltaTime;
            UITiempo.text = time.ToString("F1");
            if (time <= 0)
            {
                Time.timeScale = 0; // Detiene el juego
                isGame = false;
                BattleManager.score = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }
    public void GameBegin()
    {
        Time.timeScale = 1;
        LoadoutManager.Instance.SaveInventory();
        UITienda.SetActive(false);
        UIGameplay.SetActive(true);
        isGame = true;
    }
    public void ActualizarUI()
    {
        if (health >= MaxHealth)health = MaxHealth;
        if (shield >= maxShield)shield = maxShield;
        UIVida.value = health;
        UITextVida.text = health.ToString() + " / " + MaxHealth.ToString();
        UIShield.value = shield;
        UITextShield.text = shield.ToString() + " / " + maxShield.ToString();
    }
    public void ActualizarMaxHealth()
    {
        UIVida.maxValue = MaxHealth;
        UIVida.value = MaxHealth;
        UITextVida.text = health.ToString() + " / " + MaxHealth.ToString();
        UITextShield.text = shield.ToString() + " / " + maxShield.ToString();
    }
    public void Score(int value)
    {
        score += value;
        UIScore.text = "Score: " + score;
    }

    public void PlayerIsDamage(int damage)
    {
        if (shield > 0)
        {
            int shieldDamage = Mathf.Min(shield, damage);
            shield -= shieldDamage;
            damage -= shieldDamage;
        }
        health -= damage;
    }
}
