using System.Collections;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public ItemData data;

    Transform player;

    float timer;

    move movement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
        .transform;
        movement = player.GetComponent<move>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= data.cooldown)
        {
            timer = 0;

            UseWeapon();
        }
    }

    void UseWeapon()
    {
        switch (data.itemName)
        {
            case "Espada":
                StartCoroutine(SwordAttack());
                break;

            case "Lanza":
                StartCoroutine(SpearAttack());
                break;

            case "Escudo":
                StartCoroutine(ShieldEffect());
                break;

            case "Manzana":
                StartCoroutine(HealEffect());
                break;
        }
    }

    IEnumerator SwordAttack()
    {
        //gameObject.SetActive(true);

        Vector3 dir =
            movement.lastDirection;

        if (dir == Vector3.zero)
            dir = Vector3.right;

        // colocar delante
        transform.position =
            player.position +
            dir * 1f;

        // rotación según dirección
        float angle =
            Mathf.Atan2(dir.y, dir.x) *
            Mathf.Rad2Deg;

        float duration = 0.15f;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            transform.rotation =
                Quaternion.Euler(
                    0,
                    0,
                    angle +
                    Mathf.Lerp(-90, 90, t / duration)
                );

            yield return null;
        }

        //gameObject.SetActive(false);
    }

    IEnumerator SpearAttack()
    {
        //gameObject.SetActive(true);

        Vector3 dir =
            movement.lastDirection;

        if (dir == Vector3.zero)
            dir = Vector3.right;

        Vector3 start =
            player.position +
            dir * 0.5f;

        Vector3 end =
            player.position +
            dir * data.attackRange;

        transform.rotation =
            Quaternion.Euler(
                0,
                0,
                Mathf.Atan2(dir.y, dir.x) *
                Mathf.Rad2Deg
            );

        float t = 0;

        while (t < 0.15f)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(
                    start,
                    end,
                    t / 0.15f
                );

            yield return null;
        }

        //gameObject.SetActive(false);
    }

    IEnumerator ShieldEffect()
    {
        gameObject.SetActive(true);

        transform.position =
            player.position;

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    IEnumerator HealEffect()
    {
        gameObject.SetActive(true);

        transform.position =
            player.position;

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ignorar player
        if (collision.CompareTag("Player"))
            return;

        // dañar enemigo
        if (collision.CompareTag("Enemy"))
        {
            moveEnemy enemy =
                collision.GetComponent<moveEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }
    }
}