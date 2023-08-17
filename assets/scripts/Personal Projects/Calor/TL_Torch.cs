using UnityEngine;

public class TL_Torch : MonoBehaviour
{
    public GameObject Torch;
    private TL_AnimationManager AnimationManagerScript;
    private ParticleSystem TorchParticles;
    private ParticleSystem.CollisionModule TorchFireCollision;
    

    void Start()
    {
        AnimationManagerScript = GetComponent<TL_AnimationManager>();
        DontDestroyOnLoad(gameObject);
    }

    //Toggles the particle collision during the torch animation
    void ToggleParticleCollision()
    {
        //If the player obtains the torch
        if (Torch != null)
        {
            //Obtain the particle system from the torch flame
            TorchParticles = Torch.GetComponentInChildren<ParticleSystem>();

            //Obtain the collision module from the torch fire's particle system
            TorchFireCollision = TorchParticles.collision;

            //If the current state is the swing torch
            if (AnimationManagerScript.ReturnCurrentState() == TL_AnimationManager.CharacterState.SwingTorch)
            {
                //Turn on particle collision
                TorchFireCollision.enabled = true;
            }
            else    //If the current animator state is not the swing
            {
                //Turn of particle collision
                TorchFireCollision.enabled = false;
            }
        }
    }

    void Update()
    {
        ToggleParticleCollision();
    }

}
