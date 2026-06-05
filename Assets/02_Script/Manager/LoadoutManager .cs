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
            }
            else
            {
                obj = Instantiate(
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
            }
            WeaponBehaviour wb =
                obj.GetComponent<WeaponBehaviour>();

            wb.data = item;
        }
    }
}