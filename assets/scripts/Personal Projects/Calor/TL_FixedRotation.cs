using UnityEngine;

public class TL_FixedRotation : MonoBehaviour
{
    private Quaternion InitialRotation;

    void Awake()
    {
        //Set the initial rotation
        InitialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        //Set the rotation to the initial value
        transform.rotation = InitialRotation;
    }

}
