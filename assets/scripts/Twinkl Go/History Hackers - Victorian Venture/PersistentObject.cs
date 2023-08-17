using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    [SerializeField] private static PersistentObject Instance;


    void SingletonMethod()
    {
        //If the instance exist and it's not the same as this instance
        if (Instance != null && Instance != this)
        {
            //Destroy this object
            Destroy(gameObject);
        }
        else
        {
            //Set the instance to this object
            Instance = this;

            //Don't destroy this instance
            DontDestroyOnLoad(Instance);
        }
    }

    void Awake()
    {
        SingletonMethod();
    }

}
