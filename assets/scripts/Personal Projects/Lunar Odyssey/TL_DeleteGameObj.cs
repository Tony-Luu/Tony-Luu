using UnityEngine;
using System.Collections;

public class TL_DeleteGameObj : MonoBehaviour {



	void Update()
	{
		if(!GetComponent<Renderer>().isVisible)
		{
			Destroy(gameObject);
		}
	}

}
