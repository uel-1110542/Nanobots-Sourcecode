using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TL_Restart : MonoBehaviour {

	//Variables	
    public GameObject go_Door;
    public GameObject go_Cubes;
    public GameObject RestartMessage;
    private GameObject go_PC;

    public List<GameObject> lt_Cubes = new List<GameObject>();
	public List<Vector3> lt_Destructibles = new List<Vector3>();
	public List<GameObject> lt_Weights = new List<GameObject>();
	public List<Vector3> lt_WeightPos = new List<Vector3>();
	public Vector3 v3_SpawnPoint;
	public bool bl_Entered = false;
	private int in_Count;
	private TL_NanobotDevour NanobotScript;
    private TL_Pickup PickUpScript;
    
    

	void Start()
	{
        //Find the PC
		go_PC = GameObject.Find ("PC");

        //Obtain the script from the PC
		PickUpScript = go_PC.GetComponent<TL_Pickup>();

        //Loop through the cubes in this gameobject
		foreach(Transform tr_Cubes in transform)
		{
			//Stores the initial positions and gameobjects in a list
			if(tr_Cubes.gameObject.tag == "Destructible")
			{
				lt_Cubes.Add (tr_Cubes.gameObject);
				lt_Destructibles.Add (tr_Cubes.position);
			}

			if(tr_Cubes.gameObject.tag == "Weight")
			{
				lt_Weights.Add (tr_Cubes.gameObject);
				lt_WeightPos.Add (tr_Cubes.position);
			}
		}
		//Sets the value of the cubes count to the variable
		in_Count = lt_Cubes.Count;
	}

	void Update()
	{
        //Displays restart text
        DisplayMessage();

        //Function for restarting the room
        RestartRoom();
	}

    void DisplayMessage()
    {
        //Obtain the text from the child gameobject
        Text Restart_Text = RestartMessage.GetComponentInChildren<Text>();

        //Display the text
        Restart_Text.text = "Press R to restart";
    }

	void RestartRoom()
	{
        //If the door hasn't been destroyed and the player has pressed R
		if(go_Door != null && bl_Entered && Input.GetKeyDown("r"))
		{
			Reset();
		}
	}

	public void Reset()
	{
		//Sets the PC's position to the spawn point
		go_PC.transform.position = v3_SpawnPoint;

		//Sets the rotation facing the entrance of the room
		go_PC.transform.eulerAngles = new Vector3(0, 270f, 0);

		//Turns off the pickup
		PickUpScript.bl_PickUp = false;
		
		foreach(Transform tr in go_PC.transform)
		{
			if(tr.tag == "Weight")
			{
				Rigidbody rb_ = tr.gameObject.GetComponent<Rigidbody>();

				//Sets the parent to the laser room
				tr.SetParent(GameObject.Find ("Laser_Room").transform);

				//Freezes rotation
				rb_.constraints = RigidbodyConstraints.FreezeRotation;

				//Turns gravity on
				rb_.useGravity = true;
			}
		}

		//Sets the initial positions and rotations to the weights in the list
		for(int c=0; c < lt_Weights.Count; c++)
		{
			lt_Weights[c].gameObject.transform.position = new Vector3(lt_WeightPos[c].x, lt_WeightPos[c].y, lt_WeightPos[c].z);
			lt_Weights[c].gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
		}


		for(int i=0; i < in_Count; i++)
		{
			if(lt_Cubes[i] != null)
			{
				//Grabs the nanobot scripts from every cube and sets contact with nanobots bool to false
				NanobotScript = lt_Cubes[i].gameObject.GetComponent<TL_NanobotDevour>();
				NanobotScript.bl_ContactWithNanobots = false;

				//Destroys the nanobots attached to the cubes
				foreach(Transform tr_Child in lt_Cubes[i].transform)
				{
					Destroy (tr_Child.gameObject);
				}

				//Sets both the velocity and angular velocity to zero
				lt_Cubes[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				lt_Cubes[i].gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

				//Sets the initial positions and rotations
				lt_Cubes[i].transform.position = new Vector3(lt_Destructibles[i].x, lt_Destructibles[i].y, lt_Destructibles[i].z);
				lt_Cubes[i].transform.rotation = new Quaternion(0, 0, 0, 0);

				//Sets the scale to one
				lt_Cubes[i].transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else
			{
				//If there are any cubes missing then re-instantiate them
				lt_Cubes[i] = (GameObject) Instantiate(go_Cubes, new Vector3(lt_Destructibles[i].x, lt_Destructibles[i].y, lt_Destructibles[i].z), new Quaternion(0, 0, 0, 0));

				//Sets both the velocity and angular velocity to zero
				lt_Cubes[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				lt_Cubes[i].gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

				//Sets the scale to one
				lt_Cubes[i].transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
	}

	void OnTriggerEnter(Collider _col)
	{
        //If the PC has entered the room
		if(_col.gameObject.name == "PC")
		{
            //Activate the message
            RestartMessage.SetActive(true);

            //Set bool to true
            bl_Entered = true;

            //Record the PC's position as a spawn point
			v3_SpawnPoint = _col.transform.position;
		}
	}

	void OnTriggerExit(Collider _col)
	{
        //If the PC has left the room
		if(_col.gameObject.name == "PC")
		{
            //Deactivate the message
            RestartMessage.SetActive(false);

            //Set bool to false
            bl_Entered = false;
		}
	}

}
