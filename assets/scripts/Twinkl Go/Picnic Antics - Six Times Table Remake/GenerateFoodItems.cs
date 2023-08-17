using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateFoodItems : MonoBehaviour
{
    [SerializeField] private Image[] FoodImages = new Image[4];
    [SerializeField] private List<Sprite> FoodSprites;



    void RandomizeFoodSprites()
    {
        for (int i = 0; i < FoodImages.Length; i++)
        {
            int UniqueNumber = UniqueRandomNumberGenerator.ReturnRandomValue(0, FoodSprites.Count);
            FoodImages[i].sprite = FoodSprites[UniqueNumber];
            FoodImages[i].preserveAspect = true;
        }
    }

    void Awake()
    {
        RandomizeFoodSprites();
        GameObserver.ResetFoodSprites = RandomizeFoodSprites;
    }

}
