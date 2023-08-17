using UnityEngine;

public class AdjustPosition : MonoBehaviour
{

    void Awake()
    {
        float AdjustedWidth = Camera.main.ScreenToWorldPoint(transform.position).x / 1.25f;
        float AdjustedHeight = Camera.main.ScreenToWorldPoint(transform.position).y / 1.84f;
        transform.position = new Vector3(AdjustedWidth, AdjustedHeight, 0f);
    }

}
