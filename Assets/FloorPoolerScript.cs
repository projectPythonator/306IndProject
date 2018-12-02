using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPoolerScript : MonoBehaviour {

    public static FloorPoolerScript current;
    public GameObject pooledObject;
    public static int pooledAmount = 23 * 23;
    public bool willGrow = true;
    public int timer = 0;

    List<GameObject> pooledObjects;

    void Awake()
    {
        current = this;
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            if (pooledObjects[i] == null)
            {
                Debug.Log("hehehehehe");
            }
            else if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
