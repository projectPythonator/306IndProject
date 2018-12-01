using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject tmp = PoolerScript.current.GetPooledObject();

        while(tmp != null)
        {
            tmp.SetActive(true);
            tmp = PoolerScript.current.GetPooledObject();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
