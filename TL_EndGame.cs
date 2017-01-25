using UnityEngine;
using UnityEngine.SceneManagement;

public class TL_EndGame : MonoBehaviour {

    //Variables
    public GameObject VictoryMessage;
	private GameObject go_PC;
	private float fl_Distance;
	public bool bl_PickedUp = false;
	private TL_Instructions InstructionScript;


	void Start()
	{
        //Find the PC
		go_PC = GameObject.Find ("PC");

        //Obtain the script from the PC
		InstructionScript = go_PC.GetComponent<TL_Instructions>();
	}

	void Update()
	{
		EndGame();
	}

    public void ReturnToStart()
    {
        SceneManager.LoadScene("Level_Scene");
    }

	void EndGame()
	{
        //Calculate the distance between this gameobject and the PC
		fl_Distance = Vector3.Distance(go_PC.transform.position, transform.position);

        //If the PC is close enough and the player has pressed E
		if(fl_Distance <= 2f && Input.GetKeyDown(KeyCode.E) && !bl_PickedUp)
		{
            //Activate the victory message
            VictoryMessage.SetActive(true);

            //Set bool to true
            bl_PickedUp = true;

            //Disable the script
			InstructionScript.enabled = false;

            //Freeze time
			Time.timeScale = 0f;
		}
	}

}
