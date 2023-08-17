using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<ObjectPoolItem> ItemPool;

    [SerializeField]
    public static ObjectPool CurrentInstance;

    [SerializeField]
    private List<GameObject> PooledObjects;


    void Awake()
    {
        CurrentInstance = this;
    }

    void Start()
    {
        PooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem Item in ItemPool)
        {
            for (int i = 0; i < Item.AmountToPool; i++)
            {
                GameObject Object = Instantiate(Item.ObjectToPool);
                Object.SetActive(false);
                PooledObjects.Add(Object);
            }
        }
    }

    public GameObject GetPooledObject(string Tag)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == Tag)
            {
                return PooledObjects[i];
            }
        }

        foreach (ObjectPoolItem Item in ItemPool)
        {
            if (Item.ObjectToPool.tag == Tag)
            {
                if (Item.ShouldExpand)
                {
                    GameObject Object = Instantiate(Item.ObjectToPool);
                    Object.SetActive(false);
                    PooledObjects.Add(Object);
                    return Object;
                }
            }
        }
        return null;
    }

}
