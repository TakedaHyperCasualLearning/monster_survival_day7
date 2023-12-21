using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameEvent gameEvent;
    private Dictionary<int, List<GameObject>> objectPool = new Dictionary<int, List<GameObject>>();
    private bool isNewGenerate;

    public ObjectPool(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;

        gameEvent.ReleaseObject += ReleaseObject;
    }


    public GameObject GetGameObject(GameObject prefab)
    {
        int hash = prefab.GetHashCode();

        if (objectPool.ContainsKey(hash))
        {
            List<GameObject> tempList = objectPool[hash];
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].activeSelf) continue;
                tempList[i].SetActive(true);
                return tempList[i];
            }

            GameObject tempObject = Object.Instantiate(prefab);
            tempList.Add(tempObject);
            isNewGenerate = true;
            return tempObject;
        }

        List<GameObject> objectList = new List<GameObject>();
        GameObject gameObject = Object.Instantiate(prefab);
        objectList.Add(gameObject);
        objectPool.Add(hash, objectList);
        isNewGenerate = true;
        return gameObject;
    }

    private void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public List<GameObject> GetGameObjectList(GameObject prefab)
    {
        int hash = prefab.GetHashCode();
        if (objectPool.ContainsKey(hash))
        {
            return objectPool[hash];
        }

        return null;
    }

    public bool IsNewGenerate { get => isNewGenerate; set => isNewGenerate = value; }
}

