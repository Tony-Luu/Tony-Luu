using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateMaterialPosition : MonoBehaviour, IEndDragHandler
{
    [SerializeField]
    private int SiblingIndex;

    [SerializeField]
    private DragUI DragUIScript;

    [SerializeField]
    private CheckGameProgression CheckGameProgressionScript;


    void Start()
    {
        DragUIScript = GetComponent<DragUI>();
        CheckGameProgressionScript = GameObject.Find("CircuitBoxArea").GetComponent<CheckGameProgression>();
    }

    void ChangeMaterialBox(GameObject Material, GameObject Box)
    {
        MaterialBox MaterialBoxScript = Box.GetComponent<MaterialBox>();
        MaterialBoxScript.SetNewMaterial(Material);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //If the material is a conductor and it's in the conductor funnel or If the material is a insulator and it's in an insulator funnel
            if (eventData.pointerCurrentRaycast.gameObject.name.Contains("InsulatorFunnel") && transform.tag.Contains("Insulator") ||
                eventData.pointerCurrentRaycast.gameObject.name.Contains("ConductorFunnel") && transform.tag.Contains("Conductor"))
            {
                //Find the box gameobject and make the material in the box null
                ChangeMaterialBox(null, GameObject.Find("Box"));

                //Find the funnel and make the material in the box the same as this gameobject
                ChangeMaterialBox(gameObject, eventData.pointerCurrentRaycast.gameObject);

                //Check if the material is a conductor or insulator
                CheckGameProgressionScript.CheckIfMaterialIsConductorOrInsulator();

                //Obtain the rect transform from the funnel
                RectTransform FunnelRectTransform = eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<RectTransform>();

                //Set the position to be above the funnel
                Vector2 FunnelPosition = new Vector2(FunnelRectTransform.anchoredPosition.x, FunnelRectTransform.anchoredPosition.y + 50f);

                //Set the bool to true to snap the UI element back into position
                DragUIScript.DoesUIElementSnapBackIntoPosition = true;

                //Set the new original position to snap the UI back into position
                DragUIScript.SetNewOriginalPosition(FunnelPosition);
            }
            else if (eventData.pointerCurrentRaycast.gameObject.tag.Contains("Box"))
            {
                //Find the funnels and make the material in the box null
                ChangeMaterialBox(null, GameObject.Find("InsulatorFunnel"));
                ChangeMaterialBox(null, GameObject.Find("ConductorFunnel"));

                //Find the box and set the material as this gameobject
                ChangeMaterialBox(gameObject, eventData.pointerCurrentRaycast.gameObject);

                //Obtain the rect transform of the box
                RectTransform BoxRectTransform = eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<RectTransform>();

                //Set the new original position to snap the UI back into position
                DragUIScript.SetNewOriginalPosition(BoxRectTransform.anchoredPosition);
            }
            else
            {
                //Set the bool to true to snap the UI element back into position
                DragUIScript.DoesUIElementSnapBackIntoPosition = true;
            }
        }     
    }

}
