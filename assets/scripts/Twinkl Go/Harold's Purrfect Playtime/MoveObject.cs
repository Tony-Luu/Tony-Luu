using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float DefaultSpeed;
    [SerializeField] private GameObject Harold;


    public void ResetSpeed()
    {
        MoveSpeed = DefaultSpeed;
    }

    public void AddNewMoveSpeed(float NewMoveSpeed)
    {
        MoveSpeed += NewMoveSpeed;
    }

    public void SetNewMoveSpeed(float NewMoveSpeed)
    {
        MoveSpeed = NewMoveSpeed;
    }

    void Start()
    {
        Harold = GameObject.FindGameObjectWithTag("Harold");
    }

    void MoveEnemy()
    {
        if (Harold != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-10f, transform.position.y), MoveSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        MoveEnemy();
    }

}