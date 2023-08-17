using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TL_CameraMove : MonoBehaviour {

	//Variables
	public GameObject[] go_PlaneType1 = new GameObject[2];
	public GameObject[] go_PlaneType2 = new GameObject[2];
	public GameObject[] go_PlaneType3 = new GameObject[2];
	public GameObject[] go_PlaneType4 = new GameObject[2];
	public GameObject[] go_NPCBoats = new GameObject[3];
	private GameObject go_Plane;
	private GameObject go_CreateObj;
	private Plane[] pl_CameraPlane;
	public List<GameObject> lt_PlaneObj = new List<GameObject>();
	private Vector3 v3_CameraViewPos;
	private int in_Stages = 0;
	private int in_Index = 0;
	private int in_Random = 0;
	private int in_BoatSpawn;
	private int in_BoatTypes;

	public GameObject go_Volcano;
	public GameObject go_Cyclops;
	public GameObject go_Thunder;
	public GameObject go_Kraken;
	private GameObject go_Water;
	private GameObject[] go_TentacleClone;
	public bool bl_Restart = false;
	private MPI_Level_Manager LMScript;


	void Start()
	{
		LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		go_Water = GameObject.Find ("GO_Water");
		RecreatePlane();
	}

	public void RecreatePlane()
	{
		if (!bl_Restart)
		{
			in_Random = Random.Range (0, 4);
			switch(in_Random)
			{
			case 0:
				for (int i = 0; i < go_PlaneType1.Length; i++)
				{
					GameObject go_PlaneClone = (GameObject) Instantiate (go_PlaneType1[i], new Vector3(transform.position.x + (2.1f * i), transform.position.y, 0f), Quaternion.identity);
					lt_PlaneObj.Add(go_PlaneClone);
				}
				break;
				
			case 1:
				for (int i = 0; i < go_PlaneType2.Length; i++)
				{
					GameObject go_PlaneClone = (GameObject) Instantiate (go_PlaneType2[i], new Vector3(transform.position.x + (2.1f * i), transform.position.y, 0f), Quaternion.identity);
					lt_PlaneObj.Add(go_PlaneClone);
				}
				break;
				
			case 2:
				for (int i = 0; i < go_PlaneType3.Length; i++)
				{
					GameObject go_PlaneClone = (GameObject) Instantiate (go_PlaneType3[i], new Vector3(transform.position.x + (2.1f * i), transform.position.y, 0f), Quaternion.identity);
					lt_PlaneObj.Add(go_PlaneClone);
				}
				break;
				
			case 3:
				for (int i = 0; i < go_PlaneType4.Length; i++)
				{
					GameObject go_PlaneClone = (GameObject) Instantiate (go_PlaneType4[i], new Vector3(transform.position.x + (2.1f * i), transform.position.y, 0f), Quaternion.identity);
					lt_PlaneObj.Add(go_PlaneClone);
				}
				break;
			}
			bl_Restart = true;
		}
	}

	void Update()
	{
		RendererManager();
	}

	void RendererManager()
	{
		GameObject go_VolcanoPrefab = GameObject.Find ("pf_Volcano(Clone)");
		GameObject go_CyclopsPrefab = GameObject.Find ("pf_Cyclops(Clone)");
		GameObject go_KrakenPrefab = GameObject.Find ("pf_Kraken(Clone)");
		GameObject[] go_PlaneClone = GameObject.FindGameObjectsWithTag ("Plane");
		go_TentacleClone = GameObject.FindGameObjectsWithTag("Tentacle");
		pl_CameraPlane = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());

		foreach(GameObject go in go_PlaneClone)
		{
			if(!GeometryUtility.TestPlanesAABB(pl_CameraPlane, go.GetComponent<Collider>().bounds))
			{
				GameObject go_NPCBoatClone;
				in_Random = Random.Range (0, 4);
				switch(in_Random)
				{
				case 0:
					go_CreateObj = (GameObject) Instantiate (go_PlaneType1[in_Index], Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height / 2f, 25f)), Quaternion.identity);
					in_Index++;
					if(in_Index > 1)
					{
						in_Index = 0;
						in_Random = Random.Range (0, 4);
						break;
					}
					break;
					
				case 1:
					go_CreateObj = (GameObject) Instantiate (go_PlaneType2[in_Index], Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height / 2f, 25f)), Quaternion.identity);
					in_Index++;
					if(in_Index > 1)
					{
						in_Index = 0;
						in_Random = Random.Range (0, 4);
						break;
					}
					break;
					
				case 2:
					go_CreateObj = (GameObject) Instantiate (go_PlaneType3[in_Index], Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height / 2f, 25f)), Quaternion.identity);
					in_Index++;
					if(in_Index > 1)
					{
						in_Index = 0;
						in_Random = Random.Range (0, 4);
						break;
					}
					break;
					
				case 3:
					go_CreateObj = (GameObject) Instantiate (go_PlaneType4[in_Index], Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height / 2f, 25f)), Quaternion.identity);
					in_Index++;
					if(in_Index > 1)
					{
						in_Index = 0;
						in_Random = Random.Range (0, 4);
						break;
					}
					break;
				}

				switch(go.name)
				{
				case "pf_VolcanoPlane(Clone)":
					if(go_VolcanoPrefab == null)
					{
						GameObject go_VolcanoClone;
						go_VolcanoClone = (GameObject) Instantiate (go_Volcano, Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height / 3f, 30f)), Quaternion.identity);
						in_BoatSpawn = Random.Range (1, 6);
						in_BoatTypes = Random.Range (1, 3);
						if(in_BoatSpawn >= 4)
						{
							go_NPCBoatClone = (GameObject) Instantiate (go_NPCBoats[in_BoatTypes], new Vector3 (40f, go_Water.transform.position.y + 10f, 0f), Quaternion.identity);
							go_NPCBoatClone.GetComponentInChildren<TextMesh>().GetComponent<Renderer>().enabled = false;
						}
					}
					break;

				case "pf_CyclopsPlane(Clone)":
					if(go_CyclopsPrefab == null)
					{
						GameObject go_CyclopsClone;
						go_CyclopsClone = (GameObject) Instantiate (go_Cyclops, Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0, 30f)), Quaternion.identity);
					}
					break;

				case "pf_ThunderPlane(Clone)":
					GameObject go_ThunderClone;
					if(LMScript.st_Difficulty == MPI_Level_Manager.DifficultyLevel.Easy)
					{
						go_ThunderClone = (GameObject) Instantiate (go_Thunder, new Vector3 (10f, 11f, 0f), Quaternion.identity);
					}
					else if(LMScript.st_Difficulty == MPI_Level_Manager.DifficultyLevel.Medium)
					{
						for(int i = 0; i < 2; i++)
						{
							go_ThunderClone = (GameObject) Instantiate (go_Thunder, new Vector3 (Random.Range (10f * i, 12f * i), 11f, 0f), Quaternion.identity);
						}
					}
					else if(LMScript.st_Difficulty == MPI_Level_Manager.DifficultyLevel.Hard)
					{
						go_ThunderClone = (GameObject) Instantiate (go_Thunder, new Vector3 (8f, 11f, 0f), Quaternion.identity);
						for(int i = 0; i < 2; i++)
						{
							go_ThunderClone = (GameObject) Instantiate (go_Thunder, new Vector3 (Random.Range (10f * i, 12f * i), 11f, 0f), Quaternion.identity);
						}
					}
					in_BoatSpawn = Random.Range (1, 6);
					in_BoatTypes = Random.Range (1, 3);
					if(in_BoatSpawn >= 4)
					{
						go_NPCBoatClone = (GameObject) Instantiate (go_NPCBoats[in_BoatTypes], new Vector3 (40f, go_Water.transform.position.y + 10f, 0f), Quaternion.identity);
						go_NPCBoatClone.GetComponentInChildren<TextMesh>().GetComponent<Renderer>().enabled = false;
					}
					break;

				case "pf_KrakenPlane(Clone)":
					if(go_KrakenPrefab == null)
					{
						GameObject go_KrakenClone;
						go_KrakenClone = (GameObject) Instantiate (go_Kraken, new Vector3 (20f + (go_Kraken.transform.localScale.x * 3f), go_Water.transform.position.y + 7f, 0f), Quaternion.identity);
						in_BoatSpawn = Random.Range (1, 6);
						in_BoatTypes = Random.Range (1, 3);
						if(in_BoatSpawn >= 4)
						{
							go_NPCBoatClone = (GameObject) Instantiate (go_NPCBoats[in_BoatTypes], new Vector3 (40f, go_Water.transform.position.y + 10f, 0f), Quaternion.identity);
							go_NPCBoatClone.GetComponentInChildren<TextMesh>().GetComponent<Renderer>().enabled = false;
						}
					}
					break;
				}
				lt_PlaneObj.Remove(go);
				Destroy(go);
				lt_PlaneObj.Add(go_CreateObj);
			}
		}
	}

}
