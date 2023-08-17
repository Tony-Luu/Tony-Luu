using UnityEngine;

public class ColourSelection : MonoBehaviour
{
    [SerializeField]
    private static RectTransform DropdownMenuRectTransform;



    void Start()
    {
        DropdownMenuRectTransform = GetComponent<RectTransform>();
        HideDropdownMenu();
    }

    public static RectTransform ReturnDropdownMenu()
    {
        return DropdownMenuRectTransform;
    }

    public static void ShowDropdownMenu()
    {
        DropdownMenuRectTransform.gameObject.SetActive(true);
    }

    public static void HideDropdownMenu()
    {
        DropdownMenuRectTransform.gameObject.SetActive(false);
    }

}
