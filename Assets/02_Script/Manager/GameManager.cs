using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public int health;
    public int shield;

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
        score = 0;
        health = 100;
        shield = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Time.timeScale = 0; // Detiene el juego
        }
    }
    public void GameBegin()
    {
        isGame = true;
    }
}
