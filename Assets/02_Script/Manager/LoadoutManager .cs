using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance;

    public List<ItemData> equippedItems =
        new List<ItemData>();

    public Transform player;

    public GameObject weaponPrefab;
    public GameObject ItemPrefab;
    public GameObject UIItemGameplay;
    void Awake()
    {
        Instance = this;
    }

    public void SaveInventory()
    {
        equippedItems.Clear();

        ItemUI[] items =
            FindObjectsByType<ItemUI>(
                FindObjectsSortMode.None);

        foreach (ItemUI item in items)
        {
            if (item.transform.parent.name ==
                "ItemsContainer")
            {
                equippedItems.Add(item.data);
            }
        }

        SpawnWeapons();

        Debug.Log(
        "Items guardados: " +
        equippedItems.Count);
    }

    void SpawnWeapons()
    {
        // guardar mejor espada y mejor lanza
        ItemData bestSword = null;
        ItemData bestSpear = null;

        foreach (ItemData item in equippedItems)
        {
            GameObject obj;
            if (item.type != ItemType.Weapon)
            {
                obj = Instantiate(
                    ItemPrefab,
                    UIItemGameplay.transform.position,
                    Quaternion.identity,
                    UIItemGameplay.transform
                );
                Image img =
                    obj.GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = item.sprite;
                }
                WeaponBehaviour wb =
                    obj.GetComponent<WeaponBehaviour>();

                wb.data = item;
            }
            if (item.itemName.Contains("Espada"))
            {
                if (bestSword == null ||
                    item.level > bestSword.level)
                {
                    bestSword = item;
                }
            }

            if (item.itemName.Contains("Lanza"))
            {
                if (bestSpear == null ||
                    item.level > bestSpear.level)
                {
                    bestSpear = item;
                }
            }
        }

        // generar solo las mejores
        if (bestSword != null)
        {
            SpawnWeaponByLevel(bestSword);
        }

        if (bestSpear != null)
        {
            SpawnWeaponByLevel(bestSpear);
        }
    }
    void SpawnWeaponByLevel(ItemData item)
    {
        int amount = 1;

        // cantidad según nivel
        switch (item.level)
        {
            case 1:
                amount = 1;
                break;

            case 2:
                amount = 2;
                break;

            case 3:
                amount = 4;
                break;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(
                weaponPrefab,
                player.position,
                Quaternion.identity
            );

            obj.transform.SetParent(player);

            SpriteRenderer sr =
                obj.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                sr.sprite = item.sprite;
            }
            BoxCollider2D bc =
                obj.GetComponent<BoxCollider2D>();
            if (item.itemName == "Espada")
            {
                bc.offset = new Vector2(0f, 1.5f);
            }
            if (item.itemName == "Lanza")
            {
                bc.offset = new Vector2(-4f, 0f);
            }

            WeaponBehaviour wb =
                obj.GetComponent<WeaponBehaviour>();

            wb.data = item;

            // posición orbital
            float angle =
                (360f / amount) * i;

            wb.startAngle = angle;
        }
    }
}