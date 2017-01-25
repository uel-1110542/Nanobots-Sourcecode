using UnityEngine;
using System.Collections;

public class TL_KeyCard : MonoBehaviour {


	
	void Update()
	{
		//Rotates the Y-axis
		transform.Rotate (0, 90f * Time.deltaTime, 0);
	}

}
