using UnityEngine;
using System.Collections;

public class TL_VolcanoScript : MonoBehaviour {

	//Variables
	public GameObject go_MoltenRock;
	private float fl_EruptionDelay = 3.5f; 
	private float fl_EruptionCooldown;
	public float fl_LavaRockInterval;
	public float fl_LavaRockCooldown = 2f;
	private ParticleSystem ps_Volcano;
	public bool bl_Erupt = false;
	public MPI_Level_Manager MPI_LMScript;


	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		ps_Volcano = GetComponentInChildren<ParticleSystem>();
		fl_EruptionCooldown = fl_EruptionDelay + Time.realtimeSinceStartup;
		AttackStages (MPI_LMScript.st_Difficulty);
	}

	void Update()
	{
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			if (GetComponent<Renderer>().isVisible)
			{
				Eruption();
			}
		}
	}

	void Eruption()
	{
		if(bl_Erupt)
		{
			ps_Volcano.Play();
		}

		if(!bl_Erupt)
		{
			ps_Volcano.Stop();
			if(fl_LavaRockCooldown < Time.realtimeSinceStartup)
			{
				GameObject go_LavaRockClone;
				go_LavaRockClone = (GameObject) Instantiate(go_MoltenRock, new Vector3(Random.Range (0, 25), 15, 0), transform.rotation);
				fl_LavaRockCooldown = fl_LavaRockInterval + Time.realtimeSinceStartup;
			}
		}

		if(fl_EruptionCooldown < Time.realtimeSinceStartup)
		{
			bl_Erupt = !bl_Erupt;
			fl_EruptionCooldown = fl_EruptionDelay + Time.realtimeSinceStartup;
		}

	}

	void AttackStages(MPI_Level_Manager.DifficultyLevel dl_Level)
	{
		switch (dl_Level)
		{
		case MPI_Level_Manager.DifficultyLevel.Easy:
			fl_LavaRockInterval = 1.3f;
			break;

		case MPI_Level_Manager.DifficultyLevel.Medium:
			fl_LavaRockInterval = 1f;
			break;

		case MPI_Level_Manager.DifficultyLevel.Hard:
			fl_LavaRockInterval = 0.75f;
			break;
		}
	}

}
