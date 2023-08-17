using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TL_GenerationController : MonoBehaviour {

	//Variables
	public float fl_GenerationDelay = 25f;
	public float fl_GenerationCooldown = 25f;
	private TL_TerrainGenerator TerrainScript;
	private TL_CameraMove CameraScript;
	private int in_index;
	public bool bl_Toggle = false;



	void Awake()
	{
		TerrainScript = GetComponent<TL_TerrainGenerator>();
		CameraScript = GetComponent<TL_CameraMove>();
		CameraScript.enabled = false;
	}

	void Update()
	{
		TriggerGeneration();
	}

	void TriggerGeneration()
	{
		if(fl_GenerationCooldown < Time.realtimeSinceStartup)
		{
			GameObject[] go_Planes = GameObject.FindGameObjectsWithTag("Plane");
			bl_Toggle = !bl_Toggle;
			GetComponent<TL_TerrainGenerator>().enabled = bl_Toggle;
			GetComponent<TL_CameraMove>().enabled = !bl_Toggle;
			if(bl_Toggle)
			{
				foreach(GameObject _go in go_Planes)
				{
					Destroy(_go);
				}
				TerrainScript.bl_Restart = false;
				TerrainScript.Reinitialize();
			}
			else
			{
				CameraScript.bl_Restart = false;
				CameraScript.RecreatePlane();
			}
			fl_GenerationCooldown = fl_GenerationDelay + Time.realtimeSinceStartup;
		}
	}

}
