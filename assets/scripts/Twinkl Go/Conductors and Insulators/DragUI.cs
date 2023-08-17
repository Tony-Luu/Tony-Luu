using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private AudioClip DragSound;

    [SerializeField]
    private AudioClip PlaceSound;

    [SerializeField]
    private AudioSource SoundSource;

    [SerializeField]
    private int SiblingIndex;

    [SerializeField]
    private Vector2 OriginalPosition;

    [SerializeField]
    private RectTransform UIRectTransform;

    [SerializeField]
    public bool DoesUIElementSnapBackIntoPosition;


    //Set a new original position for the UI element to snap back to
    public void SetNewOriginalPosition(Vector2 NewOriginalPosition)
    {
        OriginalPosition = NewOriginalPosition;
    }

    void Start()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        SoundSource = GetComponent<AudioSource>();
        UIRectTransform = GetComponent<RectTransform>();
        OriginalPosition = UIRectTransform.anchoredPosition;
    }

    //Sets an audio clip and plays a sound from the audio source
    void PlaySound(AudioClip Sound)
    {
        if (SoundSource != null && SoundSource.enabled)
        {
            SoundSource.clip = DragSound;
            SoundSource.Play();
        }
    }

    //Updates the original position if the UI element snaps back into position
    void UpdateOriginalPosition()
    {
        if (DoesUIElementSnapBackIntoPosition && UIRectTransform.anchoredPosition != OriginalPosition)
        {
            OriginalPosition = UIRectTransform.anchoredPosition;
        }
    }

    //Snaps back the UI element to its' original position
    void SnapUIElementBackToOriginalPosition()
    {
        if (DoesUIElementSnapBackIntoPosition)
        {
            UIRectTransform.anchoredPosition = OriginalPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Play a sound
        PlaySound(DragSound);

        //Change the sibling index so it's always in front of the other interactable UI elements
        transform.SetSiblingIndex(SiblingIndex);

        //Disable blocks raycasts to prevent the current dragged UI element to be detected by the mouse
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move the anchored position with the current mouse position scaled by the canvas scale factor
        UIRectTransform.anchoredPosition += eventData.delta / MainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Play a sound
        PlaySound(PlaceSound);

        //Snap the UI back into its' original position
        SnapUIElementBackToOriginalPosition();

        //Update the original position of the UI element if the UI element snaps back into position
        UpdateOriginalPosition();

        //Enable blocks raycasts for the current dragged UI element to be detected by the mouse
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
