using UnityEngine;

public class TL_FlammableObject : MonoBehaviour
{
    public GameObject Fire;
    public bool IsAlight;
    public AudioSource BurnVineSound;
    private float FireDistance = 1f;
    private float BurnRate = 0.1f;
    private float BurnCooldown = 1f;    
    private Rigidbody2D FlammableRigidbody;
    private GameObject FireClone;
    private ParticleSystem FireParticles;


    void Start()
    {
        //Obtain the rigidbody and hinge joint components
        FlammableRigidbody = GetComponent<Rigidbody2D>();
    }

    void BurningEffect()
    {
        //If the object is set alight
        if (IsAlight)
        {
            FireClone.transform.position = Vector3.MoveTowards(FireClone.transform.position, transform.position + (transform.right * FireDistance), 1f * Time.deltaTime);

            //If the burning cooldown is less than the time since startup
            if (BurnCooldown < Time.realtimeSinceStartup)
            {
                //Decrease the scale of the object
                transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y, 0f);

                //If the X scale is less than 0.1
                if (transform.localScale.x < 0.1f)
                {
                    //Destroy this object and the fire particle
                    Destroy(gameObject);
                    Destroy(FireClone);
                }
                //Add the cooldown
                BurnCooldown = BurnRate + Time.realtimeSinceStartup;
            }
        }
    }

    void Update()
    {
        BurningEffect();
    }

    void OnParticleCollision(GameObject Object)
    {
        //If the flammable object has touched fire
        if (Object.tag == "TorchFire" || Object.tag == "SpreadFire")
        {
            //If the rigidbody is kinematic
            if (FlammableRigidbody.isKinematic)
            {
                //Switch off kinematic
                FlammableRigidbody.isKinematic = false;
            }

            //If the fire hasn't been spawned
            if (FireClone == null)
            {
                //Play the burning vine sound and loop it
                BurnVineSound.Play();
                BurnVineSound.loop = true;

                //Spawn the fire and switch the boolean to true
                FireClone = Instantiate(Fire, transform.position + (-transform.right * transform.localScale.x), Quaternion.identity);
                FireParticles = FireClone.GetComponent<ParticleSystem>();

                //Calculate the angle
                float Angle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;

                //Obtain the main particles and set the rotation as the angle
                ParticleSystem.MainModule MainParticles = FireParticles.main;
                MainParticles.startRotation = Angle;

                //Set bool to true
                IsAlight = true;
            }

        }

    }

}
