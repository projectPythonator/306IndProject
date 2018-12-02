using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int curPosX = 11;
    public int curPosY = 11;
    int counter = 0;

    // Use this for initialization
    void Start () {
        Invoke("PlacePlayer", .1f);
    }

    public void PlacePlayer()
    {
        GameObject GridMan = GameObject.Find("GridManager");
        GridManager script = GridMan.GetComponent<GridManager>();
        script.PlaceObject(gameObject, 11, 11);
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
        if (counter % 5 == 0) {
            GetInputs();
        }
        counter += 1;
    }

    public void GetInputs()
    {
        bool up   = Input.GetButton("Up");
        bool down = Input.GetButton("Down");
        bool left = Input.GetButton("Left");
        bool right= Input.GetButton("Right");
        if (up && !down)
        {
            Debug.Log("pressed up");
            GridManager script = GetGridMan();
            script.MoveObject(gameObject, curPosX, curPosY, 0, 1);
        }
        if (!up && down)
        {
            Debug.Log("pressed down");
            GridManager script = GetGridMan();
            script.MoveObject(gameObject, curPosX, curPosY, 0, -1);
        }
        if (left && !right)
        {
            Debug.Log("pressed left");
            GridManager script = GetGridMan();
            script.MoveObject(gameObject, curPosX, curPosY, -1, 0);
        }
        if (!left && right)
        {
            Debug.Log("pressed right");
            GridManager script = GetGridMan();
            script.MoveObject(gameObject, curPosX, curPosY, 1, 0);
        }
    }
}
