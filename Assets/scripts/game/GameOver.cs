using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void Reload()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }
    public void Returnmenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
