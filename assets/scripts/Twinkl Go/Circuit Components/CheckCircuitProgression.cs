using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckCircuitProgression : MonoBehaviour
{
    [Header("Component Count")]
    [Tooltip("Keeps track of the current components on the scene")]
    [SerializeField]
    private int BatteryComponentCount = 0;

    [SerializeField]
    private int CellComponentCount = 0;

    [SerializeField]
    private int LightbulbComponentCount = 0;

    [SerializeField]
    private int CrocodileClipsCount = 0;

    [SerializeField]
    private int BuzzerCount = 0;

    [SerializeField]
    private int MotorCount = 0;

    [SerializeField]
    private int SwitchCount = 0;

    [Header("Win Conditions")]
    [Tooltip("Booleans to show if the current circuit diagram being displayed is correct or not")]
    [SerializeField]
    public bool IsFirstCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsSecondCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsThirdCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsFourthCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsFifthCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsSixthCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsSeventhCircuitDiagramConnectedCorrectly;

    [SerializeField]
    public bool IsEighthCircuitDiagramConnectedCorrectly;

    [Header("Characters")]
    [Tooltip("Displays their success animations when the player is successful")]
    [SerializeField]
    private GameObject Etta;

    [SerializeField]
    private GameObject Granbot;

    [Header("Lists")]
    [Tooltip("Keeps track of the current components on the scene")]
    [SerializeField]
    private List<GameObject> ComponentsList;

    [SerializeField]
    private List<AttachedComponent> AttachedComponentsList;

    [Header("Scripts")]
    [Tooltip("References to other scripts in the scene")]
    [SerializeField]
    private ScoreManager ScoreManagerScript;

    [SerializeField]
    private CircuitDisplayManager CircuitDisplayManagerScript;

    [Header("Winning Screen")]
    [Tooltip("Shows the end screen")]
    [SerializeField]
    private GameObject EndScreen;

    [Header("Sound Effect")]
    [Tooltip("Success sound effect when the player completes the circuit correctly")]
    [SerializeField]
    private AudioSource SoundSource;


    public void AddAttachedComponentsToList(AttachedComponent Component)
    {
        if (!AttachedComponentsList.Contains(Component))
        {
            AttachedComponentsList.Add(Component);
        }
    }

    public void RemoveAttachedComponentsFromList(AttachedComponent Component)
    {
        AttachedComponentsList.Remove(Component);
    }

    public List<AttachedComponent> ReturnAttachedComponentsList()
    {
        return AttachedComponentsList;
    }

    public List<GameObject> ReturnComponentsList()
    {
        return ComponentsList;
    }

    void Start()
    {
        SoundSource = GetComponent<AudioSource>();
        ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        CircuitDisplayManagerScript = GameObject.Find("CircuitDiagramDisplay").GetComponent<CircuitDisplayManager>();
    }

    void DestroyAllComponents()
    {
        GameObject[] Components = GameObject.FindGameObjectsWithTag("Component");

        foreach (GameObject Component in Components)
        {
            Destroy(Component);
        }

        ComponentsList.Clear();
        AttachedComponentsList.Clear();
    }

    //Toggles the success animation by activating or deactivating it
    void ToggleSuccessAnimations(bool Toggle)
    {
        Etta.SetActive(Toggle);
        Granbot.SetActive(Toggle);
    }

    //Toggles the mouse cursor to show or hide it
    void ToggleMouseCursor(bool Toggle)
    {
        if (Toggle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void AddComponentToList(GameObject Component)
    {
        ComponentsList.Add(Component);
    }

    public void RemoveComponentFromList(GameObject Component)
    {
        if (ComponentsList.Contains(Component))
        {
            ComponentsList.Remove(Component);
        }
    }

    public void UpdateComponentCount()
    {
        BatteryComponentCount = ComponentsList.Count(x => x.name.Contains("BatteryComponent"));
        CellComponentCount = ComponentsList.Count(x => x.name.Contains("CellComponent"));
        LightbulbComponentCount = ComponentsList.Count(x => x.name.Contains("LightbulbComponent"));
        CrocodileClipsCount = ComponentsList.Count(x => x.name.Contains("CrocodileClipsComponent"));
        BuzzerCount = ComponentsList.Count(x => x.name.Contains("BuzzerComponent"));
        MotorCount = ComponentsList.Count(x => x.name.Contains("MotorComponent"));
        SwitchCount = ComponentsList.Count(x => x.name.Contains("SwitchComponent"));
    }

    void ResetComponentCount()
    {
        ComponentsList.RemoveAll(x => x == null);

        BatteryComponentCount = 0;
        CellComponentCount = 0;
        LightbulbComponentCount = 0;
        CrocodileClipsCount = 0;
        MotorCount = 0;
        BuzzerCount = 0;
        SwitchCount = 0;
    }

    //Set Etta and Granbot gameobjects to active to play the animation and deactive it afterwards
    IEnumerator ShowSuccessAnimation()
    {
        //Toggle the Etta and Granbot to active to start the success animations
        ToggleSuccessAnimations(true);

        //Play the success sound effect
        SoundSource.Play();

        //Toggle the mouse cursor to hide it
        ToggleMouseCursor(true);

        //Obtain Etta and Granbot animators
        Animator EttaSuccessAnimation = Etta.GetComponent<Animator>();
        Animator GranbotSuccessAnimation = Granbot.GetComponent<Animator>();

        //Calculate the average success animation time between Etta and Granbot
        float AverageSuccessAnimationTime = (EttaSuccessAnimation.GetCurrentAnimatorStateInfo(0).length + GranbotSuccessAnimation.GetCurrentAnimatorStateInfo(0).length) / 2f;

        //Wait until the success animations are completed
        yield return new WaitForSeconds(AverageSuccessAnimationTime);

        //Increment the score by one
        ScoreManagerScript.AddNewScore(1);

        //Toggle the Etta and Granbot to deactivate the animations
        ToggleSuccessAnimations(false);
        ToggleMouseCursor(false);

        //Destroy all components
        DestroyAllComponents();

        //Change the circuit diagram
        CircuitDisplayManagerScript.ChangeCircuitDiagram();

        //If the last circuit is completed correctly then activate the end screen
        if (IsEighthCircuitDiagramConnectedCorrectly)
        {
            EndScreen.SetActive(true);
        }
    }

    //Checks if all attached components complete a circuit
    bool DoesAllAttachedComponentsCompleteCircuit()
    {
        bool IsComponentAttachedInCircuit = false;

        //Loop through all of the components from the list
        foreach (GameObject Component in ComponentsList)
        {
            //Obtain the attached component script
            AttachedComponent AttachedComponentScript = Component.GetComponent<AttachedComponent>();

            //If the component is a crocodile clip
            if (Component.name.Contains("CrocodileClipsComponent"))
            {
                //Loop through the children of the crocodile clips
                foreach (Transform Clip in Component.transform)
                {
                    if (Clip.tag.Contains("CrocodileClip"))
                    {
                        //Obtain the attached component script from one of the crocodile clips
                        AttachedComponentScript = Clip.GetComponentInChildren<AttachedComponent>();

                        //If the component exists in the attached components list
                        IsComponentAttachedInCircuit = AttachedComponentScript.ReturnComponent() != null && AttachedComponentsList.Contains(AttachedComponentScript);

                        //If it doesn't exist in the circuit then break out of the loop
                        if (!IsComponentAttachedInCircuit)
                        {
                            break;
                        }
                    }                    
                }

                //Beak out of the outer loop if the inner loop is broken
                if (!IsComponentAttachedInCircuit)
                {
                    break;
                }
            }
            else
            {
                //Loop through the children of the transform
                foreach (Transform ChildConnector in Component.transform)
                {
                    //If the tag is a connector
                    if (ChildConnector.tag.Contains("Connector"))
                    {
                        //Obtain the attached component script
                        AttachedComponentScript = ChildConnector.GetComponent<AttachedComponent>();

                        //If the component exists in the attached components list
                        IsComponentAttachedInCircuit = AttachedComponentScript.ReturnComponent() != null && AttachedComponentsList.Contains(AttachedComponentScript);

                        //If the component is a battery and it exists in the circuit
                        if (Component.name.Contains("BatteryComponent") && IsComponentAttachedInCircuit)
                        {
                            break;
                        }
                    }
                }
                //If it doesn't exist in the circuit then break out of the loop
                if (!IsComponentAttachedInCircuit)
                {
                    break;
                }
            }
        }
        return IsComponentAttachedInCircuit;
    }

    //Checks if the circuit is completed
    bool IsCircuitComplete()
    {
        bool DoesCircuitCompletedWithCorrectComponents = false;

        //Loop through the components list
        foreach (GameObject PowerSource in ReturnComponentsList())
        {
            //If one of the components is a battery or cell
            if (PowerSource.name.Contains("CellComponent") || PowerSource.name.Contains("BatteryComponent"))
            {
                //Obtain the script from the gameobject
                CheckWholeCircuit CheckWholeCircuitScript = PowerSource.GetComponent<CheckWholeCircuit>();

                //Check if all of the attached components creates a whole circuit
                DoesCircuitCompletedWithCorrectComponents = CheckWholeCircuitScript.DoesAttachedComponentCompletesCircuit();
                break;
            }
        }
        return DoesCircuitCompletedWithCorrectComponents;
    }

    //Check if the components are connected correctly according to the current circuit diagram shown
    public void CheckConnectedComponents()
    {
        ResetComponentCount();
        UpdateComponentCount();

        //If the correct components in the scene are at the right amount and if all of the components are attached correctly to complete an unbroken circuit
        //Show the success animation and reset the component count
        switch (ScoreManagerScript.ReturnScore())
        {
            case 0:
                IsFirstCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && CellComponentCount == 1 && LightbulbComponentCount == 1 && CrocodileClipsCount == 2;
                
                if (IsFirstCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 1:
                IsSecondCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && CellComponentCount == 1 && LightbulbComponentCount == 2 && CrocodileClipsCount == 3;

                if (IsSecondCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 2:
                IsThirdCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && ComponentsList.Any(x => x.GetComponent<CheckWholeCircuit>() != null && 
                !x.GetComponent<CheckWholeCircuit>().AreAllSwitchesTurnedOn()) && CellComponentCount == 1 && LightbulbComponentCount == 1 && SwitchCount == 1 && CrocodileClipsCount == 3;

                if (IsThirdCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 3:
                IsFourthCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && ComponentsList.Any(x => x.GetComponent<CheckWholeCircuit>() != null &&
                x.GetComponent<CheckWholeCircuit>().AreAllSwitchesTurnedOn()) && CellComponentCount == 1 && LightbulbComponentCount == 1 && SwitchCount == 1 && CrocodileClipsCount == 3;

                if (IsFourthCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 4:
                IsFifthCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && BatteryComponentCount == 1 && LightbulbComponentCount == 1 && MotorCount == 1 && 
                    CrocodileClipsCount == 3;

                if (IsFifthCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 5:
                IsSixthCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && BatteryComponentCount == 1 && MotorCount == 2 && CrocodileClipsCount == 3;

                if (IsSixthCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 6:
                IsSeventhCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && ComponentsList.Any(x => x.GetComponent<CheckWholeCircuit>() != null &&
                x.GetComponent<CheckWholeCircuit>().AreAllSwitchesTurnedOn()) && BatteryComponentCount == 1 && SwitchCount == 1 && MotorCount == 1 && BuzzerCount == 1 && CrocodileClipsCount == 4;

                if (IsSeventhCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;

            case 7:
                IsEighthCircuitDiagramConnectedCorrectly = IsCircuitComplete() && DoesAllAttachedComponentsCompleteCircuit() && ComponentsList.Any(x => x.GetComponent<CheckWholeCircuit>() != null &&
                x.GetComponent<CheckWholeCircuit>().AreAllSwitchesTurnedOn()) && BatteryComponentCount == 1 && LightbulbComponentCount == 1 && SwitchCount == 1 && MotorCount == 1 && BuzzerCount == 1 && CrocodileClipsCount == 5;

                if (IsEighthCircuitDiagramConnectedCorrectly)
                {
                    StartCoroutine(ShowSuccessAnimation());
                    ResetComponentCount();
                }
                break;
        }
    }

}
