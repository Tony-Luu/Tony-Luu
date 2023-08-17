using UnityEngine;

public class DrawWire : MonoBehaviour
{
    [SerializeField]
    private RectTransform TopClip;

    [SerializeField]
    private RectTransform BottomClip;

    [SerializeField]
    private RectTransform RectTransformWire;


    void Start()
    {
        RectTransformWire = GetComponent<RectTransform>();
    }

    public void UpdateWire()
    {
        Vector2 Direction = (BottomClip.anchoredPosition - TopClip.anchoredPosition).normalized;
        float Distance = Vector2.Distance(TopClip.anchoredPosition, BottomClip.anchoredPosition);

        Vector2 DistanceDifference = TopClip.anchoredPosition - BottomClip.anchoredPosition;
        float Angle = Mathf.Atan2(DistanceDifference.y, DistanceDifference.x) * Mathf.Rad2Deg;

        RectTransformWire.sizeDelta = new Vector2(Distance, RectTransformWire.sizeDelta.y);
        RectTransformWire.anchoredPosition = TopClip.anchoredPosition + Direction * Distance * 0.5f;
        RectTransformWire.localEulerAngles = new Vector3(0f, 0f, Angle);
    }

    void LateUpdate()
    {
        UpdateWire();
    }

}
