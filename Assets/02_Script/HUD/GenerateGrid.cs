using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public GameObject slotPrefab;

    public int width = 8;
    public int height = 6;

    void Start()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Instantiate(
                    slotPrefab,
                    transform
                );
            }
        }
    }
}