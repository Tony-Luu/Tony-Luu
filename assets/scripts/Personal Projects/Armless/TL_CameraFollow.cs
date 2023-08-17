using UnityEngine;

public class TL_CameraFollow : MonoBehaviour
{
    public float MouseSensitivity;
    private float xRotation = 0f;
    private Transform Player;
    

    void Awake()
    {
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Obtain the parent of the transform
        Player = transform.parent;
    }

    //Rotates the camera
    void RotateCamera()
    {
        //Obtain the X and Y mouse inputs
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        //Subtract the X rotation with the Y axis of the mouse
        xRotation -= MouseY;

        //Clamp the X rotation between two angles
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Set the local rotation with quaternion euler
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotate the player based on the upwards vector3 and the X axis of the mouse
        Player.Rotate(Vector3.up * MouseX);
    }

    void LateUpdate()
    {
        RotateCamera();
    }

}
