using UnityEngine;

public class MaterialBox : MonoBehaviour
{
    [SerializeField]
    private GameObject Material;


    public void SetNewMaterial(GameObject NewMaterial)
    {
        Material = NewMaterial;
    }

    public GameObject ReturnMaterial()
    {
        return Material;
    }

}
