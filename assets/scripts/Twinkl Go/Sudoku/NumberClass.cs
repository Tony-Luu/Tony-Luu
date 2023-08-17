using UnityEngine;
using System;
using TMPro;

public class NumberClass : MonoBehaviour
{
    [SerializeField]
    private GameObject Box;

    [SerializeField]
    private int X;

    [SerializeField]
    private int Y;

    [SerializeField]
    private int Number;
    private int PreviousNumber;

    [SerializeField]
    private bool IsDuplicateNumber;

    [SerializeField]
    private TextMeshProUGUI NumberTextMesh;



    public void SetNewBox(GameObject NewBox)
    {
        Box = NewBox;
    }

    public void SetNewNumber(int NewNumber)
    {
        Number = NewNumber;
    }

    public void SetNewXPosition(int NewXPosition)
    {
        X = NewXPosition;
    }

    public void SetNewYPosition(int NewYPosition)
    {
        Y = NewYPosition;
    }

    public void SetNewTextColour(Color NewTextColour)
    {
        NumberTextMesh.color = NewTextColour;
    }

    public void IsNumberStillDuplicate(bool IsDuplicate)
    {
        IsDuplicateNumber = IsDuplicate;
    }

    public GameObject ReturnBox()
    {
        return Box;
    }

    public int ReturnNumber()
    {
        return Number;
    }

    public int ReturnXPosition()
    {
        return X;
    }

    public int ReturnYPosition()
    {
        return Y;
    }

    public bool IsNumberDuplicate()
    {
        return IsDuplicateNumber;
    }

    void Start()
    {
        PreviousNumber = Number;
        NumberTextMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    bool HasNumberChanged()
    {
        return PreviousNumber != Number;
    }

    void UpdatePreviousNumber()
    {
        if (HasNumberChanged())
        {
            PreviousNumber = Number;
        }
    }

    void Update()
    {
        UpdatePreviousNumber();
    }

}
