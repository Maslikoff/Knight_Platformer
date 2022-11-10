using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadScane(int Index)
    {
        SceneManager.LoadSceneAsync(Index);
    }

    public void QuitScane()
    {
        Application.Quit();
    }
}
