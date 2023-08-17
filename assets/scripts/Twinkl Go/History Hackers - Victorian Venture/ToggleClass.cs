using UnityEngine;

public class ToggleClass : MonoBehaviour
{
    [SerializeField] private GameObject ToggledObject;
    [SerializeField] private bool Toggle;


    public bool ReturnToggleState()
    {
        return Toggle;
    }

    public void ToggleFunction()
    {
        Toggle = !Toggle;
        ToggledObject.SetActive(Toggle);

        if (ToggledObject.tag.Contains("Clipboard") || ToggledObject.tag.Contains("Minimap"))
        {
            ToggledObject.transform.SetAsLastSibling();
        }
    }

}
