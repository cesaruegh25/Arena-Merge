using UnityEngine;


public enum ItemType
{
    Weapon,
    Heal,
    Shield
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite sprite;

    public int width = 1;
    public int height = 1;

    public int level = 1;

    public ItemData nextLevelItem;

    [Header("Gameplay")]

    public ItemType type;

    public float cooldown = 1f;

    public int damage = 1;

    public int heal = 1;

    public int shield = 1;

    public float attackRange = 2f;
}