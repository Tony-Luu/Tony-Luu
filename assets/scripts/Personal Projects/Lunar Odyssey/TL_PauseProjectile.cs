using UnityEngine;
using System.Collections;

public class TL_PauseProjectile : MonoBehaviour {

	public MPI_Level_Manager MPI_LMScript;
	
	
	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
	}

	void Update()
	{
		if(MPI_LMScript.st_CurrentState == MPI_Level_Manager.GameStates.Paused)
		{
			if(GetComponent<Rigidbody>())
			{
				Destroy (gameObject.GetComponent("Rigidbody2D"));
			}
			else
			{
				gameObject.AddComponent<Rigidbody2D>();
				gameObject.GetComponent<Rigidbody>().useGravity = true;
			}
		}
	}

}
