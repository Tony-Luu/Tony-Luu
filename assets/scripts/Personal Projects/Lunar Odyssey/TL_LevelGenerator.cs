using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TL_LevelGenerator : MonoBehaviour {

	//Variables
	private GameObject go_PC;
	private Vector3 v3_StartingPCPos;
	public int in_Randomizer;
	public List<Vector3> lt_StartingPos = new List<Vector3>();
	private List<GameObject> lt_GameObjects = new List<GameObject>();
	private GameObject[] go_Planes;

	void Start()
	{
		go_PC = GameObject.Find ("Camera");
		v3_StartingPCPos = go_PC.transform.position;

		go_Planes = GameObject.FindGameObjectsWithTag("Plane");
		foreach (GameObject go in go_Planes)
		{
			lt_GameObjects.Add(go);
			lt_StartingPos.Add(go.transform.position);
		}
	}

	void OnTriggerEnter(Collider col_obj)
	{
		if(col_obj.gameObject.name == "Camera")
		{
			foreach (GameObject go in go_Planes)
			{
				lt_GameObjects.Remove(go);
				Destroy (go);
			}

			for(int i = 0; i < lt_GameObjects.Count; i++)
			{
				int in_RandomIndex = Random.Range (0, 6);
				if(lt_GameObjects[in_RandomIndex] == null)
				{
					GameObject go_PlaneClone = (GameObject) Instantiate (lt_GameObjects[i].gameObject, new Vector3(lt_StartingPos[in_RandomIndex].x, lt_StartingPos[in_RandomIndex].y, lt_StartingPos[in_RandomIndex].z), Quaternion.identity);
					lt_GameObjects.Add(go_PlaneClone);
				}
				else
				{
					in_RandomIndex = Random.Range (0, 6);
				}
			}
		}
	}

}
