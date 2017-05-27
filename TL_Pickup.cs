using UnityEngine;

public class TL_Pickup : MonoBehaviour {

    //Variables
    public GameObject WeightPickupMessage;
    public bool bl_PickUp = false;
	private GameObject[] go_Weight;    
    private TL_PreventPlacement WeightScript;
    
    
	void Update()
	{
        //Function for picking up and dropping weights
        PickUpToggle();
	}

	void PickUpToggle()
	{
		//Assigns a Vector3 variable with a TransformDirection of Vector3.forward for the raycast
		Vector3 v3_Forward = transform.TransformDirection(Vector3.forward * 2f);

        //Creates a ray in front of the PC
		Ray ry_Ray = new Ray(new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z), v3_Forward);

        //Raycast hit variable for checking collision with the ray
		RaycastHit rh_RayHit;

		if(Physics.Raycast(ry_Ray, out rh_RayHit, 2f))
		{
            //When the raycast hits the Weight object
            if (rh_RayHit.transform.gameObject.tag == "Weight")
			{
				//Obtains the script from the weight object
				WeightScript = rh_RayHit.transform.gameObject.GetComponent<TL_PreventPlacement>();

				//If the E button and the bool is true to allow placement
				if(Input.GetKeyDown(KeyCode.E) && WeightScript.bl_AllowPlacement)
				{
					//Boolean toggle
					bl_PickUp = !bl_PickUp;
					if(bl_PickUp)
					{
						//Adds the object touched by the raycast to the parent transform
						rh_RayHit.transform.SetParent(transform);

						//Freezes all positions and rotations
						rh_RayHit.rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

						//Sets local position
						rh_RayHit.transform.localPosition = new Vector3 (0, -0.2f, 1.5f);

						//Sets local rotation
						rh_RayHit.transform.localRotation = new Quaternion (0, 0, 0, 0);
					}
					else
					{
						//Sets rotations all to 0
						rh_RayHit.transform.rotation = new Quaternion (0, 0, 0, 0);

						//Freezes only the positions
						rh_RayHit.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

						//Sets the parent to null
						rh_RayHit.transform.SetParent(null);
					}
				}
			}
		}

	}

}
