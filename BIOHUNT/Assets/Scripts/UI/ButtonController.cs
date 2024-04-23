using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonController : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ExitGameButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void SettingButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
