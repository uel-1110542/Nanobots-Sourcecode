using UnityEngine;
using UnityEngine.UI;

public class TL_LevelManager : MonoBehaviour {

    //Variables
    public Text Checklist;
	private TL_KeyObtained KeyScript;
	private TL_EndGame VictoryScript;
	private GameObject go_PuzzleDoor;

    //String array variable for showing if the requirements are met
    private string[] st_Objectives = new string[4];
	private int in_Index;

    //String variables
	private string st_Objective01;
	private string st_Objective02;
	private string st_Objective03;
	private string st_Objective04;


	void Start ()
	{
        //Find the PC and obtain the script
		KeyScript = GameObject.Find ("PC").GetComponent<TL_KeyObtained>();

        //Find the gameobject and obtain the script from the child
		VictoryScript = GameObject.Find ("Invisible_PlatRoom").GetComponentInChildren<TL_EndGame>();

        //Find the door
		go_PuzzleDoor = GameObject.Find ("Coded_Door");
	}

	void Update()
	{
        //Check objectives
        CheckObjectives();

        //Updates the checklist from the UI
        UpdateChecklist();
	}

    void CheckObjectives()
    {
        //If the key is obtained then change the text to a tick
        if (KeyScript.bl_KeyObtained01)
        {
            in_Index = 0;
            st_Objectives[in_Index] = "✔";
        }
        else
        {
            st_Objectives[0] = "X";
        }

        //If the key is obtained then change the text to a tick
        if (KeyScript.bl_KeyObtained02)
        {
            in_Index = 1;
            st_Objectives[in_Index] = "✔";
        }
        else
        {
            st_Objectives[1] = "X";
        }

        //If the door that leads to the puzzle is destroyed then change the text to a tick
        if (go_PuzzleDoor == null)
        {
            in_Index = 2;
            st_Objectives[in_Index] = "✔";

        }
        else
        {
            st_Objectives[2] = "X";
        }

        //If the item is picked up then change the text to a tick
        if (VictoryScript.bl_PickedUp)
        {
            in_Index = 3;
            st_Objectives[in_Index] = "✔";
        }
        else
        {
            st_Objectives[3] = "X";
        }
    }

    void UpdateChecklist()
    {
        //Change the text depending on the objectives
        Checklist.text = "Checklist" + "\n\n" + "Purple Keycard: " + st_Objectives[0] + "\n" + "Orange Keycard: " + st_Objectives[1] + "\n" + "Memory Pad: " + st_Objectives[2] + "\n" + "Pandora's Box: " + st_Objectives[3];
    }

}
