using UnityEngine;
using UnityEngine.EventSystems;

public class DropComponentFromScene : MonoBehaviour, IEndDragHandler
{
    [SerializeField]
    private CheckCircuitProgression CheckCircuitProgressionScript;


    void Start()
    {
        CheckCircuitProgressionScript = GameObject.Find("CircuitBoxArea").GetComponent<CheckCircuitProgression>();
    }

    void RemoveConnectionFromCrocodileClips(GameObject CrocodileClips)
    {
        //Loop through the children from the crocodile clip transform
        foreach (Transform Clip in CrocodileClips.transform)
        {
            //Obtain the attached component script from the clip
            AttachedComponent AttachedCrocodileClipsComponent = Clip.GetComponentInChildren<AttachedComponent>();

            //If the script and the returned component exists
            if (AttachedCrocodileClipsComponent != null && AttachedCrocodileClipsComponent.ReturnComponent() != null)
            {
                //Remove both the crocodile clip and the attached component from the list
                CheckCircuitProgressionScript.ReturnAttachedComponentsList().Remove(AttachedCrocodileClipsComponent.ReturnComponent().GetComponent<AttachedComponent>());
                CheckCircuitProgressionScript.ReturnAttachedComponentsList().Remove(AttachedCrocodileClipsComponent);

                //Set the removed attached component to null
                AttachedCrocodileClipsComponent.ReturnComponent().GetComponent<AttachedComponent>().SetNewComponent(null);
                AttachedCrocodileClipsComponent.SetNewComponent(null);
            }
        }
    }

    void RemoveConnectionFromList()
    {
        AttachedComponent AttachedChildComponentScript;

        //If the gameobject is a crocodile clip
        if (transform.parent.name.Contains("CrocodileClipsComponent"))
        {
            //Remove connections from the crocodile clips
            RemoveConnectionFromCrocodileClips(transform.parent.gameObject);
        }
        else
        {
            //Loops through the children in this transform
            foreach (Transform Child in transform)
            {
                //Obtain the attached component script from the child
                AttachedChildComponentScript = Child.GetComponent<AttachedComponent>();

                //If the script and the returned component exists
                if (AttachedChildComponentScript != null && AttachedChildComponentScript.ReturnComponent() != null)
                {
                    //If the returned component is the crocodile clip
                    if (AttachedChildComponentScript.ReturnComponent().transform.parent.parent.name.Contains("CrocodileClipsComponent"))
                    {
                        //Remove the returned component from the list
                        CheckCircuitProgressionScript.RemoveComponentFromList(AttachedChildComponentScript.ReturnComponent().transform.parent.parent.gameObject);

                        //Destroy the returned component
                        Destroy(AttachedChildComponentScript.ReturnComponent().transform.parent.parent.gameObject);

                        //Remove connections from the crocodile clips
                        RemoveConnectionFromCrocodileClips(AttachedChildComponentScript.ReturnComponent().transform.parent.parent.gameObject);
                    }
                    else
                    {
                        //Remove the returned component from the list
                        CheckCircuitProgressionScript.RemoveComponentFromList(AttachedChildComponentScript.ReturnComponent().transform.parent.gameObject);

                        //Destroy the returned component
                        Destroy(AttachedChildComponentScript.ReturnComponent().transform.parent.gameObject);
                    }
                }
            }
        }
    }

    void RemoveGameObjectFromList()
    {
        //If the transform is a crocodile clip
        if (transform.parent.name.Contains("CrocodileClipsComponent"))
        {
            //Obtain the parent and remove this gameobject from the list
            CheckCircuitProgressionScript.RemoveComponentFromList(transform.parent.gameObject);
        }
        else
        {
            //Remove this gameobject from the list
            CheckCircuitProgressionScript.RemoveComponentFromList(gameObject);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null || eventData.pointerCurrentRaycast.gameObject != null && 
            !eventData.pointerCurrentRaycast.gameObject.tag.Contains("CircuitBox") && !eventData.pointerCurrentRaycast.gameObject.tag.Contains("Component") && 
            !eventData.pointerCurrentRaycast.gameObject.tag.Contains("CrocodileClip") && !eventData.pointerCurrentRaycast.gameObject.tag.Contains("DropdownMenu") && 
            eventData.pointerCurrentRaycast.gameObject.tag.Contains("Bin") || eventData.pointerCurrentRaycast.gameObject.tag.Contains("Button") || 
            eventData.pointerCurrentRaycast.gameObject.tag.Contains("Untagged"))
        {
            //Remove the connection from the list
            RemoveConnectionFromList();

            //Remove gameobject from the list
            RemoveGameObjectFromList();

            //Update the amount of components in the scene
            CheckCircuitProgressionScript.UpdateComponentCount();

            //Check and update any changes to the connected components in the scene
            CheckCircuitProgressionScript.CheckConnectedComponents();

            if (transform.name.Contains("CellComponent") || transform.name.Contains("BatteryComponent"))
            {
                //Obtain the check whole circuit script
                CheckWholeCircuit CheckWholeCircuitScript = GetComponent<CheckWholeCircuit>();

                //Switch all lightbulbs, buzzers, and motors to false
                CheckWholeCircuitScript.SwitchLightbulbs(false);
                CheckWholeCircuitScript.SwitchBuzzers(false);
                CheckWholeCircuitScript.SwitchMotors(false);
            }

            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag.Contains("Bin"))
            {
                //Obtain the audio source and play the sound if the component is dropped into the bin
                AudioSource ComponentAudioSource = eventData.pointerCurrentRaycast.gameObject.GetComponent<AudioSource>();
                ComponentAudioSource.Play();
            }
            else
            {
                GetComponent<AudioSource>().enabled = false;
            }

            if (transform.tag.Contains("CrocodileClip"))
            {
                //Destroy the parent gameobject
                Destroy(transform.parent.gameObject);
            }
            else
            {
                //Destroy this gameobject
                Destroy(gameObject);
            }
        }
    }

}
