using UnityEngine;
using System.Collections;

public class TL_PlaneCollision : MonoBehaviour {




	void OnTriggerEnter(Collider col_Plane)
	{
		if (col_Plane.gameObject.tag == "MoltenRock")
		{
			Debug.Log ("got hit by lava!!");
		}

		if (col_Plane.gameObject.tag == "Boulder")
		{
			Debug.Log ("got hit by a boulder!!");
		}

		if (col_Plane.gameObject.tag == "Bolt")
		{
			Debug.Log ("got hit by lightning!!");
		}
	}
}
