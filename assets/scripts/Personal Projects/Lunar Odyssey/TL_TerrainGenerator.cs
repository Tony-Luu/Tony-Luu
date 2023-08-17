using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TL_TerrainGenerator : MonoBehaviour {

	//Variables
	public List<GameObject> lt_Terrain01 = new List<GameObject>();
	public List<GameObject> lt_Terrain02 = new List<GameObject>();
	public List<GameObject> lt_Terrain03 = new List<GameObject>();
	public List<GameObject> lt_Terrain04 = new List<GameObject>();
	public List<GameObject> lt_goTerrain = new List<GameObject>();
	public float fl_EndLeft;
	public bool bl_Restart = false;

	private float fl_SpawnDelay = 0.64f;
	private float fl_SpawnCooldown;
	private int in_index;
	public int in_Count;


	void Start()
	{
		Reinitialize();
	}

	void Update()
	{
		TerrainGeneration();
	}

	public void Reinitialize()
	{
		if(!bl_Restart)
		{
			lt_goTerrain.Clear();
			in_Count = Random.Range(15, 25);
			GameObject go_TerrainPlane;
			go_TerrainPlane = (GameObject) Instantiate (lt_Terrain01[0].gameObject, new Vector3 (48.5f, -4f, 0f), Quaternion.identity);
			lt_goTerrain.Add (go_TerrainPlane);
			bl_Restart = true;
		}
	}

	void TerrainGeneration()
	{
		if(bl_Restart)
		{
			GameObject[] go_TerrainCount = GameObject.FindGameObjectsWithTag("Landscape");
			GameObject go_TerrainClone;
			int in_random;

			if (fl_SpawnCooldown < Time.realtimeSinceStartup && go_TerrainCount.Length < in_Count)
			{
				in_random = Random.Range (0, 100);
				switch(lt_goTerrain[lt_goTerrain.Count-1].gameObject.name)
				{
				case "pf_Terrain01(Clone)":
					if(in_random >= 20)
					{
						in_index = 1;
					}
					else
					{
						in_index = 0;
					}
					go_TerrainClone = (GameObject) Instantiate (lt_Terrain01[in_index].gameObject, new Vector3 (50f, -4f, 0f), Quaternion.identity);
					lt_goTerrain.Add(go_TerrainClone);
					break;
					
				case "pf_Terrain02(Clone)":
					if(in_random >= 50)
					{
						in_index = 2;
					}
					else if(in_random >= 25)
					{
						in_index = 1;
					}
					else
					{
						in_index = 0;
					}
					go_TerrainClone = (GameObject) Instantiate (lt_Terrain02[in_index].gameObject, new Vector3 (50f, -4f, 0f), Quaternion.identity);
					lt_goTerrain.Add(go_TerrainClone);
					break;
					
				case "pf_Terrain03(Clone)":
					if(in_random >= 50)
					{
						in_index = 2;
					}
					else if(in_random >= 40)
					{
						in_index = 1;
					}
					else if(in_random >= 30)
					{
						in_index = 0;
					}
					go_TerrainClone = (GameObject) Instantiate (lt_Terrain03[in_index].gameObject, new Vector3 (50f, -4f, 0f), Quaternion.identity);
					lt_goTerrain.Add(go_TerrainClone);
					break;
					
				case "pf_Terrain04(Clone)":
					if(in_random >= 70)
					{
						in_index = 1;
					}
					else
					{
						in_index = 0;
					}
					go_TerrainClone = (GameObject) Instantiate (lt_Terrain04[in_index].gameObject, new Vector3 (50f, -4f, 0f), Quaternion.identity);
					lt_goTerrain.Add(go_TerrainClone);
					break;
				}
				fl_SpawnCooldown = fl_SpawnDelay + Time.realtimeSinceStartup;
			}
		}
	}

}
