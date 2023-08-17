using UnityEngine;
using UnityEngine.UI;

public class CheckMaterialComponent : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private GameObject OnButton;

    [SerializeField]
    private Image ButtonImage;

    [SerializeField]
    private Sprite OnButtonSprite;

    [SerializeField]
    private Sprite LitOnButtonSprite;

    [SerializeField]
    private bool IsButtonOn;

    [Header("Lightbulb")]
    [SerializeField]
    private Image LightbulbImage;

    [SerializeField]
    private Sprite LitLightbulbSprite;

    [SerializeField]
    private Sprite UnlitLightbulbSprite;

    [Header("Material Box")]
    [SerializeField]
    private MaterialBox MaterialContainer;


    public bool IsSwitchTurnedOn()
    {
        return IsButtonOn;
    }

    void Start()
    {
        IsButtonOn = false;
        ButtonImage = OnButton.GetComponent<Image>();
    }

    void Update()
    {
        ToggleLightbulb();
    }

    void ToggleLightbulb()
    {
        if (IsButtonOn && MaterialContainer.ReturnMaterial() != null && MaterialContainer.ReturnMaterial().tag.Contains("Conductor"))
        {
            LightbulbImage.sprite = LitLightbulbSprite;
        }
        else
        {
            LightbulbImage.sprite = UnlitLightbulbSprite;
        }
    }

    public void ActivateOnButton()
    {
        IsButtonOn = true;
        ButtonImage.sprite = LitOnButtonSprite;
    }

    public void DeactivateOnButton()
    {
        IsButtonOn = false;
        ButtonImage.sprite = OnButtonSprite;
    }

}
