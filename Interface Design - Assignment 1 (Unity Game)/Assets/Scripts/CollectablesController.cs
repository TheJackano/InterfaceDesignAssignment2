using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class CollectablesController : MonoBehaviour {

	public CollectablesData[] cd;
    public PlacesData[] pd;

    public GameObject prefabPlacesList;
    public GameObject content;
    public GameObject addedItem;

    public Animator sideMenuAnim;
    public bool sideMenuIsShowing = false;

    public Text HealthPackTextField, GemsTextField, FoodTextField, PointsTextField;

    public Image healthBar;

    private int placesVisted, itemsCollected;
    public int Points;

    void Awake()
	{
		DontDestroyOnLoad (gameObject);
		if(FindObjectsOfType(GetType()).Length >1)
		{
			Destroy (gameObject);
		}
	}
	void Update()
	{
		if (Input.GetKeyDown ("l"))
		{
			Debug.Log ("Loading Data..");
			LoadData();
		}
		else if (Input.GetKeyDown ("s"))
		{
			Debug.Log("Saving Data..");
			SaveData();
		}
        UpdateHealthBar();
        UpdatePoints();
	}

    void Start()
    {
        sideMenuAnim.enabled = false;
    }
	public void IncrementCount(GameObject go)
	{
		if (go.name.Contains("Aid Box"))
		{
			cd [0].CollectablesNumber++;
            HealthPackTextField.text = cd[0].CollectablesNumber.ToString();

        }
		else if (go.name.Contains("Gem"))
		{
			cd [1].CollectablesNumber++;
            GemsTextField.text = cd[1].CollectablesNumber.ToString();
        }
		else if (go.name.Contains("Chicken Leg"))
		{
			cd [2].CollectablesNumber++;
            FoodTextField.text = cd[2].CollectablesNumber.ToString();
        }
        itemsCollected += 1;
    }
	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Create (Application.persistentDataPath + "/gameData.dat");
		bf.Serialize (fs, cd);
		fs.Close ();
		Debug.Log ("Saved Data");
	}
	public void LoadData()
	{
		if(File.Exists(Application.persistentDataPath + "/gameData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open (Application.persistentDataPath + "/gameData.dat", FileMode.Open);
			cd = (CollectablesData[])bf.Deserialize (fs);
			fs.Close ();
			Debug.Log ("Loaded Data");
		}
		else
		{
			Debug.LogError("The file you are trying to load is missing!");
		}
	}
    public void AddPlaceToList(GameObject go)
    {
        prefabPlacesList = Instantiate(prefabPlacesList);
        prefabPlacesList.transform.SetParent(content.transform);
        prefabPlacesList.transform.localPosition = Vector3.zero;
        prefabPlacesList.transform.localScale = Vector3.one;

        if (go.name.Contains("PlaceCave"))
        {
            Debug.Log("You've discovered the mushroom cave!");
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[0].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[0].placeDescription;

        }
        else if (go.name.Contains("PlacePark"))
        {
            Debug.Log("You've discovered the park!");
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[1].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[1].placeDescription;
        }
        else if (go.name.Contains("PlaceGrandTree"))
        {
            Debug.Log("You've discovered the grand tree!");
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[2].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[2].placeDescription;
        }
        else if (go.name.Contains("PlaceVillage"))
        {
            Debug.Log("You've discovered the village!");
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[3].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[3].placeDescription;
        }
        else if (go.name.Contains("PlaceCamp"))
        {
            Debug.Log("You've discovered the camp!");
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[4].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[4].placeDescription;
        }
        placesVisted += 1;
        Debug.Log(placesVisted);
        ShowSideMenu();
    }
    public void ShowSideMenu()
    {
        sideMenuAnim.enabled = true;
        sideMenuAnim.Play("sideMenuShow");
    }
    public void UpdateHealthBar()
    {
        healthBar.fillAmount -= 0.01f * Time.deltaTime;
        if(healthBar.fillAmount <= 0) Debug.Log("Times UP");

    }
    public void UpdatePoints()
    {
        Points = (int)(itemsCollected * 10 + placesVisted * 100 - PlayerController.StepsTaken);
        if (Points <= 0) Points = 0;
        PointsTextField.text = Points.ToString();
    }
}
