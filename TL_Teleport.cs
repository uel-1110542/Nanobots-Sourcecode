using UnityEngine;

public class TL_Teleport : MonoBehaviour {

    //Variables
    public GameObject Text_UI;
	private GameObject go_PC;
	public Vector3 v3_WarpSpot;
	private bool bl_Entered;


	void Start()
	{
		go_PC = GameObject.Find ("PC");
	}

	void Update() 
	{
        //If the PC has entered the area
        if (bl_Entered)
        {
            //Activate the text
            Text_UI.SetActive(true);
        }
        else
        {
            //Activate the text
            Text_UI.SetActive(false);
        }

		if(Input.GetKeyDown("q") && bl_Entered)
		{
			//Sets the position to the warp point
			go_PC.transform.position = v3_WarpSpot;

			//Sets rotations to zero
			go_PC.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
	}

	void OnTriggerEnter(Collider _col)
	{
        //If the collided gameobject is the PC then set bool to true
		if(_col.name == "PC")
		{
			bl_Entered = true;
		}
	}

	void OnTriggerExit(Collider _col)
	{
        //If the collided gameobject has exited the collider then set bool to false
        if (_col.name == "PC")
		{
			bl_Entered = false;
		}
	}

}
