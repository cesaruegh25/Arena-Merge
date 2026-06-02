using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance;

    public List<ItemData> equippedItems =
        new List<ItemData>();

    public Transform player;

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
            GameObject obj =
                Instantiate(
                    item.weaponPrefab,
                    player.position,
                    Quaternion.identity
                );

            obj.transform.SetParent(player);

            WeaponBehaviour wb =
                obj.GetComponent<WeaponBehaviour>();

            wb.data = item;

            //obj.SetActive(false);
        }
    }
}