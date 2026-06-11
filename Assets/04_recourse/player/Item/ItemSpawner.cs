using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    public List<ItemData> possibleItems;

    public int amount = 5;

    public AudioSource audioSource;
    public TextMeshProUGUI UIReset;

    [SerializeField] int intentos;

    void Start()
    {
        SpawnItems();
        if (BattleManager.selectedBattle != null)
        {
            intentos = BattleManager.selectedBattle.intentos;
            UIReset.text = intentos.ToString();
        }
    }
    public void SpawnItems()
    {
        audioSource.Play();
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
        if (intentos > 0)
        {
            intentos--;
            UIReset.text = intentos.ToString();
            // generar nuevos
            SpawnItems();
        }
        else
        {
            Debug.Log("No more attempts left!");
            UIReset.text = "0";
        }
    }
}