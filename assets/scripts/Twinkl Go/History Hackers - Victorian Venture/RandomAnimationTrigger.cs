using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationTrigger : MonoBehaviour
{
    [Header("Drag relevant animator here")]
    public Animator _anim;
    [Header("Type name of the Trigger from the animator")]
    public string triggerName;
    [Header("Min and Max for the random timer to choose between")]
    public float minTime, MaxTime;
    
    private float randTime;
    
    // Start is called before the first frame update
    void Start()
    {
        randTime = Random.Range(minTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        randTime -= Time.deltaTime;

        if (randTime < 0)
        {
            ActivateTrigger();
        }
    }

    void ActivateTrigger()
    {
        randTime = Random.Range(minTime, MaxTime);
        _anim.SetTrigger(triggerName);
    }
}
