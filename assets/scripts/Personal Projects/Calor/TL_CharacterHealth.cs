using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TL_CharacterHealth : MonoBehaviour
{
    public GameObject Torch;
    public int CurrentHealth;
    public int MaxHealth;    
    private Light2D TorchLight;
    private ParticleSystem TorchParticles;
    private TL_Respawn RespawnScript;


    void Awake()
    {
        //Set the current health to its' maximum health
        SetFullHealth();

        //Obtain the respawn script
        RespawnScript = GetComponent<TL_Respawn>();
    }

    //Set the current health to its' maximum health
    public void SetFullHealth()
    {
        CurrentHealth = MaxHealth;
    }

    //Displays health as the visibility from the torch and torch light
    void DisplayHealth()
    {
        //If the player has picked up the torch
        if (Torch != null)
        {
            //Obtain the torch particles
            TorchParticles = Torch.GetComponentInChildren<ParticleSystem>();

            //Obtain the emission module from the torch particle system
            ParticleSystem.EmissionModule TorchEmission = TorchParticles.emission;

            //Obtain the 2D light
            TorchLight = Torch.transform.Find("TorchFire").GetComponent<Light2D>();

            switch (CurrentHealth)
            {
                case 3:
                    TorchEmission.rateOverTime = 10;
                    TorchLight.intensity = 1f;
                    break;

                case 2:
                    TorchEmission.rateOverTime = 6;
                    TorchLight.intensity = 0.8f;
                    break;

                case 1:
                    TorchEmission.rateOverTime = 3;
                    TorchLight.intensity = 0.6f;
                    break;
            }
        }
    }

    void Update()
    {
        DisplayHealth();
    }

    public void SetCurrentHealth(int Health)
    {
        //Subtract health from current health
        CurrentHealth -= Health;

        //If current health drop to 0 or below
        if (CurrentHealth <= 0)
        {
            //Respawn at last respawn position
            RespawnScript.TeleportToRespawnPosition();

            //Set the current health to its' maximum health
            CurrentHealth = MaxHealth;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player character touches an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            //Subtract 1 health from the player character
            SetCurrentHealth(1);
        }
    }

}
