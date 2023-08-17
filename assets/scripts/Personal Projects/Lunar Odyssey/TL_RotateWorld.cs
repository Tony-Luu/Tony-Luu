using UnityEngine;
using System.Collections;

public class TL_RotateWorld : MonoBehaviour {

	public MPI_Level_Manager MPI_LMScript;
	
	
	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
	}

	void Update()
	{
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			transform.Rotate(0f, 5f * Time.deltaTime, 0f);
		}
	}

}
