using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Onur");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
