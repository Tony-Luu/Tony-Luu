using UnityEngine;

public class AttachedComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject Component;


    public void SetNewComponent(GameObject NewComponent)
    {
        Component = NewComponent;
    }

    public GameObject ReturnComponent()
    {
        return Component;
    }

}
