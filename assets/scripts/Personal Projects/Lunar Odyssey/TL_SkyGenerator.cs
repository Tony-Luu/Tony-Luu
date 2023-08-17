using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TL_SkyGenerator : MonoBehaviour {

	//Variables
	public List<GameObject> lt_CloudObj = new List<GameObject>();
	public MPI_Level_Manager MPI_LMScript;
	private GameObject[] go_Clouds;
	private float fl_SpawnDelay = 10f;
	private float fl_SpawnCooldown;


	
	void Start()
	{
		GameObject go_Cloud;
		go_Cloud = (GameObject)Instantiate (lt_CloudObj[0], Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 330f, 20f)), Quaternion.identity);
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		fl_SpawnCooldown = 2.5f + Time.realtimeSinceStartup;
	}

	void Update()
	{
		CloudGeneration();
	}

	void CloudGeneration()
	{
		go_Clouds = GameObject.FindGameObjectsWithTag("Cloud");
		GameObject go_CloudClone;
		
		foreach(GameObject _go in go_Clouds)
		{
			if (fl_SpawnCooldown < Time.realtimeSinceStartup)
			{
				int in_index = Random.Range (0, 2);
				go_CloudClone = (GameObject) Instantiate (lt_CloudObj[in_index], Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 20f, 330f, 20f)), Quaternion.identity);
				fl_SpawnCooldown = fl_SpawnDelay + Time.realtimeSinceStartup;
			}
		}
	}

}
