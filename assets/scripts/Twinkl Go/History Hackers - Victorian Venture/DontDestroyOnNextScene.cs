using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnNextScene : MonoBehaviour
{
    [SerializeField] private GameObject Instance;


    void Awake()
    {
        int AmountOfInstances = GameObject.FindGameObjectsWithTag(tag).Length;

        if (AmountOfInstances > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

}
