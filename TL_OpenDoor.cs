using UnityEngine;

public class TL_OpenDoor : MonoBehaviour {

    //Variables
    public GameObject go_Door;
    public GameObject UI_Text;
    private GameObject go_PC;	
	private float fl_Distance;
	private TL_LaserMediator LaserRoomScript;


	void Start()
	{
        //Find the PC
		go_PC = GameObject.Find ("PC");

        //Obtain the script from the parent gameobject
		LaserRoomScript = transform.parent.GetComponent<TL_LaserMediator>();
	}

	void Update()
	{
        //Calculate the distance between the PC and this gameobject
		fl_Distance = Vector3.Distance(go_PC.transform.position, transform.position);

        //If the PC is close enough to this gameobject then activate the text
        if (fl_Distance < 3f)
        {
            UI_Text.SetActive(true);
        }
        else
        {
            UI_Text.SetActive(false);
        }

        //If the door is not destroyed
		if(go_Door != null)
		{
            //If the switch have been activated and the player as pressed E then destroy the door
			if(Input.GetKeyDown("e") && fl_Distance <= 3f && LaserRoomScript.SwitchScript03.bl_Activate && LaserRoomScript.SwitchScript04.bl_Activate && LaserRoomScript.SwitchScript05.bl_Activate)
			{
				Destroy (go_Door);
			}
		}

	}

}
