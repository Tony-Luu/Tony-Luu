using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveWorkerAnt : MonoBehaviour
{
    [SerializeField] private Vector2 TargetPosition;
    [SerializeField] private RectTransform AntRectTransform;
    [SerializeField] private RectTransform ButtonRectTransform;
    [SerializeField] private bool IsAntMoving;
    [SerializeField] private bool HasAntTouchedFood;
    private GraphicRaycaster Raycaster;
    private PointerEventData PointerEventData;
    private EventSystem ButtonEventSystem;


    public bool ReturnIfAntIsMoving()
    {
        return IsAntMoving;
    }

    public void SetIfAntIsMoving(bool AntIsMoving)
    {
        IsAntMoving = AntIsMoving;
    }

    public void SetNewButtonRectTransform(RectTransform NewButtonRectTransform)
    {
        ButtonRectTransform = NewButtonRectTransform;
    }

    public void SetNewTargetPosition(Vector2 NewTargetPosition)
    {
        TargetPosition = NewTargetPosition;
    }

    public bool HasAntArrivedAtFood()
    {
        return HasAntTouchedFood;
    }

    void OnEnable()
    {
        //Set bool to true to move the ant
        IsAntMoving = true;
    }

    void OnDisable()
    {
        IsAntMoving = false;
        HasAntTouchedFood = false;
        ButtonRectTransform = null;
        TargetPosition = Vector3.zero;
    }

    void Start()
    {
        //Obtain the raycaster from the canvas
        Raycaster = GetComponentInParent<GraphicRaycaster>();

        //Obtain the event system in the Scene
        ButtonEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        //Obtain the ant's rect transform
        AntRectTransform = GetComponent<RectTransform>();
    }

    bool HasAntTouchedFoodItem()
    {
        //Set the new pointer event data for the button event system
        PointerEventData = new PointerEventData(ButtonEventSystem);

        //Convert the world position to screen point
        Vector2 WorldToScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, AntRectTransform.position);

        //Set the position of the pointer event data to the converted world to screen point variable
        PointerEventData.position = WorldToScreenPos;

        //Create a list for the raycasted results
        List<RaycastResult> results = new List<RaycastResult>();

        //Create a raycast using the Graphics Raycaster and the converted world to screen point variable
        Raycaster.Raycast(PointerEventData, results);

        //Loop through all of the raycast results
        foreach (RaycastResult result in results)
        {
            //If the raycasted gameobject still exists and the tag is the Food Button
            if (result.gameObject != null && result.gameObject.tag.Contains("FoodButton"))
            {
                return true;
            }
        }
        return false;
    }

    bool HasAntReachedTargetDestinationWithinDistance(Vector2 TargetDestination, float Distance)
    {
        return Vector2.Distance(AntRectTransform.anchoredPosition, TargetDestination) < Distance;
    }

    void MoveAnt()
    {
        //If the ant is moving
        if (IsAntMoving)
        {
            //Move the ant towards the target position
            AntRectTransform.anchoredPosition = Vector2.MoveTowards(AntRectTransform.anchoredPosition, TargetPosition, Time.deltaTime * GameObserver.ReturnSpeedValue());
        }

        //If the ant hasn't touched the food
        if (!HasAntTouchedFood)
        {
            //If the ant reached its' destination
            if (HasAntReachedTargetDestinationWithinDistance(TargetPosition, 100f))
            {
                //If the rect of the ant is overlapping the rect of the food button
                if (DetectOverlappingRectTransforms.IsFirstRectTransformOverlappingWithSecondRectTransform(AntRectTransform, ButtonRectTransform))
                {
                    //If the ant has touched the food item
                    if (HasAntTouchedFoodItem())
                    {
                        //Set bool to true
                        HasAntTouchedFood = HasAntTouchedFoodItem();

                        //Set bool to false to stop the ant from moving
                        IsAntMoving = false;
                    }
                }
            }
        }
        else
        {
            //If all of the worker ants has touched the food item and the ant is not moving
            if (GameObserver.AreWorkerAntsAtFoodItem() && !IsAntMoving)
            {
                //Set the target destination to the ant with speech bubble
                GameObserver.WorkerAntDestination();

                //Set bool to true to start moving the ant
                IsAntMoving = true;
            }            
        }
    }

    void FixedUpdate()
    {
        MoveAnt();
    }

}
