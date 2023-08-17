using UnityEngine;
using UnityEngine.UI;

public class PowerButtonComponent : MonoBehaviour
{
    [SerializeField]
    private Image ButtonImage;

    [SerializeField]
    private Sprite UnlitOnButtonSprite;

    [SerializeField]
    private Sprite LitOnButtonSprite;

    [SerializeField]
    private GameObject OnButton;

    [SerializeField]
    private bool IsButtonOn;

    [SerializeField]
    private CheckCircuitProgression CheckCircuitProgressionScript;


    public bool IsSwitchTurnedOn()
    {
        return IsButtonOn;
    }

    void Start()
    {
        IsButtonOn = false;
        CheckCircuitProgressionScript = GameObject.Find("CircuitBoxArea").GetComponent<CheckCircuitProgression>();
    }

    public void ActivateOnButton()
    {
        IsButtonOn = true;
        ButtonImage.sprite = LitOnButtonSprite;
        CheckCircuitProgressionScript.CheckConnectedComponents();
    }

    public void DeactivateOnButton()
    {
        IsButtonOn = false;
        ButtonImage.sprite = UnlitOnButtonSprite;
        CheckCircuitProgressionScript.CheckConnectedComponents();
    }

}
