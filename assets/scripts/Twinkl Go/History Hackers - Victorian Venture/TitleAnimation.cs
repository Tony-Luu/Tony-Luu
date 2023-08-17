using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private float MinimumRandomTime;
    [SerializeField] private float MaximumRandomTime;
    [SerializeField] private float RandomTime;
    [SerializeField] private int AnimationIndex;
    [SerializeField] private Animator TitleAnimator;


    void Start()
    {
        TitleAnimator = GetComponent<Animator>();

        //Set the random time between two values
        RandomTime = Random.Range(MinimumRandomTime, MaximumRandomTime);
    }

    void RandomizeTransition()
    {
        //If the current animation finishes
        if (TitleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            //Set a timer by subtracting from delta time
            RandomTime -= Time.deltaTime;

            //If the timer is 0 or below
            if (RandomTime <= 0f)
            {
                //Increment the index
                AnimationIndex++;

                //If the index is above the maximum
                if (AnimationIndex > 3)
                {
                    //Reset the index to 0
                    AnimationIndex = 0;
                }
                //Set the integet as the index to animate the title
                TitleAnimator.SetInteger("AnimationState", AnimationIndex);

                //Set the random time between two values
                RandomTime = Random.Range(MinimumRandomTime, MaximumRandomTime);
            }
        }
    }

    void Update()
    {
        RandomizeTransition();
    }

}
