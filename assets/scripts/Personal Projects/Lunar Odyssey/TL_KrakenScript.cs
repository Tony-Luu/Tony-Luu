using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TL_KrakenScript : MonoBehaviour {

	//Variables
	public GameObject go_Tentacle;
	public MPI_Level_Manager MPI_LMScript;
	private GameObject go_Water;
	private bool bl_Spawned = false;
	private TL_TentacleThrust TentacleScript;
	public float fl_Cooldown;
	public List<GameObject> lt_Tentacles = new List<GameObject>();
	private float fl_StageCooldown = 1.5f;
	private bool bl_Activate;


	void Start()
	{
		go_Water = GameObject.Find ("GO_Water");
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		TentacleSpawn();
	}

	void Update()
	{		
		TentacleStages (MPI_LMScript.st_Difficulty);
		if(transform.position.x >= 21f)
		{
			transform.Translate(-3f * Time.deltaTime, 0, 0);
		}
		
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			Disappear();
		}
	}

	void TentacleSpawn()
	{
		GameObject go_TentacleClone;
		if(!bl_Spawned)
		{
			go_TentacleClone = (GameObject) Instantiate(go_Tentacle, new Vector3(transform.position.x - 10f, go_Water.transform.position.y - 2f, 0f), Quaternion.identity);
			lt_Tentacles.Add (go_TentacleClone);

			go_TentacleClone = (GameObject) Instantiate(go_Tentacle, new Vector3(transform.position.x - 20f, go_Water.transform.position.y - 2f, 0f), Quaternion.identity);
			lt_Tentacles.Add (go_TentacleClone);

			go_TentacleClone = (GameObject) Instantiate(go_Tentacle, new Vector3(transform.position.x - 30f, go_Water.transform.position.y - 2f, 0f), Quaternion.identity);
			lt_Tentacles.Add (go_TentacleClone);

			TentacleScript = go_TentacleClone.GetComponent<TL_TentacleThrust>();
			bl_Spawned = true;
		}
	}

	void TentacleStages(MPI_Level_Manager.DifficultyLevel dl_States)
	{
		switch(dl_States)
		{			
		case MPI_Level_Manager.DifficultyLevel.Medium:
			if(fl_StageCooldown < Time.realtimeSinceStartup)
			{
				bl_Activate = !bl_Activate;
				if(lt_Tentacles[0].gameObject != null)
				{
					TentacleScript = lt_Tentacles[0].GetComponent<TL_TentacleThrust>();
					TentacleScript.enabled = bl_Activate;
				}
				
				if(lt_Tentacles[2].gameObject != null)
				{
					TentacleScript = lt_Tentacles[2].GetComponent<TL_TentacleThrust>();
					TentacleScript.enabled = bl_Activate;
				}
				fl_StageCooldown = 1.5f + Time.realtimeSinceStartup;
			}
		break;		

		case MPI_Level_Manager.DifficultyLevel.Hard:
			if(fl_StageCooldown < Time.realtimeSinceStartup)
			{
				bl_Activate = !bl_Activate;
				if(lt_Tentacles[0].gameObject != null)
				{
					TentacleScript = lt_Tentacles[0].GetComponent<TL_TentacleThrust>();
					TentacleScript.enabled = !bl_Activate;
				}

				if(lt_Tentacles[1].gameObject != null)
				{
					TentacleScript = lt_Tentacles[1].GetComponent<TL_TentacleThrust>();
					TentacleScript.enabled = bl_Activate;
				}

				if(lt_Tentacles[2].gameObject != null)
				{
					TentacleScript = lt_Tentacles[2].GetComponent<TL_TentacleThrust>();
					TentacleScript.enabled = bl_Activate;
				}
				fl_StageCooldown = 1.5f + Time.realtimeSinceStartup;
			}
			break;
		}
	}

	void Disappear()
	{
		if(bl_Spawned)
		{
			if(TentacleScript.in_Amount >= 2)
			{
				foreach(GameObject go_ in lt_Tentacles)
				{
					Destroy (go_);
				}
				Vector2 v2_Arc = new Vector2(-0.1f, -0.25f);
				transform.GetComponent<Rigidbody2D>().velocity = v2_Arc * 25f;
				if(transform.position.y <= -18f)
				{
					Destroy (gameObject);
				}
			}
			else
			{
				transform.position = new Vector3(transform.position.x, go_Water.transform.position.y + 7f, 0);
			}
		}
	}

}
