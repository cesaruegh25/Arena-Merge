using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite sprite;

    public int width = 1;
    public int height = 1;

    public int level = 1;

    public ItemData nextLevelItem;
}