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

    public Text PlayersPositionTextField, PlayersUsernameTextField, PlayersScoreTextField;

    private int whereOnScoreboardNumber;
    private GameObject HighScorePanelToHighlightGameObject;
    public GameObject CurrentPlayerPanel;
    public Sprite Highlighted;

    public void Update()
    {
        if (finishedLevel == true)
        {
            CanvasHighscore.SetActive(true);
            CanvasMainMenu.SetActive(false);
            Highscore();
            finishedLevel = false;
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
        for (int i = 0; i < hd.Count; i++)
        {
            if (hd[i].Username == PlayerData.Username && hd[i].Score == CurrentPlayersFinalScore)
            {
                Debug.Log(hd[i].Username + hd[i].Score);
                whereOnScoreboardNumber = i;
            }
        }
        HighScorePanelToHighlightGameObject = GameObject.Find("Panel_PlayersPlace");
        if (hd.Count >= 1)
        {
            FirstPlaceUsername.text = hd[0].Username;
            FirstPlaceScore.text = (hd[0].Score).ToString();
            if (whereOnScoreboardNumber == 0) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_1stPlace");
        }
        if (hd.Count >= 2)
        {
            SecondPlaceUsername.text = hd[1].Username;
            SecondPlaceScore.text = (hd[1].Score).ToString();
            if (whereOnScoreboardNumber == 1) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_2ndPlace");
        }
        if (hd.Count >= 3)
        {
            ThirdPlaceUsername.text = hd[2].Username;
            ThirdPlaceScore.text = (hd[2].Score).ToString();
            if (whereOnScoreboardNumber == 2) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_3rdPlace");
        }
        if (hd.Count >= 4)
        {
            FourthPlaceUsername.text = hd[3].Username;
            FourthPlaceScore.text = (hd[3].Score).ToString();
            if (whereOnScoreboardNumber == 3) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_4thPlace");
        }
        if (hd.Count >= 5)
        {
            FifthPlaceUsername.text = hd[4].Username;
            FifthPlaceScore.text = (hd[4].Score).ToString();
            if (whereOnScoreboardNumber == 4) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_5thPlace");
        }
        if (hd.Count >= 6)
        {
            SixthPlaceUsername.text = hd[5].Username;
            SixthPlaceScore.text = (hd[5].Score).ToString();
            if (whereOnScoreboardNumber == 5) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_6thPlace");
        }
        if (hd.Count >= 7)
        {
            SeventhPlaceUsername.text = hd[6].Username;
            SeventhPlaceScore.text = (hd[6].Score).ToString();
            if (whereOnScoreboardNumber == 6) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_7thPlace");
        }
        if (hd.Count >= 8)
        {
            EighthPlaceUsername.text = hd[7].Username;
            EighthPlaceScore.text = (hd[7].Score).ToString();
            if (whereOnScoreboardNumber == 7) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_8thPlace");
        }
        if (hd.Count >= 9)
        {
            NinthPlaceUsername.text = hd[8].Username;
            NinthPlaceScore.text = (hd[8].Score).ToString();
            if (whereOnScoreboardNumber == 8) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_9thPlace");
        }
        if (hd.Count >= 10)
        {
            TenthPlaceUsername.text = hd[9].Username;
            TenthPlaceScore.text = (hd[9].Score).ToString();
            if (whereOnScoreboardNumber == 9) HighScorePanelToHighlightGameObject = GameObject.Find("Panel_10thPlace");
        }

        if (finishedLevel == true)
        {
            Image HighScorePanelToHighlight = HighScorePanelToHighlightGameObject.GetComponent<Image>();
            HighScorePanelToHighlight.sprite = Highlighted;
        }


        PlayersPositionTextField.text = (whereOnScoreboardNumber + 1).ToString();
    }
}

