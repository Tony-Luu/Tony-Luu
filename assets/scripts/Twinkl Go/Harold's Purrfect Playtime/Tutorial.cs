using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TutorialText;
    [SerializeField] private SpawnObjects SpawnObjectsScript;
    [SerializeField] private GameObject InstructionScreen;



    void StartGame()
    {
        if (!InstructionScreen.activeInHierarchy && Time.timeScale == 1)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameObject Harold = GameObject.FindGameObjectWithTag("Harold");
                Animator HaroldAnimator = Harold.GetComponent<Animator>();
                HaroldAnimator.enabled = true;

                GameObject Muffin = GameObject.FindGameObjectWithTag("Muffin");
                Animator MuffinAnimator = Muffin.GetComponent<Animator>();
                MuffinAnimator.enabled = true;

                SpawnObjectsScript.enabled = true;
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        StartGame();
    }

}
