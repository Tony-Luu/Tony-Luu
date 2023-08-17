using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckGameProgression : MonoBehaviour
{
    [SerializeField]
    [Header("Funnels")]
    [Tooltip("Containers that hold materials that are either conductors or insulators")]
    private GameObject InsulatorFunnel;

    [SerializeField]
    private GameObject ConductorFunnel;

    [Header("Characters")]
    [Tooltip("Displays their success animations when the player is successful")]
    [SerializeField]
    private GameObject Etta;

    [SerializeField]
    private GameObject Granbot;

    private float AverageSuccessAnimationTime;

    [Header("Scripts")]
    [Tooltip("References to other scripts in the scene")]
    [SerializeField]
    private ScoreManager ScoreManagerScript;

    [Header("Winning Screen")]
    [Tooltip("Shows the end screen")]
    [SerializeField]
    private GameObject EndScreen;

    [Header("Sound Effect")]
    [Tooltip("Success sound effect when the player completes the circuit correctly")]
    [SerializeField]
    private AudioSource SoundSource;


    void Start()
    {
        SoundSource = GetComponent<AudioSource>();
        ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
        AverageSuccessAnimationTime = (EttaSuccessAnimation.GetCurrentAnimatorStateInfo(0).length + GranbotSuccessAnimation.GetCurrentAnimatorStateInfo(0).length) / 2f;

        //Increment the score by one
        ScoreManagerScript.AddNewScore(1);

        //Wait until the success animations are completed
        yield return new WaitForSeconds(AverageSuccessAnimationTime);

        //Toggle the Etta and Granbot to deactivate the animations
        ToggleSuccessAnimations(false);
        ToggleMouseCursor(false);

        //Check the victory condition
        CheckVictoryCondition();
    }

    //Check if the material is placed in the correct funnel
    bool IsMaterialPlacedInCorrectFunnel(string MaterialTag, GameObject Funnel)
    {
        //Obtain the material box script
        MaterialBox MaterialBoxScriptForFunnel = Funnel.GetComponent<MaterialBox>();

        //If the funnel is not empty and the material is placed in the correct funnel
        if (MaterialBoxScriptForFunnel.ReturnMaterial() != null && MaterialBoxScriptForFunnel.ReturnMaterial().tag.Contains(MaterialTag))
        {
            return true;
        }
        return false;
    }

    //Give a score to the player if the material is a conductor or insulator and placed in the correct funnel
    public void CheckIfMaterialIsConductorOrInsulator()
    {
        //Obtain a script from both the conductor and insulator funnel
        MaterialBox MaterialBoxScriptForInsulatorFunnel = InsulatorFunnel.GetComponent<MaterialBox>();
        MaterialBox MaterialBoxScriptForConductorFunnel = ConductorFunnel.GetComponent<MaterialBox>();

        //If the material in the insulator funnel is an insulator
        if (MaterialBoxScriptForInsulatorFunnel.ReturnMaterial() != null && IsMaterialPlacedInCorrectFunnel(MaterialBoxScriptForInsulatorFunnel.ReturnMaterial().tag, InsulatorFunnel))
        {
            //Then set the gameobject variable as the insulator
            RemoveMaterialFromScene(MaterialBoxScriptForInsulatorFunnel.ReturnMaterial());
        }
        else if (MaterialBoxScriptForConductorFunnel.ReturnMaterial() != null && IsMaterialPlacedInCorrectFunnel(MaterialBoxScriptForConductorFunnel.ReturnMaterial().tag, ConductorFunnel))
        {
            //If the material in the conductor funnel is an conductor then set the gameobject variable as the conductor
            RemoveMaterialFromScene(MaterialBoxScriptForConductorFunnel.ReturnMaterial());
        }
    }

    //Removes the material from the scene and plays the success animation
    void RemoveMaterialFromScene(GameObject Material)
    {
        //Start the success animation
        StartCoroutine(ShowSuccessAnimation());

        //Removes the material from the buttons
        RemoveMaterialFromButtons(Material);

        //Destroy the material when the success animation time ends
        Destroy(Material, AverageSuccessAnimationTime);
    }

    //Removes the material from the buttons
    void RemoveMaterialFromButtons(GameObject Material)
    {
        //Find all of the buttons in the scene
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");

        //Loop through all of the buttons
        foreach (GameObject Button in Buttons)
        {
            //Obtain the cscript from the button
            CreateMaterial CreateMaterialScript = Button.GetComponent<CreateMaterial>();

            //Find and return the index of the material from the material name list
            int MaterialIndex = CreateMaterialScript.ReturnMaterialNameList().IndexOf(Material.name);

            //If the name exists in the material name list
            if (CreateMaterialScript.ReturnMaterialNameList().Contains(Material.name))
            {
                //If the sprite of the button contains the same sprite as the material
                if (Button.transform.GetChild(0).GetComponent<Image>().sprite == Material.GetComponent<Image>().sprite || Material.transform.childCount > 0 &&
                    Button.transform.GetChild(0).GetComponent<Image>().sprite == Material.transform.GetChild(0).GetComponent<Image>().sprite)
                {
                    //Set the image of the button to null
                    Button.transform.GetChild(0).GetComponent<Image>().sprite = null;
                }
                //Set the name and sprite from both lists to be null based on the index
                CreateMaterialScript.ReturnMaterialNameList()[MaterialIndex] = null;
                CreateMaterialScript.ReturnMaterialSpriteList()[MaterialIndex] = null;
            }
            else
            {
                continue;
            }
        }
    }

    //Checks if the victory condition has been fulfilled
    void CheckVictoryCondition()
    {
        //If the player gets a score of 12 then reveal the end screen
        if (ScoreManagerScript.ReturnScore() == 12)
        {
            EndScreen.SetActive(true);
        }
    }

}
