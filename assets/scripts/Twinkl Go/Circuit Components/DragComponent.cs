using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private AudioClip ButtonDragSound;

    [SerializeField]
    private AudioClip DragSound;

    [SerializeField]
    private AudioClip PlaceSound;

    [SerializeField]
    private AudioSource ComponentAudioSource;

    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private RectTransform ComponentRectTransform;

    [SerializeField]
    private int SiblingIndex;

    [SerializeField]
    private Image ComponentImage;

    [SerializeField]
    private Material DefaultMaterial;

    [SerializeField]
    private Material PrimaryMaterial;

    [SerializeField]
    private Material SecondaryMaterial;

    private AttachedComponent AttachedComponentScript;


    void SetAlphaHitTestThreshhold()
    {
        if(transform.tag.Contains("Component"))
        {
            ComponentImage = GetComponent<Image>();
            ComponentImage.alphaHitTestMinimumThreshold = 0.1f;
        }
    }

    void Start()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        ComponentAudioSource = GetComponent<AudioSource>();
        ComponentRectTransform = GetComponent<RectTransform>();
        SetAlphaHitTestThreshhold();
    }

    //Obtains the sibling index of the last created object to always display the selected number box first
    public int ObtainLastCreatedObjectSiblingIndex()
    {
        GameObject[] CircuitComponents = GameObject.FindGameObjectsWithTag("Component");
        int SiblingIndexOffset = 1;
        int LastSiblingIndexOfClone = (SiblingIndex - SiblingIndexOffset) + CircuitComponents.Length;
        return LastSiblingIndexOfClone;
    }

    void PlaySound(AudioClip Sound)
    {
        if (ComponentAudioSource.enabled)
        {
            ComponentAudioSource.clip = Sound;
            ComponentAudioSource.Play();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.tag.Contains("Button"))
        {
            PlaySound(ButtonDragSound);
        }
        else
        {
            PlaySound(DragSound);
        }

        if (transform.tag.Contains("CrocodileClip"))
        {
            //Change the sibling index of the parent so it's always in front of the other interactable UI elements
            transform.parent.SetSiblingIndex(ObtainLastCreatedObjectSiblingIndex());
        }
        else
        {
            //Change the sibling index so it's always in front of the other interactable UI elements
            transform.SetSiblingIndex(ObtainLastCreatedObjectSiblingIndex());
        }
        DragCrocodileClips();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void DragCrocodileClips()
    {
        //If the dragged gameobject is a component
        if (transform.tag.Contains("Component"))
        {
            //Loop through the children of this transform
            foreach (Transform Child in transform)
            {
                //Obtain the attached component script from the child transform
                AttachedComponentScript = Child.GetComponent<AttachedComponent>();

                //If the script exists and the gameobject is attached onto another gameobject
                if (AttachedComponentScript != null && AttachedComponentScript.ReturnComponent() != null)
                {
                    //If the returned component is a crocodile clip
                    if (AttachedComponentScript.ReturnComponent().transform.parent.parent.name.Contains("CrocodileClipsComponent"))
                    {
                        //Set the returned component's position to the child position
                        AttachedComponentScript.ReturnComponent().transform.parent.position = Child.position;

                        //Increase the sibling index of the returned component based on the amount of current attachments
                        AttachedComponentScript.ReturnComponent().transform.parent.parent.SetSiblingIndex(ObtainLastCreatedObjectSiblingIndex());
                    }
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        ComponentRectTransform.anchoredPosition += eventData.delta / MainCanvas.scaleFactor;
        DragCrocodileClips();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && !eventData.pointerCurrentRaycast.gameObject.tag.Contains("Bin"))
        {
            PlaySound(PlaceSound);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
