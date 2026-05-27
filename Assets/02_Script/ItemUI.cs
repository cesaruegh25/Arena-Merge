using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemData data;

    public int currentX = -1;
    public int currentY = -1;

    void Start()
    {
        GetComponent<Image>().sprite = data.sprite;

        RectTransform rt =
        GetComponent<RectTransform>();

        rt.sizeDelta =
        new Vector2(
        data.width * 64,
        data.height * 64);
    }
}