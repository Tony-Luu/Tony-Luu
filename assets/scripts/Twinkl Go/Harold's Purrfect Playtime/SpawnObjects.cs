using UnityEngine;
using System.Collections.Generic;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private int SpawnedWave;
    [SerializeField] private int SpawnCount;
    [SerializeField] private int SpawnQuantity;
    [SerializeField] private string CurrentSpawnedObject;
    [SerializeField] private float SpawnInterval;
    [SerializeField] private float SpawnCooldown;
    [SerializeField] private bool IsSpawning;
    [SerializeField] private List<GameObject> ActiveObjects;
    [SerializeField] private int RandomIndex;
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private RectTransform SpawnPoint;


    void Start()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        SpawnCooldown = SpawnInterval + Time.time;
    }

    void ChangeSpawnWave()
    {
        switch (SpawnedWave)
        {
            case 0:
                CurrentSpawnedObject = "Ball of Yarn";
                break;

            case 1:
                CurrentSpawnedObject = "Books";
                break;

            case 2:
                CurrentSpawnedObject = "Cat Bowl";
                break;

            case 3:
                CurrentSpawnedObject = "Cat Box";
                break;

            case 4:
                CurrentSpawnedObject = "Laptop";
                break;
        }
    }

    void SpawnEnemy()
    {
        //Set default gameobject variable
        GameObject Object = null;

        //If the cooldown is less than the current time and the spawning is true
        if (SpawnCooldown < Time.time && IsSpawning)
        {
            //Set the gameobject as the currently spawned enemy
            Object = ObjectPool.CurrentInstance.GetPooledObject(CurrentSpawnedObject);
            Object.transform.SetParent(MainCanvas.transform);
            Object.transform.SetSiblingIndex(7);
            Object.transform.localScale = new Vector3(1f, 1f, 1f);

            //Uniquely randomize a number
            SpawnedWave = RandomizeUniqueNumber(0, 5);

            //Change the spawn wave based on the value of spawned wave
            ChangeSpawnWave();

            //Increase spawn quantity
            SpawnQuantity++;

            //Add the cooldown with the interval and the current time
            SpawnCooldown = SpawnInterval + Time.time;
        }

        //If the objects in the scene is equal to the amount already pooled
        if (SpawnQuantity == ObjectPool.CurrentInstance.ItemPool[SpawnedWave].AmountToPool)
        {
            //Set spawn quantity to 0
            SpawnQuantity = 0;

            //Set the spawning to false
            IsSpawning = false;
        }

        if (Object != null)
        {
            //Obtain the object's rect transform
            RectTransform ObjectRectTransform = Object.GetComponent<RectTransform>();

            //If the spawned enemy matches the tags
            if (Object.tag.Contains("Ball of Yarn") || Object.tag.Contains("Cat Box"))
            {
                //Set the new position for the enemy
                ObjectRectTransform.anchoredPosition = SpawnPoint.anchoredPosition;
            }
            else if (Object.tag.Contains("Books"))
            {
                //Set the new position for the enemy
                ObjectRectTransform.anchoredPosition = SpawnPoint.anchoredPosition;
            }
            else if (Object.tag.Contains("Cat Bowl"))
            {
                ObjectRectTransform.anchoredPosition = SpawnPoint.anchoredPosition;
            }
            else if (Object.tag.Contains("Laptop"))
            {
                ObjectRectTransform.anchoredPosition = SpawnPoint.anchoredPosition;
            }
            //Activate the enemy in the scene
            Object.SetActive(true);

            //Returns all of the active objects in the scene into a list
            ActiveObjects = ObjectPool.CurrentInstance.ReturnAllActiveObjects();
        }
    }

    void FixedUpdate()
    {
        SpawnEnemy();
    }

    void IncreaseSpawnAmount()
    {
        foreach (ObjectPoolItem Item in ObjectPool.CurrentInstance.ItemPool)
        {
            Item.AmountToPool += 1;
            if (Item.AmountToPool > 3 && Item.ObjectToPool.GetComponent<MoveObject>() != null)
            {
                Item.AmountToPool = 3;
            }
        }
    }

    public void CheckCurrentlySpawnedAmount()
    {
        //Returns all of the active objects in the scene into a list
        ActiveObjects = ObjectPool.CurrentInstance.ReturnAllActiveObjects();

        //If there are no more objects in the scene and the spawning has stopped
        if (ActiveObjects.Count == 0 && !IsSpawning)
        {
            //Increase the spawned count
            SpawnCount++;

            //If the spawned wave is more than the amount of items in the list
            if (SpawnCount > 4)
            {
                //Increase the spawn amount
                IncreaseSpawnAmount();

                //Set spawn count to 0
                SpawnCount = 0;
            }
            //Set the spawning to true
            IsSpawning = true;
        }
    }

    int RandomizeUniqueNumber(int MinNumber, int MaxNumber)
    {
        //Randomize the number between the minimum and maximum value
        int RandomNumber = Random.Range(MinNumber, MaxNumber);

        //While the random number is the same as the previous random number, re-randomize it
        while (RandomNumber == RandomIndex)
        {
            //Randomize the number between the minimum and maximum value again
            RandomNumber = Random.Range(MinNumber, MaxNumber);
        }
        //Assign the variable to the random number
        RandomIndex = RandomNumber;

        //Return the value
        return RandomIndex;
    }

}
