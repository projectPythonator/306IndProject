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
        Vector3 curPos = Grid[posY, posX].transform.position;
        Grid[posY, posX].SetActive(false);
        Grid[posY, posX] = obj;
        Grid[posY, posX].transform.position = curPos;
        GridSpaces[posY, posX] = 0;
    }

    public void PlaceBlank (int posX, int posY)
    {
        GameObject tmp = FloorPoolerScript.current.GetPooledObject();
        tmp.SetActive(true);
        tmp.transform.position = new Vector3(tileSize * posX, tileSize * posY, 0);
        Grid[posY, posX] = tmp;
        GridSpaces[posY, posX] = 1;
    }

    public bool InBounds(int pos)
    {
        return (0 <= pos && gridSize > pos);
    }

    public bool CanPlaceObject(int posX, int posY)
    { 
        return (GridSpaces != null && InBounds(posX) && InBounds(posY) && GridSpaces[posY, posX] == 1);
    }

    public void MoveObject(GameObject obj, int cPosX, int cPosY, int dirX, int dirY)
    {
        int curX = cPosX + dirX;
        int curY = cPosY + dirY;
        if (CanPlaceObject(curX, curY))
        {
            PlaceBlank(cPosX, cPosY);
            PlaceObject(obj, curX, curY);
            obj.SendMessage("SetPosX", curX);
            obj.SendMessage("SetPosY", curY);
        }
        else
        {
            //loop ahead to check if there is a open spot in the grid
            curX += dirX;
            curY += dirY;
            while (InBounds(curX) && InBounds(curY))
            {
                if (CanPlaceObject(curX, curY))
                {
                    PlaceBlank(cPosX, cPosY);
                    PlaceObject(obj, cPosX + dirX, cPosY + dirY);
                    Debug.Log(obj.name);
                    obj.SendMessage("SetPosX", cPosX + dirX);
                    obj.SendMessage("SetPosY", cPosY + dirY);

                    GameObject tmp = BrickPoolerScript.current.GetPooledObject();
                    tmp.SetActive(true);
                    PlaceObject(tmp, curX, curY);
                    break;
                }
                curX += dirX;
                curY += dirY;
            }
        }
        

    }
    // Update is called once per frame
    void Update () {
		
	}
}
