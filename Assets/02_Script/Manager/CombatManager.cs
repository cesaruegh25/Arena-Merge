using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Transform itemsContainer;

    public void StartCombat()
    {
        foreach (Transform child in itemsContainer)
        {
            ItemUI item =
                child.GetComponent<ItemUI>();

            if (item == null)
                continue;

            GameObject obj =
                new GameObject(item.data.itemName);

            WeaponController wc =
                obj.AddComponent<WeaponController>();

            wc.data = item.data;
        }
    }
}