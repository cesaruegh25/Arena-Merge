using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject UIGameplay;
    public Slider UIVida;
    public Slider UIShield;
    public GameObject UITienda;
    public TextMeshProUGUI UITiempo;

    public int score;
    public int MaxHealth = 100;
    public int health;
    public int shield;
    public float speed = 5f;
    public float time;
    public bool isGame;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        UIShield.value = shield;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Time.timeScale = 0; // Detiene el juego
            SceneManager.LoadScene("Menu");
        }
        if (isGame)
        {
            time -= Time.deltaTime;
            UITiempo.text = time.ToString("F1");
            if (time <= 0)
            {
                Time.timeScale = 0; // Detiene el juego
                SceneManager.LoadScene("Menu");
            }
        }
    }
    public void GameBegin()
    {
        LoadoutManager.Instance.SaveInventory();
        UITienda.SetActive(false);
        UIGameplay.SetActive(true);
        isGame = true;
    }
    public void ActualizarUI()
    {
        if (health >= MaxHealth)health = MaxHealth;
        if (shield >= 100)shield = 100;
        UIVida.value = health;
        UIShield.value = shield;
    }
    public void ActualizarMaxHealth()
    {
        UIVida.maxValue = MaxHealth;
        UIVida.value = MaxHealth;
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
