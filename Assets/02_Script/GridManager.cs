using UnityEngine;
using static UnityEditor.Progress;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 8;
    public int height = 6;

    public float cellSize = 64;

    bool[,] occupied;

    public GameObject itemPrefab;

    void Awake()
    {
        Instance = this;

        occupied =
        new bool[width, height];
    }

    public bool CanPlace(ItemUI item, int x, int y)
    {
        // si empieza fuera de la mochila
        if (x < 0 || y < 0)
            return false;

        // si el objeto completo se sale
        if (x + item.data.width > width)
            return false;

        if (y + item.data.height > height)
            return false;

        for (int i = 0; i < item.data.width; i++)
        {
            for (int j = 0; j < item.data.height; j++)
            {
                if (occupied[x + i, y + j])
                    return false;
            }
        }

        return true;
    }

    public void Place(ItemUI item, int x, int y)
    {
        for (int i = 0; i < item.data.width; i++)
        {
            for (int j = 0; j < item.data.height; j++)
            {
                occupied[x + i, y + j] = true;
            }
        }

        item.currentX = x;
        item.currentY = y;
    }

    public void Remove(ItemUI item)
    {
        for (int i = 0; i < item.data.width; i++)
        {
            for (int j = 0; j < item.data.height; j++)
            {
                int px = item.currentX + i;
                int py = item.currentY + j;

                if (px >= 0 && py >= 0)
                    occupied[px, py] = false;
            }
        }
    }

    public Vector2 GetPosition(int x, int y)
    {
        RectTransform panel =
        GetComponent<RectTransform>();

        float startX =
        -panel.rect.width / 2;

        float startY =
        panel.rect.height / 2;

        return new Vector2(
            startX + x * cellSize,
            startY - y * cellSize
        );
    }
}