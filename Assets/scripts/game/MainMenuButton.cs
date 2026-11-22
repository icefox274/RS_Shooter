using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public GameObject HowtoUI;
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ShowHowto()
    {
        HowtoUI.SetActive(true);
    }

    public void CloseHowto()
    {
        HowtoUI.SetActive(false);
    }
}
