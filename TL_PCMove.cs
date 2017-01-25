using UnityEngine;

public class TL_PCMove : MonoBehaviour {

	// Variables 	==================================================================================
	private CharacterController cc_PC;
	public float fl_speed = 6; 
	public float fl_turn_rate = 90; 
	public float fl_jump_force = 5;
	public float fl_gravity = 20;
    public bool bl_PC_carrying = false;
    public Vector3 v3_Respawn;

    private Vector3 v3_direction;
	private TL_Restart RestartScript;

	// Camera 
	public float fl_min_cam_height = -1F;
	public float fl_max_cam_height = 3F;
	public float fl_cam_distance = -2.5F;
	public float fl_Xrotation;

	private GameObject go_cam;
	private GameObject go_Emitter;
	private GameObject go_Shield;

	// GUI Stuff
	private string st_PC_GUI_text;	
	
	
	
	// Run Once when Scene is first Loaded	 =========================================================
	void Start ()
	{				
		// Find Game Objects
		go_cam = GameObject.Find("Camera");	
		go_Emitter = transform.FindChild("Nanobot_Emitter").gameObject;
		go_Shield = transform.FindChild("pf_NanobotShield").gameObject;
		cc_PC = GetComponent<CharacterController>();

		RestartScript = GameObject.Find ("Laser_Room").GetComponent<TL_Restart>();

		// Set initial values and game states 
		v3_direction = Vector3.zero;
		v3_Respawn = transform.position;
	}	//-----
	
	
	// Update is called once per frame      ==========================================================
	void Update ()
	{
		// While Alive Functions
		MovePC();
		MouseLook();
		
	} //-----
	
	// Move PC		 =================================================================================
	void MovePC()
	{		
		// Is the PC on some ground
		if (cc_PC.isGrounded) 
		{			
			// Add Z movement to the direction vector based on Vertical input (W,S or Cursor U,D) 
			v3_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			
			// Convert world coordinates to local for the PC
			v3_direction = fl_speed * transform.TransformDirection(v3_direction);
			
			// if the jump key is pressed add jump force to the direction vector
			if (Input.GetButton("Jump"))
			{
				v3_direction.y = fl_jump_force;
			}
		}
		else
		{ // when the PC is in the air
			
			// Add fl_gravity to the direction vector
			v3_direction.y -= fl_gravity * Time.deltaTime;
		}
		
		// Move the character controller with the direction vector
		cc_PC.Move( v3_direction * Time.deltaTime );
		
	}//------
		
	// Mouse Look ==================================================================================
	void MouseLook()
	{
		// Mouse Rotate
		transform.Rotate (0, 3 * fl_turn_rate *  Time.deltaTime * Input.GetAxis ("Mouse X") ,0);
		fl_Xrotation += -Input.GetAxis("Mouse Y") * -180 * Time.deltaTime;
		fl_Xrotation = Mathf.Clamp(fl_Xrotation, -80f, 80f);
		go_cam.transform.localEulerAngles = new Vector3 (-fl_Xrotation, 0, 0);

		foreach (Transform tr_Child in transform)
		{
			if(tr_Child.gameObject.tag == "Emitter" || tr_Child.gameObject.tag == "Hook" || tr_Child.gameObject.name == "FlashLight")
			{
				tr_Child.localEulerAngles = new Vector3 (-fl_Xrotation, 0, 0);
			}
		}

	}//------

}
