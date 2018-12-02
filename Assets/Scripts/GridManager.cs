using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public int gridSize = 23;
    public float tileSize;
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
                GameObject tmp = FloorPoolerScript.current.GetPooledObject();
                tmp.SetActive(true);
                tmp.transform.position = new Vector3(x, y, 0);
                Grid[i, j] = tmp;
                GridSpaces[i, j] = 1;
                x += tileSize;
            }
            y += tileSize;
            x = 0;
        }
        for (int i = 4; i < gridSize - 4; i++)
        {
            for (int j = 4; j < gridSize - 4; j++)
            {
                if (!(i == 11 && j == 11))
                {
                    GameObject tmp = BrickPoolerScript.current.GetPooledObject();
                    tmp.SetActive(true);
                    PlaceObject(tmp, i, j);
                }
            }
        }
    }

    public void PlaceObject(GameObject obj, int posX, int posY)
    {
        Vector3 curPos = Grid[posX, posY].transform.position;
        Grid[posX, posY].SetActive(false);
        Grid[posX, posY] = obj;
        Grid[posX, posY].transform.position = curPos;
        GridSpaces[posX, posY] = 0;
    }

    public bool CanPlaceObject(int posX, int posY)
    { 
        return (GridSpaces != null && GridSpaces[posX, posY] == 1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
