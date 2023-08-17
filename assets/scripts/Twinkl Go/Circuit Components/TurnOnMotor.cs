using UnityEngine;

public class TurnOnMotor : MonoBehaviour
{
    [SerializeField]
    private Animator MotorFanAnimator;

    [SerializeField]
    private bool IsMotorSwitchedOn;



    public void ToggleMotorFan(bool Toggle)
    {
        IsMotorSwitchedOn = Toggle;
        MotorFanAnimator.SetBool("IsMotorSwitchedOn", IsMotorSwitchedOn);
    }

    void Start()
    {
        MotorFanAnimator = GetComponentInChildren<Animator>();
    }

}
