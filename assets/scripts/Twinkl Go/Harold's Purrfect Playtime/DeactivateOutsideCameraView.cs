using UnityEngine;

public class DeactivateOutsideCameraView : MonoBehaviour
{
    [SerializeField] private SpawnObjects SpawnObjectsScript;



    void Start()
    {
        SpawnObjectsScript = GameObject.Find("GameManager").GetComponent<SpawnObjects>();
    }

    void ToggleEnemyActivation()
    {
        if (transform.position.x <= -10f)
        {
            gameObject.SetActive(false);
            SpawnObjectsScript.CheckCurrentlySpawnedAmount();
        }
    }

    void FixedUpdate()
    {
        ToggleEnemyActivation();
    }

}
