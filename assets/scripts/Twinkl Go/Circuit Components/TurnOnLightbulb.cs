using UnityEngine;
using UnityEngine.UI;

public class TurnOnLightbulb : MonoBehaviour
{
    [SerializeField]
    private Sprite LitLightbulb;

    [SerializeField]
    private Sprite UnlitLightbulb;

    [SerializeField]
    private bool CanLightbulbSwitchOn;

    private Image LightbulbImage;



    public void ToggleLightbulb(bool Toggle)
    {
        CanLightbulbSwitchOn = Toggle;
    }

    void Start()
    {
        LightbulbImage = GetComponent<Image>();
    }

    void SwitchLightbulb()
    {
        if (CanLightbulbSwitchOn)
        {
            LightbulbImage.sprite = LitLightbulb;
        }
        else
        {
            LightbulbImage.sprite = UnlitLightbulb;
        }
    }

    void Update()
    {
        SwitchLightbulb();
    }

}
