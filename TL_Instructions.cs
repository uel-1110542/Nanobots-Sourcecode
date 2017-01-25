using UnityEngine;

public class TL_Instructions : MonoBehaviour {

    //Variables
    public GameObject Instructions;
	public bool bl_HelpToggle = true;
	private TL_NanobotDevour NanobotScript;
    


	void Update()
	{
        //Function for toggling the pause
		PauseToggle();
	}

	void PauseToggle()
	{
        //Find the gameobjects with the tag
		GameObject[] go_NanobotDevour = GameObject.FindGameObjectsWithTag("Destructible");

		//If the P key is pressed down
		if (Input.GetKeyDown ("p"))
		{
			//Toggles between true and false
			bl_HelpToggle = !bl_HelpToggle;
		}

		if(bl_HelpToggle)
		{
			//Freezes the game and the particle effects as well as disabling the use of the nanobots
			Time.timeScale = 0f;
			foreach(GameObject go in go_NanobotDevour)
			{
				NanobotScript = go.GetComponent<TL_NanobotDevour>();
				NanobotScript.enabled = false;
			}

            //Display the instructions
            Instructions.SetActive(true);
        }
		else
		{
			//Un-freezes the game and the particle effects as well as enabling the use of the nanobots
			Time.timeScale = 1f;
			foreach(GameObject go in go_NanobotDevour)
			{
				NanobotScript = go.GetComponent<TL_NanobotDevour>();
				NanobotScript.enabled = true;
			}
            //Hide the instructions
            Instructions.SetActive(false);
        }

	}

}
