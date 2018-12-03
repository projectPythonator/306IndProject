using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour {

    Vector3 nil = new Vector3(-1, -1, -1);
    int[] yPos = { 1, 1, 0, -1, -1, -1, 0, 1 };
    int[] xPos = { 0, 1, 1, 1, 0, -1, -1, -1 };
    public int curPosX = 1;
    public int curPosY = 1;
    int counter = 0;

    // Use this for initialization
    void Start()
    {
        Invoke("PlacePlayer", .1f);
    }

    public void PlacePlayer()
    {
        GameObject GridMan = GameObject.Find("GridManager");
        GridManager script = GridMan.GetComponent<GridManager>();
        script.PlaceObject(gameObject, 1, 1);
    }

    public GridManager GetGridMan()
    {
        GameObject GridMan = GameObject.Find("GridManager");
        GridManager script = GridMan.GetComponent<GridManager>();
        return script;
    }

    public void SetPosX(int newX)
    {
        curPosX = newX;
    }

    public void SetPosY(int newY)
    {
        curPosY = newY;
    }

    void Update()
    {
        if (counter % 20 == 0)
        {
            MakeMove();
        }
        counter += 1;
    }


    public int GetPath(Vector3 state, Dictionary<Vector3, Vector3> Meta)
    {
        List<int> path = new List<int>();

        while(Meta[state] != nil)
        {
            state = Meta[state];
            path.Add((int)state.z);
        }
        if (path.Count > 0)
        {
            return path[path.Count - 1];
        }
        else
        {
            return 0;
        }
    }

    public void MakeMove()
    {
        GameObject PlayerPos = GameObject.Find("PlayerTile");
        PlayerScript scriptPlayer = PlayerPos.GetComponent<PlayerScript>();
        int goalX = scriptPlayer.curPosX;
        int goalY = scriptPlayer.curPosY;
        int chance = Random.Range(0, 10);
        int version = 0;
        if (chance < 7)
        {
            int[,] visited = new int[23, 23];
            GridManager curGrid = GetGridMan();
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 23; j++)
                {
                    if (curGrid.CanPlaceObject(i, j))
                    {
                        visited[j, i] = 0;
                    }
                    else
                    {
                        visited[j, i] = 1;
                    }
                }
            }

            Queue<Vector3> queue = new Queue<Vector3>();
            queue.Enqueue(new Vector3(curPosX, curPosY, 0));

            Dictionary<Vector3, Vector3> Meta = new Dictionary<Vector3, Vector3>();
            Meta.Add(new Vector3(curPosX, curPosY, 0), nil);

            while (queue.Count > 0)
            {
                Vector3 vec = queue.Dequeue();

                if (vec.x == goalX && vec.y == goalY)
                {
                    version = GetPath(vec, Meta);
                    break;
                }

                for (int i = 0; i < 8; i++)
                {
                    int yTmp = (int)vec.y + yPos[i];
                    int xTmp = (int)vec.x + xPos[i];
                    if (InBounds(yTmp) && InBounds(xTmp) && visited[yTmp, xTmp] == 0 && !Meta.ContainsKey(new Vector3(xTmp, yTmp, i)))
                    {
                        Meta.Add(new Vector3(xTmp, yTmp, i), vec);
                        queue.Enqueue(new Vector3(xTmp, yTmp, i));

                    }
                }
                visited[(int)vec.y, (int)vec.x] = 1;
            }
        }else
        {
            version = Random.Range(0, 8);
        }
        GridManager script = GetGridMan();
        script.MoveObjectCat(gameObject, curPosX, curPosY, yPos[version], xPos[version]);
    }

    public bool InBounds(int pos)
    {
        return (0 <= pos && 23 > pos);
    }
}
