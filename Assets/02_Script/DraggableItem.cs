using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DraggableItem :
MonoBehaviour,
IBeginDragHandler,
IDragHandler,
IEndDragHandler
{
    Vector3 startPos;

    CanvasGroup cg;

    ItemUI item;

    Transform tienda;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();

        item = GetComponent<ItemUI>();
        tienda =
        GameObject.Find("ItemsSpawn")
        .transform;
    }

    public void OnBeginDrag(
    PointerEventData eventData)
    {
        startPos = transform.position;

        cg.blocksRaycasts = false;

        if (item.currentX != -1)
            GridManager.Instance.Remove(item);
    }

    public void OnDrag(
    PointerEventData eventData)
    {
        transform.position =
        Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(
    PointerEventData eventData)
    {
        cg.blocksRaycasts = true;

        RectTransform panel =
        GridManager.Instance
        .GetComponent<RectTransform>();

        Vector2 localPoint;

        RectTransformUtility
        .ScreenPointToLocalPointInRectangle(
        panel,
        Mouse.current.position.ReadValue(),
        null,
        out localPoint);

        float size = GridManager.Instance.cellSize;

        float startX = -panel.rect.width / 2;
        float startY = panel.rect.height / 2;

        int x = Mathf.FloorToInt(
        (localPoint.x - startX) / size);

        int y = Mathf.FloorToInt(
        (startY - localPoint.y) / size);

        if (GridManager.Instance.CanPlace(item, x, y))
        {
            GridManager.Instance.Place(item, x, y);

            // mover el objeto al contenedor de la mochila
            transform.SetParent(
            GridManager.Instance.transform.Find("ItemsContainer")
            );

            // mantener escala correcta
            transform.localScale = Vector3.one;

            // colocarlo dentro de la mochila
            transform.localPosition =
            GridManager.Instance.GetPosition(x, y);
        }
        else
        {
            // mover a la tienda
            transform.SetParent(
            tienda
            );

            transform.localScale =
            Vector3.one;

            item.currentX = -1;
            item.currentY = -1;
        }
        Debug.Log("x:" + x + " y:" + y);
    }
}