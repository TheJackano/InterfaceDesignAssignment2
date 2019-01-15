using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CollectablesController : MonoBehaviour {

	public CollectablesData[] cd;

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
	}

	public void IncrementCount(GameObject go)
	{
		if (go.name.Contains("Aid Box"))
		{
			cd [0].CollectablesNumber++;
		}
		else if (go.name.Contains("Gem"))
		{
			cd [1].CollectablesNumber++;
		}
		else if (go.name.Contains("Chicken Leg"))
		{
			cd [2].CollectablesNumber++;
		}
		OutputCounts ();
	}

	void OutputCounts()
	{
		Debug.Log ("Statistics");
		Debug.Log ("Aid Box: " + cd [0].CollectablesNumber);
		Debug.Log ("Gems: " + cd [1].CollectablesNumber);
		Debug.Log ("Chicken Leg: " + cd [2].CollectablesNumber);

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
}
