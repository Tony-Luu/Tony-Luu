using UnityEngine;

public class ChangePivot : MonoBehaviour
{
    private Vector2 ScreenResolution;
    private RectTransform ComponentRectTransform;



    void Start()
    {
        ComponentRectTransform = GetComponent<RectTransform>();
        ScreenResolution = new Vector2(Screen.width, Screen.height);
        ChangePivotPoints();
    }

    //CHange the pivot points of the object depending on screen resolution
    void ChangePivotPoints()
    {
        //If the camera aspect is 1280 by 720
        if (Camera.main.aspect >= 1.7f)
        {
            //Set the pivot to default
            ComponentRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            ComponentRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        }
        else if(Camera.main.aspect >= 1.33f)
        {
            //If the camera aspect is 640 by 480 then change the Y axis of the pivot
            ComponentRectTransform.anchorMin = new Vector2(0.5f, 0.375f);
            ComponentRectTransform.anchorMax = new Vector2(0.5f, 0.375f);
        }
        
    }

    //If the game detects a screen resolution change then change the pivots
    void DetectScreenResolutionChange()
    {
        //If the X or Y positions are not the same as the screen width or height respectively
        if (ScreenResolution.x != Screen.width || ScreenResolution.y != Screen.height)
        {
            ChangePivotPoints();
            ScreenResolution = new Vector2(Screen.width, Screen.height);
        }
    }

    void Update()
    {
        DetectScreenResolutionChange();
    }
}
