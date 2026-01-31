using UnityEngine;
using UnityEngine.SceneManagement;

public class goBack : MonoBehaviour
{
    public string sceneName = "Menu";

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
