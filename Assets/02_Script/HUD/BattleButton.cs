using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleButton : MonoBehaviour
{
    public BattleConfig battleConfig;

    public void StartBattle()
    {
        BattleManager.selectedBattle = battleConfig;
        SceneManager.LoadScene("Juego");
    }
}