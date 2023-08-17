using UnityEngine;
using UnityEngine.UI;

public class ChangeAntSprite : MonoBehaviour
{
    [Header("Ant Object")]
    [SerializeField] private Image[] AntBodyParts = new Image[6];

    [Header("Ant Animator")]
    [SerializeField] private Animator AntAnimator;

    [Header("Ant 1")]
    [SerializeField] private Sprite[] Ant1BodyPartSprites = new Sprite[6];

    [Header("Ant 2")]
    [SerializeField] private Sprite[] Ant2BodyPartSprites = new Sprite[4];

    [Header("Ant 3")]
    [SerializeField] private Sprite[] Ant3BodyPartSprites = new Sprite[5];

    [Header("Ant 4")]
    [SerializeField] private Sprite[] Ant4BodyPartSprites = new Sprite[4];

    [Header("Ant 5")]
    [SerializeField] private Sprite[] Ant5BodyPartSprites = new Sprite[5];


    //Set the ant arms to be true or faslse depending on the type of ant
    void SetActiveAntArms(Sprite[] AntSprites, string AntArmName)
    {
        for (int i = 0; i < AntSprites.Length; i++)
        {
            if (AntSprites[i].name.Contains(AntArmName))
            {
                transform.Find(AntArmName).gameObject.SetActive(true);
                break;
            }
            else
            {
                transform.Find(AntArmName).gameObject.SetActive(false);
            }
        }
    }

    void ChangeAntAnimation(int AntType)
    {
        AntAnimator.SetInteger("AntType", AntType);
    }

    //Set the ant sprite and animation based on a random value
    void SetAntSprite()
    {
        //Set new sprite array
        Sprite[] AntSprites = new Sprite[6];

        //Randomly generate a unique value
        int RandomValue = UniqueRandomNumberGenerator.ReturnRandomValue(0, AntBodyParts.Length - 1);

        //While the generated value is the same as the current ant type
        while (AntBodyParts[0].sprite.name.Contains((RandomValue + 1).ToString()))
        {
            //Re-randomize the unique value
            RandomValue = UniqueRandomNumberGenerator.ReturnRandomValue(0, AntBodyParts.Length - 1);
        }

        //A switch case statement sets the ant sprites depending on the random value
        switch (RandomValue)
        {
            case 0:
                AntSprites = Ant1BodyPartSprites;
                break;

            case 1:
                AntSprites = Ant2BodyPartSprites;
                break;

            case 2:
                AntSprites = Ant3BodyPartSprites;
                break;

            case 3:
                AntSprites = Ant4BodyPartSprites;
                break;

            case 4:
                AntSprites = Ant5BodyPartSprites;
                break;
        }

        //Activate or deactivate the ant arms depending on the ant sprite
        SetActiveAntArms(AntSprites, "leftarm");
        SetActiveAntArms(AntSprites, "rightarm");

        //Change the ant animation based on the random value
        ChangeAntAnimation(RandomValue);

        //Loop through the ant body part arrary
        for (int i = 0; i < AntBodyParts.Length; i++)
        {
            //If an ant body part is active
            if (AntBodyParts[i].gameObject.activeInHierarchy)
            {
                //Set the approriate sprite for the ant
                AntBodyParts[i].sprite = ReturnAntSpriteForBodyPart(AntBodyParts[i], AntSprites);

                //If the ant body part belongs to type 4 then set the hat to active
                if (AntBodyParts[i].sprite.name.Contains("ant4"))
                {
                    transform.Find("head/hat").gameObject.SetActive(true);
                }
                else
                {
                    transform.Find("head/hat").gameObject.SetActive(false);
                }
            }
        }
    }

    Sprite ReturnAntSpriteForBodyPart(Image AntImage, Sprite[] AntSprite)
    {
        foreach (Sprite AntBodyPartSprite in AntSprite)
        {
            if (AntBodyPartSprite.name.Contains(AntImage.name))
            {
                return AntBodyPartSprite;
            }
        }
        return null;
    }

    void Awake()
    {
        GameObserver.SwapAntSprites = SetAntSprite;
    }

}
