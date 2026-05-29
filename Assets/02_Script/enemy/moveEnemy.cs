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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aquí puedes agregar el código para reducir la vida del jugador o cualquier otra acción que desees realizar al colisionar con el enemigo.
            Debug.Log("¡El enemigo ha colisionado con el jugador!");
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            GameManager.Instance.health -= damage; // Reduce la vida del jugador en 10 (puedes ajustar este valor según tus necesidades)
        }
    }
}
