using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Image Gameoverimage;

    private void Start()
    {
        if (Gameoverimage == null)
        {
            return;
        }

        instance = this;
        Gameoverimage.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Gameoverimage.gameObject.SetActive(true);
    }
}
