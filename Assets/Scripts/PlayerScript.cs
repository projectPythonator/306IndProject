using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Invoke("PlacePlayer", .1f);
    }

    public void PlacePlayer()
    {
        GameObject GridMan = GameObject.Find("GridManager");
        GridManager script = GridMan.GetComponent<GridManager>();
        while (script.CanPlaceObject(11, 11) == false)
        {
            Debug.Log("waiting");
        }
        Debug.Log(!script.CanPlaceObject(11, 11));
        script.PlaceObject(gameObject, 11, 11);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
