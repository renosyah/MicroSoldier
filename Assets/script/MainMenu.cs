
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

public void PlayNow(string scheneName)
    {
        SceneManager.LoadScene(scheneName);
       
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
