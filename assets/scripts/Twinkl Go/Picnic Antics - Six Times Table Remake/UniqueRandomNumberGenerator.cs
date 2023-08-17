using UnityEngine;

public static class UniqueRandomNumberGenerator
{
    static int PreviousValue;


    //Randomizes and returns a unique number
    public static int ReturnRandomValue(int MinValue, int MaxValue)
    {
        //Randomize a value between the min and max value
        int RandomValue = Random.Range(MinValue, MaxValue);

        //If the previous randomized value is the same as the current random value
        while (PreviousValue == RandomValue)
        {
            //Re-randomize the value again
            RandomValue = Random.Range(MinValue, MaxValue);
        }
        //Set the previous value as the current random value
        PreviousValue = RandomValue;

        //Return the randomized value
        return RandomValue;
    }

}
