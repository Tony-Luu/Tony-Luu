using UnityEngine;
using System.Collections;

public class TL_CyclopScript : MonoBehaviour {

	//Variables
	public GameObject go_Boulder01;
	public GameObject go_Boulder02;
	public GameObject go_Boulder03;
	private int in_Attack = 5;
	private float fl_AttackDelay = 1.7f;
	private float fl_AttackCooldown;
	public MPI_Level_Manager MPI_LMScript;
	private GameObject go_BoulderClone;
	private int in_Random;


	void Start()
	{
		MPI_LMScript = GameObject.Find ("MPI_Level_Manager").GetComponent<MPI_Level_Manager>();
	}

	void Update()
	{
		if(MPI_LMScript.st_CurrentState != MPI_Level_Manager.GameStates.Paused)
		{

		}
	}

	public void BoulderSpawn()
	{
		in_Random = Random.Range (1, 4);
		switch(in_Random)
		{
		case 1:
			go_BoulderClone = (GameObject) Instantiate(go_Boulder01, new Vector3(transform.position.x - 3f, transform.position.y + 1.5f, 0), transform.rotation);
			break;

		case 2:
			go_BoulderClone = (GameObject) Instantiate(go_Boulder02, new Vector3(transform.position.x - 3f, transform.position.y + 1.5f, 0), transform.rotation);
			break;

		case 3:
			go_BoulderClone = (GameObject) Instantiate(go_Boulder03, new Vector3(transform.position.x - 3f, transform.position.y + 1.5f, 0), transform.rotation);
			break;
		}
	}

	public void BoulderMove()
	{
		go_BoulderClone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 150f);
	}

	public void BoulderArcAngle()
	{
		go_BoulderClone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 150f);
		Vector2 v2_Arc = new Vector2(0.27f, 0.35f);
		go_BoulderClone.GetComponent<Rigidbody2D>().velocity = v2_Arc * 17f;
	}

	public void BoulderThrow()
	{
		go_BoulderClone.GetComponent<Collider2D>().enabled = true;
		go_BoulderClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		go_BoulderClone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 1000f);
	}

}
