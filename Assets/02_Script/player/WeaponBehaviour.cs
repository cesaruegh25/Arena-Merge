using System;
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
        if (data.itemName == "Pan")
        {
            MaxHealEffect();
        }
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
                SwordAttack();
                break;

            case "Lanza":
                SpearAttack();
                break;

            case "Escudo":
                ShieldEffect();
                break;

            case "Manzana":
                HealEffect();
                break;
        }
    }


    public void SwordAttack()
    {
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

        }

    }

    public void SpearAttack()
    {
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

        }

    }

    public void ShieldEffect()
    {
        Debug.Log("entra a la funcion");
        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = true;
        if (GameManager.Instance.shield < 100)
        {
            GameManager.Instance.shield += data.shield;
            Debug.Log("escudo: " + GameManager.Instance.shield);
            GameManager.Instance.ActualizarUI();
        }
        else
        {
            Debug.Log("escudo al 100");
        }

        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = false;
    }

    public void HealEffect()
    {
        //gameObject.GetComponents<SpriteRenderer>()[0].enabled = true;
        if (GameManager.Instance.MaxHealth > GameManager.Instance.health) 
        {
            GameManager.Instance.health += data.heal;
            Debug.Log("vida: " + GameManager.Instance.health);
            GameManager.Instance.ActualizarUI();
        }
        else
        {
            GameManager.Instance.ActualizarUI();
            Debug.Log("Vida al maximo");
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