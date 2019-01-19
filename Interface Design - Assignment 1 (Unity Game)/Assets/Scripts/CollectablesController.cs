using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectablesController : MonoBehaviour
{

    public CollectablesData[] cd;
    public PlacesData[] pd;

    public GameObject prefabPlacesList;
    public GameObject content;
    public GameObject addedItem;

    public Animator sideMenuAnim;
    public bool sideMenuIsShowing = false;

    public Text HealthPackTextField, GemsTextField, FoodTextField, PointsTextField;

    public Image healthBar;

    public GameObject PopUpCanvas;
    private bool canPopUpBeTriggered = true;

    private int placesVisted, itemsCollected;
    private int Points;
    private int AmountOfPointsToAdd;

    private float currentTime;
    private float whenInteractableTime;

    private GameObject SpecialAxe;

    public static bool isGamePaused = false;

    public Text PickUpResult;
    /*
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    */
    void Update()
    {
        UpdateHealthBar();
        UpdatePoints();
        currentTime = Time.time;

        if ((whenInteractableTime < currentTime) && (canPopUpBeTriggered == false)) canPopUpBeTriggered = true;
    }

    void Start()
    {
        sideMenuAnim.enabled = false;
        PlayerController.StepsTaken = 0;
    }
    public void IncrementCount(GameObject go)
    {
        if (go.name.Contains("Aid Box"))
        {
            cd[0].CollectablesNumber++;
            HealthPackTextField.text = cd[0].CollectablesNumber.ToString();

        }
        else if (go.name.Contains("Gem"))
        {
            cd[1].CollectablesNumber++;
            GemsTextField.text = cd[1].CollectablesNumber.ToString();
        }
        else if (go.name.Contains("Chicken Leg"))
        {
            cd[2].CollectablesNumber++;
            FoodTextField.text = cd[2].CollectablesNumber.ToString();
        }
        itemsCollected += 1;
    }
    public void AddPlaceToList(GameObject go)
    {
        prefabPlacesList = Instantiate(prefabPlacesList);
        prefabPlacesList.transform.SetParent(content.transform);
        prefabPlacesList.transform.localPosition = Vector3.zero;
        prefabPlacesList.transform.localScale = Vector3.one;

        if (go.name.Contains("PlaceCave"))
        {
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[0].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[0].placeDescription;

        }
        else if (go.name.Contains("PlacePark"))
        {
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[1].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[1].placeDescription;
        }
        else if (go.name.Contains("PlaceGrandTree"))
        {
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[2].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[2].placeDescription;
        }
        else if (go.name.Contains("PlaceVillage"))
        {
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[3].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[3].placeDescription;
        }
        else if (go.name.Contains("PlaceCamp"))
        {
            prefabPlacesList.transform.Find("Image").GetComponent<Image>().sprite = pd[4].placeImage;
            prefabPlacesList.GetComponentInChildren<Text>().text = pd[4].placeDescription;
        }
        placesVisted += 1;
        ShowSideMenu();
    }
    public void ShowSideMenu()
    {
        sideMenuAnim.enabled = true;
        sideMenuAnim.Play("sideMenuShow");
    }
    public void UpdateHealthBar()
    {
        healthBar.fillAmount -= 0.05f * Time.deltaTime;
        if (healthBar.fillAmount <= 0)
        {
            MenuController.CurrentPlayersFinalScore = Points;
            MenuController.finishedLevel = true;
            SceneManager.LoadScene(0);
        }
    }
    public void UpdatePoints()
    {
        Points = (int)(itemsCollected * 10 + placesVisted * 100 - PlayerController.StepsTaken + AmountOfPointsToAdd);
        if (Points <= 0) Points = 0;
        PointsTextField.text = Points.ToString();
    }
    public void SpecialItemColision(GameObject go)
    {
        SpecialAxe = go;
        if (canPopUpBeTriggered == true)
        {
            
            Time.timeScale = 0;
            isGamePaused = true;
            PopUpCanvas.SetActive(true);

        }
    }
    public void ClosePopUp()
    {
        canPopUpBeTriggered = false;
        Time.timeScale = 1;
        isGamePaused = false;
        whenInteractableTime = currentTime + 5;
    }
    public void PickUp()
    {
        AmountOfPointsToAdd = (int)(Random.Range(-200.0f, 200.0f));
        if (AmountOfPointsToAdd > 0)
        {
            PickUpResult.text = ("You won " + AmountOfPointsToAdd + " points.").ToString();
        }
        else if (AmountOfPointsToAdd < 0)
        {
            PickUpResult.text = ("You lost " + AmountOfPointsToAdd + " points.").ToString();
        }
        else
        {
            PickUpResult.text = "You neither won or lost any points.";
        }
        Destroy(SpecialAxe);
    }
}
