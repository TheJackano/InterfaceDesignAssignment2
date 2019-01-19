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

    public GameObject CanvasHighscore;
    public GameObject CanvasMainMenu;

    public static int CurrentPlayersFinalScore;
    public static bool finishedLevel = false;

    List<HighscoreData> hd = new List<HighscoreData>();

    public Text FirstPlaceUsername, SecondPlaceUsername;
    public Text FirstPlaceScore, SecondPlaceScore;

    public Text PlayersUsernameTextField, PlayersScoreTextField;

    public void Update()
    {
        if (finishedLevel == true)
        {
            CanvasHighscore.SetActive(true);
            CanvasMainMenu.SetActive(false);
        }
    }

    public void SetUsername()
    {
        PlayerData.Username = InputFieldUsername.text;
    }
    public void PlayGame()
    {
        if (goCharacterOneSelected.activeSelf == true && PlayerData.Username != null)
        {
            SceneManager.LoadScene(1);
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
        AddPlayerToList();
        BubbleSortingScores();
        SaveData();
        UpdateDisplayinScene();
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
            hd = (List<HighscoreData>)bf.Deserialize(fs);
            fs.Close();
            Debug.Log("Loaded Data");
        }
        else
        {
            Debug.LogError("The file you are trying to load is missing!");
        }
    }
    public void AddPlayerToList()
    {
        hd.Add(new HighscoreData(PlayerData.Username, CurrentPlayersFinalScore));
    }
    public void BubbleSortingScores()
    {
        for (int i = 0; i < hd.Count; i++)
        {
            for (int j = 0; j < hd.Count - i - 1; j++)
            {
                if (hd[j].Score < hd[j + 1].Score) //If the data in J is less than the Data in J + 1
                {
                    List<HighscoreData> temp = new List<HighscoreData>(); //Create a temp data storage to swap the information 
                    temp[0].Score = hd[j + 1].Score; //Here you will copy of the of the data from [j+1]
                    hd[j + 1].Score = hd[j].Score;
                    hd[j].Score = temp[0].Score;
                }
            }
        }
    }
    public void UpdateDisplayinScene()
    {
        PlayersUsernameTextField.text = PlayerData.Username;
        PlayersScoreTextField.text = CurrentPlayersFinalScore.ToString();

        FirstPlaceUsername.text = hd[0].Username;
        SecondPlaceUsername.text = hd[1].Username;

        FirstPlaceScore.text = hd[0].Score.ToString();
        SecondPlaceScore.text = hd[1].Score.ToString();

}

    /*
    As you can see, what the purpose of the algorithm is to check

which data is bigger, j or j+1. If they are the same or j is bigger than j+1

then nothing happens.This then repeats.It goes through each element in the array.



Some key elements of the algorithm:

in the second for loop - j<j<cd.length - i - 1 - The reason why we do this 

is because after the first iteration of loops, the smallest value will always be at the end.

So we only need to go through 1 fewer times.This is why we subtract i;



    Second thing is the actual swapping.Now I had to guess because I dont know exactly what you store in 

each element of the array.However, I used data as the main value.Im guessing it stores an int which is the best

cause it is easy to tell which is bigger.You will need to transfer ALL data stored in each element.any strings,

floats, ints.What ever, they need to be swapped and that is done through the temp object created. It stores


one set of data so that it is not deleted when it is copied.



I hope this makes sense and it is the easiest way to get the biggest value to the front with ease.



Topics that are used in this if you need to quickly revise them. 

 - arrays

 - bubble sort algorithm

 - for loops + nested for loops
 */
    }

