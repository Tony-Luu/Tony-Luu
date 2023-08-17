using UnityEngine;
using System.Collections;

public class TL_TentacleThrust : MonoBehaviour {

	//Variables
	private float fl_Delay = 0.3f;
	private float fl_AttackDelay = 2f;
	public float fl_AttackCooldown = 2f;
	public MPI_Level_Manager MPI_LMScript;
	private ParticleSystem Water_Particles;
	private bool bl_Attack = false;
	private int in_Counter;
	private GameObject go_Water;
	private TL_KrakenScript KrakenScript;
	public int in_Amount;

	
	void Start()
	{
		go_Water = GameObject.Find ("GO_Water");
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		KrakenScript = GameObject.FindGameObjectWithTag ("Kraken").GetComponent<TL_KrakenScript>();
		Water_Particles = GetComponentInChildren<ParticleSystem>();
		Water_Particles.GetComponent<ParticleSystem>().Play();
		fl_AttackCooldown = 2f + Time.realtimeSinceStartup;
	}

	void Update()
	{
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			TentacleAttack();
		}
	}

	void TentacleAttack()
	{
		if (fl_AttackCooldown < Time.realtimeSinceStartup && in_Amount <= 2)
		{
			bl_Attack = true;
		}

		if(bl_Attack)
		{
			if (fl_Delay > 0f)
			{
				Water_Particles.GetComponent<ParticleSystem>().Stop();
				fl_Delay -= Time.deltaTime;
				transform.Translate (0, 30f * Time.deltaTime, 0);
			}
			else
			{
				if(transform.position.y > go_Water.transform.position.y - 2f)
				{
					transform.Translate (0, -30f * Time.deltaTime, 0);
				}
				else
				{
					in_Amount++;
					Water_Particles.GetComponent<ParticleSystem>().Play();
					fl_AttackCooldown = fl_AttackDelay + Time.realtimeSinceStartup;
					fl_Delay = 0.3f;
					bl_Attack = false;
				}
			}
		}
	}

}
