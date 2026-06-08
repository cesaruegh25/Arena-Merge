using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public ItemData data;

    float timer;

    PlayerStats player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= data.cooldown)
        {
            timer = 0;

            UseItem();
        }
    }

    void UseItem()
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
        GameObject slash = new GameObject("SwordSlash");

        slash.transform.position =
            player.transform.position + Vector3.up;

        Image img = slash.AddComponent<Image>();

        img.sprite = data.spriteBattle;

        RectTransform rt =
            slash.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(100, 100);

        float duration = 0.2f;

        Vector3 startRot = new Vector3(0, 0, -90);
        Vector3 endRot = new Vector3(0, 0, 90);

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            slash.transform.eulerAngles =
                Vector3.Lerp(startRot, endRot, t / duration);

            yield return null;
        }

        DamageEnemies();

        Destroy(slash);
    }

    IEnumerator SpearAttack()
    {
        GameObject spear = new GameObject("Spear");

        spear.transform.position =
            player.transform.position;

        Image img = spear.AddComponent<Image>();

        img.sprite = data.spriteBattle;

        RectTransform rt =
            spear.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(180, 60);

        Vector3 start =
            player.transform.position;

        Vector3 end =
            player.transform.position +
            Vector3.right * data.attackRange;

        float duration = 0.15f;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            spear.transform.position =
                Vector3.Lerp(start, end, t / duration);

            yield return null;
        }

        DamageEnemies();

        Destroy(spear);
    }

    IEnumerator ShieldEffect()
    {
        player.AddShield(data.shield);

        GameObject shield = new GameObject("Shield");

        shield.transform.position =
            player.transform.position;

        Image img = shield.AddComponent<Image>();

        img.sprite = data.spriteBattle;

        RectTransform rt =
            shield.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(120, 120);

        yield return new WaitForSeconds(0.5f);

        Destroy(shield);
    }

    IEnumerator HealEffect()
    {
        player.Heal(data.heal);

        GameObject heal = new GameObject("Heal");

        heal.transform.position =
            player.transform.position;

        Image img = heal.AddComponent<Image>();

        img.sprite = data.spriteBattle;

        RectTransform rt =
            heal.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(80, 80);

        yield return new WaitForSeconds(0.5f);

        Destroy(heal);
    }

    void DamageEnemies()
    {
        Enemy enemy =
            FindObjectOfType<Enemy>();

        if (enemy == null)
            return;

        float dist =
            Vector2.Distance(
                player.transform.position,
                enemy.transform.position);

        if (dist <= data.attackRange)
        {
            enemy.TakeDamage(data.damage);
        }
    }
}