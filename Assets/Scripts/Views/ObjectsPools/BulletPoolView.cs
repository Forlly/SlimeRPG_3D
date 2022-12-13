using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolView : MonoBehaviour
{
    public static BulletPoolView Instance;

    private List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] private int amountPool = 128;
    [SerializeField] private GameObject bullet;
    
    private bool isFull = false;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < amountPool; i++)
        {
            GameObject tmpObj = Instantiate(bullet);
            tmpObj.SetActive(false);
            poolObjects.Add(tmpObj);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                poolObjects[i].SetActive(true);
                return poolObjects[i];
            }

            isFull = true;
        }

        if (isFull)
        {
            return CreateNewObject();
        }
        return null;
    }
    
    public  void TurnOfObject( GameObject _platform)
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (_platform == poolObjects[i])
            {
                poolObjects[i].SetActive(false);
            }
            
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject tmpObj = Instantiate(bullet);
        tmpObj.SetActive(true);
        poolObjects.Add(tmpObj);
        return tmpObj;
    }
}
