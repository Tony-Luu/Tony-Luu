using UnityEngine;
using System.Collections;

public class TL_ThunderCloud : MonoBehaviour {

	//Variables
	private float fl_Delay = 7f;
	private float fl_Cooldown;
	public MPI_Level_Manager MPI_LMScript;
	private ParticleSystem ps_Lightning;
	private ParticleSystem ps_Lightning02;
	public bool bl_Lightning;
	public int in_Increment;
	private MPI_BoatNew BoatScript;
	
	//Lightning bolts
	public GameObject go_Lightning1;
	public GameObject go_Lightning2;
	private float fl_LightningDelay = 0.45f;
	private float fl_LightningCooldown;
	
	
	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		ps_Lightning = transform.FindChild("pf_Lightning").GetComponent<ParticleSystem>();
		ps_Lightning02 = transform.FindChild("pf_Lightning02").GetComponent<ParticleSystem>();
		BoatScript = GameObject.Find("GO_Boat").GetComponent<MPI_BoatNew>();
		fl_LightningCooldown = fl_LightningDelay + Time.realtimeSinceStartup;
	}

	void Update()
	{		
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			FadeInandOut();
		}
	}

	void FadeInandOut()
	{
		if(in_Increment == 2)
		{
			fl_Cooldown = fl_Delay + Time.realtimeSinceStartup;
			if(ps_Lightning.GetComponent<ParticleSystem>().isPlaying && ps_Lightning02.GetComponent<ParticleSystem>().isPlaying)
			{
				ps_Lightning.GetComponent<ParticleSystem>().Stop();
				ps_Lightning02.GetComponent<ParticleSystem>().Stop();
			}
			transform.GetComponent<Renderer>().material.color -= new Color(0f, 0f, 0f, 0.3f * Time.deltaTime);
			Destroy (gameObject, 10f);
		}
		else
		{
			transform.GetComponent<Renderer>().material.color += new Color(0f, 0f, 0f, 0.3f * Time.deltaTime);
			if (transform.GetComponent<Renderer>().material.color.a > 1f)
			{
				if (fl_Cooldown < Time.realtimeSinceStartup)
				{
					bl_Lightning = !bl_Lightning;
					in_Increment++;
					fl_Cooldown = fl_Delay + Time.realtimeSinceStartup;
				}
			}
		}
		LightningParticles();
	}

	void LightningParticles()
	{
		if (bl_Lightning)
		{
			if(!ps_Lightning.GetComponent<ParticleSystem>().isPlaying && !ps_Lightning02.GetComponent<ParticleSystem>().isPlaying)
			{
				ps_Lightning.GetComponent<ParticleSystem>().Play();
				ps_Lightning02.GetComponent<ParticleSystem>().Play();				
			}
			LightningAttack();
		}
		else
		{
			if(ps_Lightning.GetComponent<ParticleSystem>().isPlaying && ps_Lightning02.GetComponent<ParticleSystem>().isPlaying)
			{
				ps_Lightning.GetComponent<ParticleSystem>().Stop();
				ps_Lightning02.GetComponent<ParticleSystem>().Stop();
			}
		}
	}
	
	void LightningAttack()
	{
		if(fl_LightningCooldown < Time.realtimeSinceStartup && BoatScript.st_BoatState == MPI_BoatNew.BoatState.Alive)
		{
			Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), -Vector2.up * 15f, Color.red);
			LightningRayCast();			
			fl_LightningCooldown = fl_LightningDelay + Time.realtimeSinceStartup;
		}
	}
	
	void LightningRayCast()
	{
		Ray2D ry_Ray;
		RaycastHit2D ry2d_Hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up * 15f);	
		if(ry2d_Hit != null && ry2d_Hit.collider != null)
		{
			if(ry2d_Hit.collider.gameObject.name == "GO_Boat")
			{
				BoatScript.col2D_Boat = ry2d_Hit.collider;				
			}			
		}
	}
}
