using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public int gridSize = 23;
    public float tileSize = 34.0f/100.0f;
    public GameObject[,] Grid;
    public int[,] GridSpaces;
    // Use this for initialization
    void Start () {
        Grid = new GameObject[gridSize, gridSize];
        GridSpaces = new int[gridSize, gridSize];

        float x = 0, y = 0;


        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject tmp = PoolerScript.current.GetPooledObject();
                tmp.SetActive(true);
                tmp.transform.position = new Vector3(x, y, 0);
                Grid[i, j] = tmp;
                GridSpaces[i, j] = 1;
                x += tileSize;
            }
            y += tileSize;
            x = 0;
        }
    }
	

	// Update is called once per frame
	void Update () {
		
	}
}
