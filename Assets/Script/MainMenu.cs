using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void goToLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void goToLevel3()
    {
        SceneManager.LoadScene(3);
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
