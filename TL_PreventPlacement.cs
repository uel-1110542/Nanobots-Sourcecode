using UnityEngine;

public class TL_PreventPlacement : MonoBehaviour {

	public bool bl_AllowPlacement = true;
	private TL_Pickup PickupScript;
	private Rigidbody rb_Gravity;


	void Start()
	{
        //Find the gameobject and obtain the script
		PickupScript = GameObject.Find ("PC").GetComponent<TL_Pickup>();

        //Obtain the rigidbody component from this gameobject
		rb_Gravity = GetComponent<Rigidbody>();
	}

	void Update()
	{
        //If the pickup bool is false, enable gravity
        if (!PickupScript.bl_PickUp)
		{
			rb_Gravity.useGravity = true;
		}
	}

	void OnCollisionStay(Collision col_)
	{
		//If the gameobject is within another weight, disable the pickup
		if(col_.gameObject.tag == "Weight" && PickupScript.bl_PickUp)
		{
			bl_AllowPlacement = false;
		}
	}

	void OnCollisionExit(Collision col_)
	{
		//If the gameobject is not within another weight, enable the pickup
		if(col_.gameObject.tag == "Weight" && PickupScript.bl_PickUp)
		{
			bl_AllowPlacement = true;
		}
	}

}
