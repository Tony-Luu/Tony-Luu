using UnityEngine;
using UnityEngine.EventSystems;

public class DropMaterialFromScene : MonoBehaviour, IEndDragHandler
{

    public void OnEndDrag(PointerEventData eventData)
    {
        //If the object is dragged onto no detected gameobject or into a gameobject that's not a box or one of the funnels
        if (eventData.pointerCurrentRaycast.gameObject == null || eventData.pointerCurrentRaycast.gameObject != null && 
            !eventData.pointerCurrentRaycast.gameObject.tag.Contains("Box") && !eventData.pointerCurrentRaycast.gameObject.name.Contains("InsulatorFunnel") &&
            !eventData.pointerCurrentRaycast.gameObject.name.Contains("ConductorFunnel"))
        {
            //Obtain the audio source and disable it
            AudioSource MaterialSoundSource = GetComponent<AudioSource>();
            MaterialSoundSource.enabled = false;

            //Find the box and obtain the script
            MaterialBox MaterialBoxScript = GameObject.Find("Box").GetComponent<MaterialBox>();

            //Set the new material to null
            MaterialBoxScript.SetNewMaterial(null);

            //Find the funnels in the scene
            GameObject[] Funnels = GameObject.FindGameObjectsWithTag("Funnel");

            //Loop through both funnels
            foreach (GameObject Funnel in Funnels)
            {
                //Obtain the script and set the new material to null
                MaterialBoxScript = Funnel.GetComponent<MaterialBox>();
                MaterialBoxScript.SetNewMaterial(null);
            }
            //Destroy the gameobject
            Destroy(gameObject);
        }
    }

}
