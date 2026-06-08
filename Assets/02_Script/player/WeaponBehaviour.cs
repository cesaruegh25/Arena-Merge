using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponBehaviour : MonoBehaviour
{
    public ItemData data;

    Transform player;

    float timer;

    move movement;
    // espada
    private Vector3 swordDirection;
    private Vector3 dir;
    private bool isAttacking = false;
    // Lanza
    private float rotationSpeed = 180f;
    private bool clockwise = false;
    private float angle;
    public float startAngle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
        .transform;
        movement = player.GetComponent<move>();
        if (data.itemName == "Pan")
        {
            MaxHealEffect();
        }
        if (data.itemName == "Lanza")
        {
            angle =
                startAngle * Mathf.Deg2Rad;
        }
        if (data.itemName == "Espada")
        {
            float rad =
                startAngle * Mathf.Deg2Rad;

            swordDirection =
                new Vector3(
                    Mathf.Cos(rad),
                    Mathf.Sin(rad),
                    0
                ).normalized;
        }
    }

    void Update()
    {
        if (data.itemName == "Espada" && !isAttacking)
        {
            dir = movement.lastDirection;
            transform.position = player.position + swordDirection * 1f;

            float angle = Mathf.Atan2(
                        swordDirection.y,
                        swordDirection.x
                        ) * Mathf.Rad2Deg;

            transform.rotation =
                Quaternion.Euler(0, 0, angle - 90f);
        }
        if (data.itemName == "Lanza")
        {
            dir = movement.lastDirection;
            SpearAttack();
        }

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

            case "Escudo":
                ShieldEffect();
                break;

            case "Manzana":
                HealEffect();
                break;
        }
    }


    IEnumerator SwordAttack()
    {
        isAttacking = true;
        gameObject.GetComponent<Collider2D>().enabled = true;

        Vector3 startPos = player.position + swordDirection * 1f;

        Vector3 endPos = player.position + swordDirection * data.attackRange;

        float duration = 0.15f;
        float t = 0;

        // avanzar
        while (t < duration)
        {
            t += Time.deltaTime;

            transform.position =
                Vector3.Lerp(
                    startPos,
                    endPos,
                    t / duration
                );
            yield return null;
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
        isAttacking = false;
    }

    public void SpearAttack()
    {
        // girar
        float dir =
            clockwise ? -1f : 1f;

        angle +=
            rotationSpeed *
            Mathf.Deg2Rad *
            Time.deltaTime *
            dir;

        // posición alrededor del jugador
        Vector2 orbitPos =
            new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * data.attackRange;

        transform.position =
            (Vector2)player.position +
            orbitPos;

        // rotación visual del arma
        float rot =
            angle * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(
                0,
                0,
                rot -180f
            );
    }

    public void ShieldEffect()
    {
        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = true;
        if (GameManager.Instance.shield < 100)
        {
            GameManager.Instance.shield += data.shield;
            GameManager.Instance.ActualizarUI();
        }
        else
        {
            Debug.Log("escudo al 100" + "WeaponBehaviour 177");
        }

        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = false;
    }

    public void HealEffect()
    {
        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = true;
        if (GameManager.Instance.MaxHealth > GameManager.Instance.health) 
        {
            GameManager.Instance.health += data.heal;
            GameManager.Instance.ActualizarUI();
        }
        else
        {
            GameManager.Instance.ActualizarUI();
            Debug.Log("Vida al maximo" + "WeaponBehaviour 194");
        }

        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = false;
    }
    private void MaxHealEffect()
    {
        GameManager.Instance.MaxHealth += data.heal;
        GameManager.Instance.ActualizarMaxHealth();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // dañar enemigo
        if (collision.CompareTag("Enemy"))
        {
            moveEnemy enemy =
                collision.GetComponent<moveEnemy>();

            Rigidbody2D rb =
                collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction =
                    (collision.transform.position -
                     transform.position).normalized;

                rb.linearVelocity = Vector2.zero;
                if (enemy.data.enemyType == EnemyType.Elite)
                {
                    rb.AddForce(
                        direction * (data.alejar/2f),
                        ForceMode2D.Impulse
                    );
                }
                if (enemy.data.enemyType == EnemyType.Basic)
                {
                    rb.AddForce(
                        direction * data.alejar,
                        ForceMode2D.Impulse
                    );
                }
            }
            if (enemy != null)
            {
                //Debug.Log("Enemigo golpeado" + collision.name + "WeaponBehaviour 239");
                enemy.TakeDamage(10);
            }
        }
    }
}