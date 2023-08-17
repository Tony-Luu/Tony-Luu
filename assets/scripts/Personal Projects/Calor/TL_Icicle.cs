using UnityEngine;

public class TL_Icicle : MonoBehaviour
{
    public AudioClip IcicleFallSound;
    public AudioClip IcicleCrashSound;
    private AudioSource IcicleAudioSource;
    private Rigidbody2D IcicleRigidbody;
    private GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        IcicleRigidbody = GetComponent<Rigidbody2D>();
        IcicleAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            //Make this collider collide with the player when it hits the ground
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.GetComponent<Collider2D>(), false);

            //Set the clip to play the sound
            IcicleAudioSource.clip = IcicleCrashSound;
            IcicleAudioSource.Play();

            //Destroy the rigidbody
            Destroy(IcicleRigidbody);

            //Set the parent as the collided transform
            transform.SetParent(collision.transform);

            //Round down the Y position
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Floor(transform.localPosition.y), transform.localPosition.z);
        }
    }

    void OnParticleCollision(GameObject Object)
    {
        //If this gameobject touches fire
        if (Object.CompareTag("SpreadFire"))
        {
            //Set the clip to play the sound
            IcicleAudioSource.clip = IcicleFallSound;
            IcicleAudioSource.Play();

            //Make this collider ignore the player collider when falling
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.GetComponent<Collider2D>());

            //If the rigidbody is kinematic
            if (IcicleRigidbody.isKinematic)
            {
                //Set it to false
                IcicleRigidbody.isKinematic = false;
            }
        }
    }

}
