using UnityEngine;
using System.Collections;

public class TL_SpawnObstacles : MonoBehaviour {

	//Variables
	public GameObject go_Volcano;
	public GameObject go_Cyclops;
	public GameObject go_Bolt;
	public int in_Randomizer = 0;


	void OnTriggerEnter(Collider col_obj)
	{
		if(col_obj.tag == "MainCamera")
		{
			in_Randomizer = 3;
			switch(in_Randomizer)
			{
			case 1:
				GameObject go_VolcanoClone = (GameObject) Instantiate(go_Volcano, new Vector3(col_obj.transform.position.x + 40f, 0, 40f), Quaternion.identity);
				break;

			case 2:
				GameObject go_CyclopsClone = (GameObject) Instantiate(go_Cyclops, new Vector3(col_obj.transform.position.x + 40f, 0, -3f), Quaternion.identity);
				break;

			case 3:
				GameObject go_LightningClone = (GameObject) Instantiate(go_Bolt, new Vector3(col_obj.transform.position.x + 40f, 0, 10f), Quaternion.identity);
				break;
			}

		}

	}

}
