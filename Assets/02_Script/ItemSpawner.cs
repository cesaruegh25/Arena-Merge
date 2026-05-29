using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    public List<ItemData> possibleItems;

    public int amount = 5;


    void Start()
    {
        SpawnItems();
    }
    public void SpawnItems()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj =
            Instantiate(
                itemPrefab,
                transform
            );

            ItemData randomItem =
            possibleItems[
                Random.Range(
                    0,
                    possibleItems.Count
                )
            ];

            ItemUI ui =
            obj.GetComponent<ItemUI>();

            ui.data = randomItem;
        }
    }

    public void ResetShop()
    {
        // destruir items actuales
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(
                transform.GetChild(i).gameObject
            );
        }

        // generar nuevos
        SpawnItems();
    }
}