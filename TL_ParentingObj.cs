using UnityEngine;
using System.Collections;

public class TL_ParentingObj : MonoBehaviour {




	void OnTriggerEnter(Collider col_Obj)
	{
		if(col_Obj.gameObject.tag == "Destructible")
		{
			//Childs itself to the object
			transform.parent = col_Obj.transform;
		}
	}

}
