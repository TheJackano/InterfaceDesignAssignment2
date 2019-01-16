using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioSource musicSource;
    public static string Username;
    private bool isCharacterSelected;
    public GameObject goCharacterOneSelected;
    public InputField InputFieldUsername;

    public void SetUsername()
    {
        Username = InputFieldUsername.text;
        Debug.Log(Username);
    }
    public void PlayGame()
    {
        if (goCharacterOneSelected.activeSelf == true && Username != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
        else
            musicSource.Play();
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
