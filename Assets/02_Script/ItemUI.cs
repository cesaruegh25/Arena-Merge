using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemData data;

    public int currentX = -1;
    public int currentY = -1;

    void Start()
    {
        Image img =
        GetComponent<Image>();

        img.sprite =
        data.sprite;

        RectTransform rt =
        GetComponent<RectTransform>();

        float size =
        GridManager.Instance.cellSize;

        // tamaño real según slots
        rt.sizeDelta =
        new Vector2(
            data.width * size,
            data.height * size
        );

        // anclar arriba izquierda
        rt.pivot =
        new Vector2(0, 1);
    }
}