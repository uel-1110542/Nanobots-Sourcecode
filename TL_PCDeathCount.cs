using UnityEngine;
using UnityEngine.UI;

public class TL_PCDeathCount : MonoBehaviour {

	//Variables
	public int in_PC_Deaths;
    public Text DeathCount;
	private TL_PCMove PCScript;
	private TL_Restart RestartScript;


	void Start()
	{
        //Set default value
		in_PC_Deaths = 0;

        //Obtain script from this gameobject
		PCScript = GetComponent<TL_PCMove>();

        //Find the gameobject and obtain the script
		RestartScript = GameObject.Find("Laser_Room").GetComponent<TL_Restart>();
	}

    void Update()
    {
        //Display the death count on the UI text
        DeathCount.text = "Deaths: " + in_PC_Deaths;
    }

	void OnTriggerStay(Collider col_)
	{
        //If the PC falls onto a spike, add to the death count and call the Reset function
		if(col_.tag == "Spike")
		{
			in_PC_Deaths++;
			RestartScript.Reset();
		}
	}

	void OnParticleCollision(GameObject go_Collision)
	{
		//If the PC has been touched by the gameobject tagged Laser emitting the particle effect, respawn PC to the starting location
		if(go_Collision.tag == "Laser" || go_Collision.tag == "CyanLaser" || go_Collision.tag == "YellowLaser" || go_Collision.tag == "BlueLaser" || go_Collision.tag == "OrangeLaser" || go_Collision.tag == "GreenLaser")
		{
			in_PC_Deaths++;
			RestartScript.Reset();
		}
	}

}
