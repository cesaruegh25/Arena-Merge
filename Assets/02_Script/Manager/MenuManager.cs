using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject UIMenu;
    public GameObject UINivelesMenu;

    private void Start()
    {
        OpenMenu();
    }
    public void OpenMenu()
    {
        UIMenu.SetActive(true);
        UINivelesMenu.SetActive(false);
    }
    public void OpenNivelesMenu()
    {
        UIMenu.SetActive(false);
        UINivelesMenu.SetActive(true);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
