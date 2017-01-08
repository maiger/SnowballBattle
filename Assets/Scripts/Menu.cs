using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public string level1;
    public string level2;

    // TODO: Compact this to a single function
    public void startLevel1()
    {
        SceneManager.LoadScene(level1);
    }

    public void startLevel2()
    {
        SceneManager.LoadScene(level2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
