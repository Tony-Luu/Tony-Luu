using UnityEngine;
using UnityEngine.UI;

public class AnimateButton : MonoBehaviour
{
    [SerializeField] private Image ButtonImage;
    [SerializeField] private float Speed;
    [SerializeField] private RectTransform ButtonRectTransform;
    [SerializeField] private bool IsButtonMoving;
    private RectTransform AntRectTransform;


    //Re-adjusts the pivot for the banana sprite
    void AdjustPivot()
    {
        if (ButtonImage.sprite.name.Contains("Banana"))
        {
            ButtonRectTransform.pivot = new Vector2(0.75f, 0.5f);
        }
        else
        {
            ButtonRectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }

    public void ToggleMovingButton(bool Toggle)
    {
        IsButtonMoving = Toggle;
    }

    void OnDisable()
    {
        IsButtonMoving = false;
    }

    void Start()
    {
        AntRectTransform = GameObject.Find("Canvas/Ant").GetComponent<RectTransform>();
        ButtonImage = GetComponent<Image>();
        ButtonImage.alphaHitTestMinimumThreshold = 0.1f;
        AdjustPivot();
        GameObserver.ResetFoodSprites += AdjustPivot;
    }

    void MoveButton()
    {
        //If the button is moving
        if (IsButtonMoving)
        {
            //If the button is within distance of the ant
            if (Vector2.Distance(ButtonRectTransform.anchoredPosition, AntRectTransform.anchoredPosition) <= 0.1f)
            {
                //Set bool to false to stop moving
                IsButtonMoving = false;
            }
            else
            {
                //Make the button move towards the ant based on it's speed and delta time
                ButtonRectTransform.anchoredPosition = Vector2.MoveTowards(ButtonRectTransform.anchoredPosition, AntRectTransform.anchoredPosition, Time.deltaTime * GameObserver.ReturnSpeedValue());
            }
        }
    }

    void Update()
    {
        MoveButton();
    }

}
