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

    public AudioSource pos;
    public AudioSource drop;
    public AudioSource level1;
    public AudioSource level2;
    public AudioSource level3;

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
        pos.Play();
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

    public void OnEndDrag(PointerEventData eventData)
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

        // PRIMERO mirar si hay item debajo
        DraggableItem otherItem =
        GetItemUnderMouse(eventData);

        if (otherItem != null &&
            otherItem != this &&
            otherItem.item.data == item.data)
        {
            MergeWith(otherItem);
            return;
        }
        else
        {
            Debug.Log("No item to merge with, trying to place on grid.");
        }

        // DESPUÉS intentar colocar
        if (GridManager.Instance.CanPlace(item, x, y))
        {
            level1.Play();
            GridManager.Instance.Place(item, x, y);

            transform.SetParent(
            GridManager.Instance.transform
            .Find("ItemsContainer"));

            transform.localScale =
            Vector3.one;

            transform.localPosition =
            GridManager.Instance
            .GetPosition(x, y);
        }
        else
        {
            drop.Play();
            // volver a tienda
            transform.SetParent(tienda);

            transform.localScale =
            Vector3.one;

            item.currentX = -1;
            item.currentY = -1;
        }
    }
    private DraggableItem GetItemUnderMouse(PointerEventData eventData)
    {
        Debug.Log("Raycast para detectar item bajo el mouse" + eventData);
        var results =
            new System.Collections.Generic.List<RaycastResult>();

        UnityEngine.EventSystems.EventSystem.current
            .RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            DraggableItem other =
                result.gameObject
                .GetComponent<DraggableItem>();

            if (other != null &&
                other != this)
            {
                return other;
            }
        }

        return null;
    }

    private void MergeWith(DraggableItem other)
    {
        if (item.data.nextLevelItem == null)
            return;
        if (item.data.level == 1)
        {
            level2.Play();
            Debug.Log("Fusionando nivel 1");
        }
        if (item.data.level == 2)
        {
            level3.Play();
            Debug.Log("Fusionando nivel 2");
        }
        //Debug.Log("Fusionando " + item.data.itemName + " con " + other.item.data.itemName);

        Vector3 pos = other.transform.position;

        GridManager.Instance.Remove(other.item);
        GridManager.Instance.Remove(item);

        Destroy(other.gameObject);
        Destroy(gameObject);

        GameObject newObj = Instantiate(
            GridManager.Instance.itemPrefab,
            pos,
            Quaternion.identity,
            other.transform.parent
        );

        DraggableItem draggable =
            newObj.GetComponent<DraggableItem>();

        draggable.item.data =
            item.data.nextLevelItem;

        UnityEngine.UI.Image img =
            newObj.GetComponent<UnityEngine.UI.Image>();

        img.sprite =
            item.data.nextLevelItem.spriteStore;

        RectTransform rt =
            newObj.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(
            item.data.nextLevelItem.width *
            GridManager.Instance.cellSize,

            item.data.nextLevelItem.height *
            GridManager.Instance.cellSize
        );
    }
}