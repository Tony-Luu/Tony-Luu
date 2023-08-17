using UnityEngine;

public class FindCamera : MonoBehaviour
{
    [SerializeField] private Canvas MainCanvas;


    void Start()
    {
        MainCanvas = GetComponent<Canvas>();
        MainCanvas.worldCamera = Camera.main;
    }

}
