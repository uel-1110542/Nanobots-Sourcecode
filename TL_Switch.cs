using UnityEngine;
using System.Collections;

public class TL_Switch : MonoBehaviour {

	//Variables
	public bool bl_Activate = false;



	void OnCollisionEnter(Collision _col)
	{
		//If the weight is on top of the switch then set the bool to true
		if( _col.gameObject.tag == "Weight")
		{
			bl_Activate = true;
			transform.localScale = new Vector3 (transform.localScale.x, 0.01f, transform.localScale.z);
		}
	}

	void OnCollisionExit(Collision _col)
	{
		//If the weight is not on top of the switch then set the bool to false
		if(_col.gameObject.tag == "Weight")
		{
			bl_Activate = false;
			transform.localScale = new Vector3 (transform.localScale.x, 0.1f, transform.localScale.z);
		}
	}

}
