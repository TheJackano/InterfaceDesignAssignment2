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

    public Text FirstPlaceUsername, SecondPlaceUsername, ThirdPlaceUsername, FourthPlaceUsername, FifthPlaceUsername, SixthPlaceUsername, SeventhPlaceUsername , EighthPlaceUsername, NinthPlaceUsername, TenthPlaceUsername;
    public Text FirstPlaceScore, SecondPlaceScore, ThirdPlaceScore, FourthPlaceScore, FifthPlaceScore, SixthPlaceScore, SeventhPlaceScore, EighthPlaceScore, NinthPlaceScore, TenthPlaceScore;

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
    public void RetryGame()
    {
        SceneManager.LoadScene(1);
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
        Debug.Log("Username :" + hd[hd.Count- 1].Username + "Score: : " + hd[hd.Count-1].Score);
        Debug.Log("Size of list: " + hd.Count);
        Debug.Log("Player Added to List");
    }
    public void BubbleSortingScores()
    {
        for (int i = 0; i < hd.Count; i++)
        {
            for (int j = 0; j < hd.Count - i - 1; j++)
            {
                if (hd[j].Score < hd[j + 1].Score)
                {
                    List<HighscoreData> temp = new List<HighscoreData>();
                    if (temp.Count == 0)temp.Add(new HighscoreData("temp", (int)0));
                    temp[0] = hd[j + 1];
                    hd[j + 1] = hd[j];
                    hd[j] = temp[0];
                }
            }
        }
    }
    public void UpdateDisplayinScene()
    {
        PlayersUsernameTextField.text = PlayerData.Username;
        PlayersScoreTextField.text = CurrentPlayersFinalScore.ToString();

        if(hd.Count >= 1)
        {
            FirstPlaceUsername.text = hd[0].Username;
            FirstPlaceScore.text = (hd[0].Score).ToString();
        }
        if (hd.Count >= 2)
        {
            SecondPlaceUsername.text = hd[1].Username;
            SecondPlaceScore.text = (hd[1].Score).ToString();
        }
        if (hd.Count >= 3)
        {
            ThirdPlaceUsername.text = hd[2].Username;
            ThirdPlaceScore.text = (hd[2].Score).ToString();
        }
        if (hd.Count >= 4)
        {
            FourthPlaceUsername.text = hd[3].Username;
            FourthPlaceScore.text = (hd[3].Score).ToString();
        }
        if (hd.Count >= 5)
        {
            FifthPlaceUsername.text = hd[4].Username;
            FifthPlaceScore.text = (hd[4].Score).ToString();
        }
        if (hd.Count >= 6)
        {
            SixthPlaceUsername.text = hd[5].Username;
            SixthPlaceScore.text = (hd[5].Score).ToString();
        }
        if (hd.Count >= 7)
        {
            SeventhPlaceUsername.text = hd[6].Username;
            SeventhPlaceScore.text = (hd[6].Score).ToString();
        }
        if (hd.Count >= 8)
        {
            EighthPlaceUsername.text = hd[7].Username;
            EighthPlaceScore.text = (hd[7].Score).ToString();
        }
        if (hd.Count >= 9)
        {
            NinthPlaceUsername.text = hd[8].Username;
            NinthPlaceScore.text = (hd[8].Score).ToString();
        }
        if (hd.Count >= 10)
        {
            TenthPlaceUsername.text = hd[9].Username;
            TenthPlaceScore.text = (hd[9].Score).ToString();
        }
    }
}

