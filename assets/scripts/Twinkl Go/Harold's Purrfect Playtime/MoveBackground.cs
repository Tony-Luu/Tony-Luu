using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float DefaultSpeed;
    [SerializeField] private GameObject Harold;
    [SerializeField] private RectTransform BackgroundRectTransform;
    [SerializeField] private RectTransform CanvasRectTransform;
    private Vector2 ScreenResolution;


    public void ResetSpeed()
    {
        Speed = DefaultSpeed;
    }

    public void AddNewSpeed(float NewSpeed)
    {
        Speed += NewSpeed;
    }

    public void SetNewSpeed(float NewSpeed)
    {
        Speed = NewSpeed;
    }

    void Awake()
    {
        ScreenResolution = new Vector2(Screen.width, Screen.height);
    }

    void Start()
    {
        Harold = GameObject.FindGameObjectWithTag("Harold");
        BackgroundRectTransform = GetComponent<RectTransform>();
        CanvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    void UpdateBackgroundPosition()
    {
        if (ScreenResolution.x != Screen.width || ScreenResolution.y != Screen.height)
        {
            if (Camera.main.aspect >= 1.33f)
            {
                //Percentage difference between canvas X size of 1280 and 1104 from 1280 by 720 and 4:3 respectively
                float PercentageDifference = 0.1440f;

                float xDifference = BackgroundRectTransform.anchoredPosition.x * PercentageDifference;
                float xTotal;

                if (BackgroundRectTransform.anchoredPosition.x > 0f)
                {
                    xTotal = BackgroundRectTransform.anchoredPosition.x + xDifference;

                    BackgroundRectTransform.anchoredPosition = new Vector2(xTotal, BackgroundRectTransform.anchoredPosition.y);
                    Debug.Log("Anchored X Position: " + BackgroundRectTransform.anchoredPosition.x + "   X Total: " + xTotal);
                    //BackgroundRectTransform.sizeDelta = new Vector2(-BackgroundRectTransform.anchoredPosition.x, BackgroundRectTransform.sizeDelta.y);
                }
                else if (BackgroundRectTransform.anchoredPosition.x < 0f)
                {
                    xTotal = BackgroundRectTransform.anchoredPosition.x - xDifference;

                    BackgroundRectTransform.anchoredPosition = new Vector2(xTotal, BackgroundRectTransform.anchoredPosition.y);
                    Debug.Log("Anchored X Position: " + BackgroundRectTransform.anchoredPosition.x + "   X Total: " + xTotal);
                    //BackgroundRectTransform.sizeDelta = new Vector2(xTotal, BackgroundRectTransform.sizeDelta.y);
                }
                ScreenResolution = new Vector2(Screen.width, Screen.height);
            }
        }
    }

    void ScrollBackground()
    {
        if (Harold != null)
        {
            BackgroundRectTransform.anchoredPosition = Vector2.MoveTowards(BackgroundRectTransform.anchoredPosition, new Vector2(-CanvasRectTransform.sizeDelta.x, BackgroundRectTransform.anchoredPosition.y), Speed * (2f * Time.deltaTime));
        }
    }

    void Update()
    {
        UpdateBackgroundPosition();
    }

    void FixedUpdate()
    {
        ScrollBackground();
        DeactivateBackground();
    }

    void DeactivateBackground()
    {
        if (BackgroundRectTransform.anchoredPosition.x == -CanvasRectTransform.sizeDelta.x)
        {
            gameObject.SetActive(false);
        }
    }

}
