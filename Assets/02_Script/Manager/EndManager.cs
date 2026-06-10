using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI UIScore;
    
    private void Start()
    {
        UIScore.text = "Score: " + BattleManager.score;
    }

    public void volver()
    {
        SceneManager.LoadScene("Menu");
    }
}
