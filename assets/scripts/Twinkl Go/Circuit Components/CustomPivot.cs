using UnityEngine;

public class CustomPivot : MonoBehaviour
{
    [SerializeField]
    private GameObject Clip;

    [SerializeField]
    private float Angle;

    [SerializeField]
    private float RotationRadius;

    [SerializeField]
    private float RadiusOffset;

    private RectTransform PivotRectTransform;


    void Start()
    {
        PivotRectTransform = GetComponent<RectTransform>();
    }

    public void RotateWithClip()
    {
        if(Clip != null)
        {
            RectTransform ClipRectTransform = Clip.GetComponent<RectTransform>();

            Angle = ClipRectTransform.eulerAngles.magnitude * Mathf.Deg2Rad - RadiusOffset;
            float xPos = Mathf.Cos(Angle) * RotationRadius;
            float yPos = Mathf.Sin(Angle) * RotationRadius;

            PivotRectTransform.anchoredPosition = ClipRectTransform.anchoredPosition + new Vector2(xPos, yPos);
        }
    }

    void Update()
    {
        RotateWithClip();
    }

}
