using UnityEngine;

public class TL_PickupTorch : MonoBehaviour
{
    public AnimatorOverrideController TorchAnimationOverride;
    public ParticleSystem TorchParticles;

        
    void PickupTorch(GameObject Player)
    {
        if (Input.GetKey(KeyCode.X))
        {
            //Set the parent of the torch to the player
            transform.SetParent(Player.transform);

            //Reset local positions and euler angles
            transform.GetChild(0).localPosition = new Vector3(1.2f, 2f, 0f);
            transform.localPosition = new Vector3(-1f, 0f, 0f);
            transform.localEulerAngles = new Vector3(0f, 0f, 60f);

            //Obtain the animator from the player
            Animator PlayerAnimations = Player.GetComponent<Animator>();

            //Override the animator controller
            PlayerAnimations.runtimeAnimatorController = TorchAnimationOverride;

            //Obtain the torch script to set this gameobject as the torch
            TL_Torch TorchScript = Player.GetComponent<TL_Torch>();
            TorchScript.Torch = gameObject;
            TL_CharacterHealth HealthScript = Player.GetComponent<TL_CharacterHealth>();
            HealthScript.Torch = gameObject;

            //Destroy the box collider and this script
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //If the player touches the torch then pick up the torch
            PickupTorch(collision.gameObject);
        }
    }

}
