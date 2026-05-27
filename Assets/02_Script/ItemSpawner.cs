using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    public ItemData[] possibleItems;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj =
            Instantiate(
            itemPrefab,
            transform);

            ItemUI ui =
            obj.GetComponent<ItemUI>();

            ui.data =
            possibleItems[
            Random.Range(
            0,
            possibleItems.Length)];
        }
    }
}