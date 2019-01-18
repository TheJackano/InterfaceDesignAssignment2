using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesCollider : MonoBehaviour {

	CollectablesController cc;

	void Start()
	{
		GameObject ccgo = GameObject.Find ("_CollectablesController");
		cc = ccgo.GetComponent<CollectablesController>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (gameObject.tag == "Collectable")
		{
            cc.IncrementCount(gameObject);
        }
		else if(gameObject.tag == "Place")
        {
            cc.AddPlaceToList(gameObject);
        }
        if (gameObject.name.Contains("Special Axe"))
        {
            cc.SpecialItem(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
