using UnityEngine;
using System.Collections.Generic;

public class CheckWholeCircuit : MonoBehaviour
{
    [SerializeField]
    private List<TurnOnLightbulb> LightbulbComponentsList = new List<TurnOnLightbulb>();

    [SerializeField]
    private List<TurnOnBuzzer> BuzzerComponentsList = new List<TurnOnBuzzer>();

    [SerializeField]
    private List<TurnOnMotor> MotorComponentsList = new List<TurnOnMotor>();

    [SerializeField]
    private List<PowerButtonComponent> SwitchComponentsList = new List<PowerButtonComponent>();

    [SerializeField]
    private CheckCircuitProgression CheckCircuitProgressionScript;



    public List<PowerButtonComponent> ReturnSwitchComponentList()
    {
        return SwitchComponentsList;
    }

    void Start()
    {
        CheckCircuitProgressionScript = GameObject.Find("CircuitBoxArea").GetComponent<CheckCircuitProgression>();
    }

    //Checks if all switches are turned on
    public bool AreAllSwitchesTurnedOn()
    {
        foreach (PowerButtonComponent SwitchComponent in SwitchComponentsList)
        {
            if(!SwitchComponent.IsSwitchTurnedOn())
            {
                return false;
            }
        }
        return true;
    }

    //Switch all lightbulbs on or off
    public void SwitchLightbulbs(bool AreLightbulbsInCircuit)
    {
        foreach (TurnOnLightbulb Lightbulb in LightbulbComponentsList)
        {
            Lightbulb.ToggleLightbulb(AreLightbulbsInCircuit);
        }
    }

    //Switch all buzzer sounds on or off
    public void SwitchBuzzers(bool AreBuzzersInCircuit)
    {
        foreach (TurnOnBuzzer Buzzer in BuzzerComponentsList)
        {
            Buzzer.ToggleBuzzerSound(AreBuzzersInCircuit);
        }
    }

    //Switch all motors on or off
    public void SwitchMotors(bool AreMotorsInCircuit)
    {
        foreach (TurnOnMotor Motor in MotorComponentsList)
        {
            Motor.ToggleMotorFan(AreMotorsInCircuit);
        }
    }

    void AddComponentInList(GameObject Component)
    {
        if (Component.name.Contains("LightbulbComponent"))
        {
            //If the next attached component is a lightbulb and it's not in the lightbulb list
            if (!LightbulbComponentsList.Contains(Component.GetComponent<TurnOnLightbulb>()))
            {
                //Add the lightbulb in the list
                LightbulbComponentsList.Add(Component.GetComponent<TurnOnLightbulb>());
            }
        }
        else if (Component.name.Contains("SwitchComponent"))
        {
            if (!SwitchComponentsList.Contains(Component.GetComponent<PowerButtonComponent>()))
            {
                //If the next attached component is a switch and it's not in the switch list, add the switch in the list
                SwitchComponentsList.Add(Component.GetComponent<PowerButtonComponent>());
            }
        }
        else if (Component.name.Contains("BuzzerComponent"))
        {
            if (!BuzzerComponentsList.Contains(Component.GetComponent<TurnOnBuzzer>()))
            {
                //If the next attached component is a buzzer and it's not in the buzzer list, add the buzzer in the list
                BuzzerComponentsList.Add(Component.GetComponent<TurnOnBuzzer>());
            }
        }
        else if (Component.name.Contains("MotorComponent"))
        {
            if (!MotorComponentsList.Contains(Component.GetComponent<TurnOnMotor>()))
            {
                //If the next attached component is a motor and it's not in the motor list, add the motor in the list
                MotorComponentsList.Add(Component.GetComponent<TurnOnMotor>());
            }
        }
    }

    //Checks if the attached component completes the circuit
    public bool DoesAttachedComponentCompletesCircuit()
    {
        GameObject NextAttachedComponent = null;

        AttachedComponent AttachedComponentScript = null;

        bool IsComponentInList = false;

        //Loops through the transform
        foreach (Transform ChildConnector in transform)
        {
            //Obtains the attached component script
            AttachedComponentScript = ChildConnector.GetComponent<AttachedComponent>();

            //If the transform is a battery component and it's connected on the left side
            if (ChildConnector.name.Contains("LeftConnector"))
            {
                //If the battery's positive sides are connected
                if (AttachedComponentScript.ReturnComponent() != null)
                {
                    //Set the next attached component to be searched as the parent of the gameobject
                    NextAttachedComponent = AttachedComponentScript.ReturnComponent().transform.parent.gameObject;
                    break;
                }
            }
        }

        //While the next attached component is not the same as this current gameobject
        while (NextAttachedComponent != gameObject)
        {
            //Set the next attached component to be excluded from loops
            GameObject ExcludedObject = NextAttachedComponent;

            //If the attached component exists
            if (NextAttachedComponent != null)
            {
                //If the next component is a crocodile clip
                if (NextAttachedComponent.transform.parent.name.Contains("CrocodileClipsComponent"))
                {
                    //Loop through the gameobject to find the crocodile clip
                    foreach (Transform Connector in NextAttachedComponent.transform.parent)
                    {
                        //If the connector is not the excluded gameobject
                        if (Connector.gameObject != ExcludedObject)
                        {
                            //Obtain the attached component script from the child gameobject
                            AttachedComponentScript = Connector.GetComponentInChildren<AttachedComponent>();

                            //If the script exists
                            if (AttachedComponentScript != null)
                            {
                                //If the returned component exists
                                if (AttachedComponentScript.ReturnComponent() != null)
                                {
                                    //Set the next attached component to be searched as the parent gameobject
                                    NextAttachedComponent = AttachedComponentScript.ReturnComponent().transform.parent.gameObject;

                                    //Check if the component exists in the list
                                    IsComponentInList = CheckCircuitProgressionScript.ReturnComponentsList().Contains(NextAttachedComponent);

                                    //If the component doesn't exist in the list then break out of the loop
                                    if (!IsComponentInList)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        //Add the next attached component in the list
                                        AddComponentInList(NextAttachedComponent);
                                    }
                                    break;
                                }
                                else
                                {
                                    //If the next attached component doesn't exists then break out of the loop
                                    NextAttachedComponent = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Loop through the returned component
                    foreach (Transform Connector in AttachedComponentScript.ReturnComponent().transform.parent)
                    {
                        //If the connector is not the same as the returned component and the gameobject is a connector
                        if (Connector.gameObject != AttachedComponentScript.ReturnComponent() && Connector.tag.Contains("Connector"))
                        {
                            //Obtain the attached component script from the gameobject
                            AttachedComponentScript = Connector.GetComponent<AttachedComponent>();

                            //If the script exists
                            if (AttachedComponentScript != null)
                            {
                                //If the returned component exists
                                if (AttachedComponentScript.ReturnComponent() != null)
                                {
                                    //Obtain the next attached component from the parent of the returned component
                                    NextAttachedComponent = AttachedComponentScript.ReturnComponent().transform.parent.gameObject;

                                    //If the next attached component is a crocodile clip
                                    if (NextAttachedComponent.transform.parent.name.Contains("CrocodileClipsComponent"))
                                    {
                                        //Check if the component exists in the list
                                        IsComponentInList = CheckCircuitProgressionScript.ReturnComponentsList().Contains(NextAttachedComponent.transform.parent.gameObject);

                                        //If the component doesn't exist in the list then break out of the loop
                                        if (!IsComponentInList)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //Check if the component exists in the list
                                        IsComponentInList = CheckCircuitProgressionScript.ReturnComponentsList().Contains(NextAttachedComponent.transform.parent.gameObject);

                                        //If the component doesn't exist in the list then break out of the loop
                                        if (!IsComponentInList)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            //Add the next attached component in the list
                                            AddComponentInList(NextAttachedComponent);
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    //If the returned component doesn't exist, set the next attached component to null
                                    NextAttachedComponent = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                //If the next attached component doesn't exist or if the component doesn't exist in the list
                if (NextAttachedComponent == null || !IsComponentInList)
                {
                    break;
                }
            }
            else
            {
                //If the attached component doesn't exist then break out of the loop
                break;
            }
        }

        //If the next attached component is the same as this gameobject
        if (NextAttachedComponent == gameObject)
        {
            //If all of the switches that exist in the circuit are switched on
            if (AreAllSwitchesTurnedOn())
            {
                //Switch all lightbulbs, buzzers, and motors to true
                SwitchLightbulbs(true);
                SwitchBuzzers(true);
                SwitchMotors(true);
            }
            else if (!AreAllSwitchesTurnedOn())
            {
                //If only one of the switches that exist in the circuit is switched off
                //Switch all lightbulbs, buzzers, and motors to false
                SwitchLightbulbs(false);
                SwitchBuzzers(false);
                SwitchMotors(false);
            }
            else if(SwitchComponentsList.Count == 0)
            {
                //If there are no switches in the circuit then switch all lightbulbs, buzzers, and motors to true
                SwitchLightbulbs(true);
                SwitchBuzzers(true);
                SwitchMotors(true);
            }
            return true;
        }
        else
        {
            //Switch all lightbulbs, buzzers, and motors to false
            SwitchLightbulbs(false);
            SwitchBuzzers(false);
            SwitchMotors(false);

            //Clear the lightbulb, buzzer, motor, and switch lists
            LightbulbComponentsList.Clear();
            BuzzerComponentsList.Clear();
            MotorComponentsList.Clear();
            SwitchComponentsList.Clear();
        }
        //Return false if the circuit is incomplete
        return false;
    }

}
