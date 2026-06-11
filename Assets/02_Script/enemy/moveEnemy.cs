using UnityEngine;
using UnityEngine.UI;

public class moveEnemy : MonoBehaviour
{
    public Slider UIVida;
    public RectTransform healthBar;
    private GameObject player;
    public EnemyData data;
    private int currentHealth;

    public AudioSource hurt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = data.vida;

        SpriteRenderer sr =
            GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            sr.sprite = data.sprite;
        }

        AudioSource audioSource =
            GetComponent<AudioSource>();
        if (audioSource != null) audioSource.clip = data.hurt;
        hurt = audioSource;

        UIVida.maxValue = currentHealth;
        UIVida.value = currentHealth;
        if (data.enemyType == EnemyType.Basic)
        {
            healthBar.anchoredPosition = new Vector2(25f, 300f);
        }
        if (data.enemyType == EnemyType.Elite)
        {
            healthBar.anchoredPosition = new Vector2(45f, 300f);
        }
        if (data.enemyType == EnemyType.Boss)
        {
            healthBar.anchoredPosition = new Vector2(45f, 382f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGame)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                player.transform.position, 
                data.velocidad);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UIVida.value = currentHealth;
        if (currentHealth <= 0)
        {
            GameManager.Instance.Score(data.scoreValue);
            EnemySpawner.Instance.EnemyDied();
            Destroy(gameObject);
        }
    } 

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.PlayerIsDamage(data.damage);
            //Debug.Log("¡El enemigo ha colisionado con el jugador!" + GameManager.Instance.health + "moveEnemy 57");
            GameManager.Instance.ActualizarUI();
            // EMPUJE HACIA ATRÁS
            Rigidbody2D rb =
                collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction =
                    (collision.transform.position -
                     transform.position).normalized;

                rb.linearVelocity = Vector2.zero;

                rb.AddForce(
                    direction * data.KnockBackForce,
                    ForceMode2D.Impulse
                );
            }
            collision.gameObject.GetComponent<Animator>().SetTrigger("hurt");
            EfectoGolpe efectoGolpe = collision.gameObject.GetComponent<EfectoGolpe>();
            if (efectoGolpe != null)
            {
                efectoGolpe.RecibirGolpe();
            }
        }
    }
}
