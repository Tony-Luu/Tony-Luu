using UnityEngine;
using System.Collections;

public class TL_NPCBoat : MonoBehaviour {

	//Variables
	private GameObject go_Water;
	private Rigidbody2D rb2d_NPCBoat;
	private float fl_NPCBoatYPos;


	void Start()
	{
		go_Water = GameObject.Find ("GO_Water");
		rb2d_NPCBoat = GetComponent<Rigidbody2D>();
		fl_NPCBoatYPos = go_Water.transform.position.y + 9f;
	}
	
	
	void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x, go_Water.transform.position.y + 8.5f, 0);
		if(transform.position.y < fl_NPCBoatYPos)
		{
			ApplyUpForce(rb2d_NPCBoat);
		}
	}
	
	void ApplyUpForce(Rigidbody2D body)
	{
		body.AddForce(transform.up * 15);
		Debug.Log("Up");
	}
	
}
