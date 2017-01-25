using UnityEngine;
using UnityEngine.UI;

public class TL_NanobotParticles : MonoBehaviour {

    //Variables
    public int in_NanobotMode = 1;
    public bool bl_Hooked = false;
    public GameObject go_Target;
    public Text Instructions;
    public Text SelectedMode;

    private float fl_FiringDelay = 0.75f;
	private float fl_FiringCooldown = 0f;	
	private TL_PCMove PCScript;
	private ParticleSystem ps_Nanobot;
    private ParticleSystem.MainModule MainMod;
    private string st_Control;



	void Start()
	{
        //Obtain script from this gameobject
		PCScript = GetComponent<TL_PCMove>();

        //Find the child and obtain the component from it
		ps_Nanobot = transform.FindChild ("Nanobot_Hook").GetComponent<ParticleSystem>();

        //Set the script interface of the module from the variable
        MainMod = ps_Nanobot.main;
    }

	void Update()
	{
        //Function for shooting nanobots
		ShootNanobots();

        //Select the mode of the nanobots
		SelectMode();

        //Function for the hook mode
		HookFunction();

        //Update the text for the UI
        UpdateUI();
    }

	void HookFunction()
	{
		if(bl_Hooked)
		{
			//Calcuates the distance between the PC and the hook target
			float fl_Distance = Vector3.Distance(transform.position, go_Target.transform.position);

			//As long as the player is holding down on the mouse button then the PC will move towards the attached hook
			if(Input.GetMouseButton(0) && in_NanobotMode == 2 && fl_Distance > 0.75f)
			{
				//Moves the PC towards the hooked target
				transform.position = Vector3.MoveTowards(transform.position, go_Target.transform.position, 10f * Time.deltaTime);

                //Set lifetime of particle effect
                MainMod.startLifetime = 0.75f;

                //Disable script
                PCScript.enabled = false;
			}
			else
			{
                //Set lifetime of particle effect
                MainMod.startLifetime = 2f;

                //Enable script
				PCScript.enabled = true;

                //Set bool to false
				bl_Hooked = false;
			}			
		}
	}

    void UpdateUI()
    {
        //Set text for the currently selected mode
        SelectedMode.text = "Currently selected mode: " + in_NanobotMode;

        //If the nanobot mode is not set to 1, change the text
        if (in_NanobotMode != 1)
        {
            Instructions.text = "Hold Left Mouse button to activate it.";

        }
        else
        {
            Instructions.text = "Click Left Mouse button to activate it.";
        }


    }

	void ShootNanobots()
	{
		//Loops through all of the child transforms the PC has
		foreach (Transform tr_Child in transform)
		{
			switch(tr_Child.name)
			{
			case "Nanobot_Emitter":
				//If the Nanobot Mode is 1 and the left mouse button has been clicked, the particle effect will play
				if (Input.GetMouseButtonDown(0) && fl_FiringCooldown < Time.realtimeSinceStartup && in_NanobotMode == 1)
				{
                    //Activate the child transform
					tr_Child.transform.gameObject.SetActive(true);
                    
                    //Obtain the component and play the particle system
					tr_Child.GetComponent<ParticleSystem>().Play();
                    
                    //Add the cooldown
					fl_FiringCooldown = fl_FiringDelay + Time.realtimeSinceStartup;
				}
				else
				{
                    //Obtain the component and stop the particle system
                    tr_Child.GetComponent<ParticleSystem>().Stop();
				}
				break;

			case "Nanobot_Hook":
                //If the Nanobot Mode is 2 and the left mouse button has been clicked, the particle effect will play
                if (Input.GetMouseButton(0) && in_NanobotMode == 2)
				{
					tr_Child.transform.gameObject.SetActive(true);
				}
				else
				{
					tr_Child.transform.gameObject.SetActive(false);
				}
				break;

			case "pf_NanobotShield":
                //If the Nanobot Mode is 3 and the left mouse button has been clicked, the particle effect will play
                if (Input.GetMouseButton(0) && in_NanobotMode == 3)
				{
					tr_Child.transform.gameObject.SetActive(true);
				}
				else
				{
					tr_Child.transform.gameObject.SetActive(false);
				}
				break;
			}

		}
	}

	void SelectMode()
	{
		//Pressing 1 or 2 above the key letters will select the mode
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			in_NanobotMode = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			in_NanobotMode = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			in_NanobotMode = 3;
		}
	}

}
