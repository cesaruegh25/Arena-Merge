using UnityEngine;

public class moveEnemy : MonoBehaviour
{

    private GameObject player;
    public float speed = 0.01f;
    public int health = 100;
    public int damage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGame)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.health -= damage;
            Debug.Log("¡El enemigo ha colisionado con el jugador!" + GameManager.Instance.health);
            GameManager.Instance.ActualizarUI(); 
        }
    }
}
