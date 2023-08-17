using UnityEngine;
using System.Collections;

public class TL_MoveObjs : MonoBehaviour {

	public MPI_Level_Manager MPI_LMScript;
	public float fl_EndLeft;
	public float fl_EndBottom;
	//private TL_VolcanoScript VolcanoScript;


	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
	}

	void Update()
	{
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			transform.Translate (Vector3.left * 2.5f * Time.deltaTime);
		}

		if (transform.position.x <= fl_EndLeft || transform.position.y <= fl_EndBottom)
		{
			GameObject[] go_LavaRocks = GameObject.FindGameObjectsWithTag("MoltenRock");
			foreach(GameObject go_ in go_LavaRocks)
			{
				Destroy (go_);
			}
			Destroy(gameObject);
		}
	}

}
