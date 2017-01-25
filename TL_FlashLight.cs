using UnityEngine;

public class TL_FlashLight : MonoBehaviour {

    //Variables
    public GameObject PopupMessage;
	private GameObject go_PC;
	private MeshCollider mh_Collider;
	private Light lt_FlashLight;
	private bool bl_PickedUp = false;
	private bool bl_Toggle = false;
	private float fl_Distance;
	private Camera cam_Main;
	private Color col_Opacity;



	void Start()
	{
        //Find the PC
		go_PC = GameObject.Find ("PC");

        //Obtain the mesh collider from this gameobject
		mh_Collider = GetComponent<MeshCollider>();

        //Obtain the light component from this gameobject
		lt_FlashLight = GetComponent<Light>();
	}

	void Update()
	{
        //If the flashlight hasn't been picked up
		if (!bl_PickedUp)
		{
            //Slowly rotates on the Y-axis to indicate the object is a pickup, aesthetic only
            transform.Rotate (0, 45 * Time.deltaTime, 0);

            //Pick up the flashlight
			PickedUp();
		}
		else
		{
			//If the transform parent is not null, obtain the camera from the parent for the raycast
			if(transform.parent != null)
			{
				cam_Main = GetComponentInParent<Camera>();
			}
            //Function for the flashlight
			FlashLight();
		}
	}


	void PickedUp()
	{
        //Calculate the distance between the PC and this gameobject
        fl_Distance = Vector3.Distance(go_PC.transform.position, transform.position);

        //If the flashlight hasn't been picked up and the player has pressed E and it is close to the player
        if (!bl_PickedUp && Input.GetKeyDown("e") && fl_Distance <= 3f)
		{
			//Parents itself to the PC and sets new local position and euler angles
			transform.parent = go_PC.transform;
			transform.localEulerAngles = new Vector3(0, 0, 0);
			transform.localPosition = new Vector3(0.5f, 0.5f, 1f);

            //Destroy the collider
			Destroy (mh_Collider);

            //Set the bool to true
			bl_PickedUp = true;
		}
	}

	void FlashLight()
	{
        //Turn the light on
        lt_FlashLight.enabled = true;

        //Create ray
        Ray ry_Ray = new Ray(transform.position, transform.forward * 5f);
        RaycastHit rh_RayHit;

        if (Physics.Raycast(ry_Ray, out rh_RayHit, 5f))
        {
            //Debugging purposes only
            Debug.DrawRay(ry_Ray.origin, ry_Ray.direction * 5f, Color.red);

            //If the raycast hits a tagged gameobject called invisible then slowly change the opacity of the gameobject
            if (rh_RayHit.transform.gameObject.tag == "Invisible")
            {
                col_Opacity = rh_RayHit.transform.gameObject.GetComponent<Renderer>().material.color;
                col_Opacity.a += 0.5f * Time.deltaTime;
                rh_RayHit.transform.gameObject.GetComponent<Renderer>().material.color = col_Opacity;
            }
        }

    }

}
