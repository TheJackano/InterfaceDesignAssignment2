using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuController : MonoBehaviour
{
    public AudioSource musicSource;
    private bool isCharacterSelected;
    public GameObject goCharacterOneSelected;
    public InputField InputFieldUsername;

    public HighscoreData[] hd;

    public void SetUsername()
    {
        PlayerData.Username = InputFieldUsername.text;
        Debug.Log(PlayerData.Username);
    }
    public void PlayGame()
    {
        if (goCharacterOneSelected.activeSelf == true && PlayerData.Username != null)
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
    public void Highscore()
    {
        LoadData();
        //Add current players details to list
        //Sort list
        SaveData();
        //Display list on screen
    }
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/gameData.dat");
        bf.Serialize(fs, hd);
        fs.Close();
        Debug.Log("Saved Data");
    }
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            hd = (HighscoreData[])bf.Deserialize(fs);
            fs.Close();
            Debug.Log("Loaded Data");
        }
        else
        {
            Debug.LogError("The file you are trying to load is missing!");
        }
    }
}
