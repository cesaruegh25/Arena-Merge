using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoGolpe : MonoBehaviour
{
    [Header("Configuración del Efecto")]
    [SerializeField] private Color colorGolpe = Color.red;
    [SerializeField] private float duracionAlerta = 0.15f;

    // Listas para almacenar los componentes y sus colores originales
    public SpriteRenderer[] listaSprites;
    private Coroutine corrutinaGolpe;

    // Este es el método público que debes llamar desde tu script de salud/daño
    public void RecibirGolpe()
    {
        // Si ya nos estaban golpeando, paramos el efecto anterior para que no falle el color
        if (corrutinaGolpe != null)
        {
            StopCoroutine(corrutinaGolpe);
        }

        // Iniciamos el efecto visual de parpadeo
        corrutinaGolpe = StartCoroutine(RutinaColorGolpe());
    }

    private IEnumerator RutinaColorGolpe()
    {
        // 1. Cambiamos todas las piezas al color de golpe (Rojo)
        for (int i = 0; i < listaSprites.Length; i++)
        {
            if (listaSprites[i] != null)
            {
                listaSprites[i].color = colorGolpe;
            }
        }

        // 2. Esperamos el tiempo que le hayamos asignado
        yield return new WaitForSeconds(duracionAlerta);

        // 3. Devolvemos cada pieza a su color original de forma exacta
        for (int i = 0; i < listaSprites.Length; i++)
        {
            if (listaSprites[i] != null)
            {
                listaSprites[i].color = Color.white;
            }
        }

        corrutinaGolpe = null;
    }
}