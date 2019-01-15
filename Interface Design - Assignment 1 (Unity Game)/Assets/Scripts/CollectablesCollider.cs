using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesCollider : MonoBehaviour {

	CollectablesController cc;

	void Start()
	{
		GameObject ccgo = GameObject.Find ("CollectablesController");
		cc = ccgo.GetComponent<CollectablesController>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (gameObject.name.Contains("Aid Box"))
		{
			Debug.Log ("You've collected a useful aid box!");
		}
		else if (gameObject.name.Contains("Gem"))
		{
			Debug.Log ("You've collected a shiny gem!");
		}
		else if (gameObject.name.Contains("Chicken Leg"))
		{
			Debug.Log ("You've collected a tasty chicken leg!");
		}
		else if (gameObject.name.Contains("PlaceCave"))
		{
			Debug.Log ("You've discovered the mushroom cave!");
		}
		else if (gameObject.name.Contains("PlacePark"))
		{
			Debug.Log ("You've discovered the park!");
		}
		else if (gameObject.name.Contains("PlaceGrandTree"))
		{
			Debug.Log ("You've discovered the grand tree!");
		}
		else if (gameObject.name.Contains("PlaceVillage"))
		{
			Debug.Log ("You've discovered the village!");
		}
		else if (gameObject.name.Contains("PlaceCamp"))
		{
			Debug.Log ("You've discovered the camp!");
		}

		cc.IncrementCount (gameObject);
		Destroy (gameObject);
	}
}
