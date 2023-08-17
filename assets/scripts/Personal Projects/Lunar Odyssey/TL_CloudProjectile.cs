using UnityEngine;
using System.Collections;

public class TL_CloudProjectile : MonoBehaviour {

	//Variables
	public GameObject go_Projectile;
	public float fl_Delay;
	public float fl_Angle;
	private float fl_Cooldown;
	public Vector2 v2_Angle;
	public MPI_Level_Manager MPI_LMScript;
	private GameObject go_Boat;
	
	
	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
		go_Boat = GameObject.Find("GO_Boat");
	}
	
	void Update()
	{
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{
			GenerateProjectile();
		}
	}
	
	void GenerateProjectile()
	{
		if (Vector3.Distance (transform.position, go_Boat.transform.position) >= 2f)
		{
			if (fl_Cooldown < Time.realtimeSinceStartup)
			{
				GameObject go_ProjectileClone;
				go_ProjectileClone = (GameObject) Instantiate(go_Projectile, new Vector3(transform.position.x - go_Projectile.transform.localScale.x, transform.position.y - go_Projectile.transform.localScale.y, transform.position.z), transform.rotation);
				go_ProjectileClone.transform.eulerAngles = new Vector3(0, 0, fl_Angle);
				
				v2_Angle.Normalize();
				go_ProjectileClone.GetComponent<Rigidbody2D>().velocity = v2_Angle * 25f;
				fl_Cooldown = fl_Delay + Time.realtimeSinceStartup;
			}	
		}
	}

}
