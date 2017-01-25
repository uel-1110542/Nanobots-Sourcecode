using UnityEngine;
using System.Collections;

public class TL_LaserMediator : MonoBehaviour {

	//Gameobjects
	public GameObject go_Switch01;
	public GameObject go_Switch02;
	public GameObject go_Switch03;
	public GameObject go_Switch04;
	public GameObject go_Switch05;

	//Switch Scripts
	public TL_Switch SwitchScript01;
	public TL_Switch SwitchScript02;
	public TL_Switch SwitchScript03;
	public TL_Switch SwitchScript04;
	public TL_Switch SwitchScript05;

	//Particlesystems
	private ParticleSystem ps_Laser01;
	private ParticleSystem ps_Laser02;
	private ParticleSystem ps_Laser03;
	private ParticleSystem ps_Laser04;
	private ParticleSystem ps_Laser05;


	void Start()
	{
		SwitchScript01 = go_Switch01.GetComponent<TL_Switch>();
		SwitchScript02 = go_Switch02.GetComponent<TL_Switch>();
		SwitchScript03 = go_Switch03.GetComponent<TL_Switch>();
		SwitchScript04 = go_Switch04.GetComponent<TL_Switch>();
		SwitchScript05 = go_Switch05.GetComponent<TL_Switch>();
	}

	void Update()
	{
		LaserActivation();
	}

	void LaserActivation()
	{
		//Finds the gameobject with the tag "CyanLaser"
		GameObject[] go_CyanLaser = GameObject.FindGameObjectsWithTag("CyanLaser");

		//If the 1st switch is activated
		if(SwitchScript01.bl_Activate)
		{
			foreach(GameObject go in go_CyanLaser)
			{
				//Obtain the particle system and if the laser is playing then stop it
				ps_Laser01 = go.GetComponent<ParticleSystem>();
				if(ps_Laser01.isPlaying)
				{
					ps_Laser01.Stop ();
				}
			}
		}
		else
		{
			foreach(GameObject go in go_CyanLaser)
			{
				//Obtain the particle system and if the laser is stopped then play it
				ps_Laser01 = go.GetComponent<ParticleSystem>();
				if(ps_Laser01.isStopped)
				{
					ps_Laser01.Play ();
				}
			}
		}

		//Finds the gameobject with the tag "YellowLaser"
		GameObject[] go_YellowLaser = GameObject.FindGameObjectsWithTag("YellowLaser");
		if(SwitchScript02.bl_Activate)
		{
			foreach(GameObject go in go_YellowLaser)
			{
				//Obtain the particle system and if the laser is playing then stop it
				ps_Laser02 = go.GetComponent<ParticleSystem>();
				if(ps_Laser02.isPlaying)
				{
					ps_Laser02.Stop ();
				}
			}
		}
		else
		{
			foreach(GameObject go in go_YellowLaser)
			{
				//Obtain the particle system and if the laser is stopped then play it
				ps_Laser02 = go.GetComponent<ParticleSystem>();
				if(ps_Laser02.isStopped)
				{
					ps_Laser02.Play ();
				}
			}
		}

		//Finds the gameobject with the tag "BlueLaser"
		GameObject[] go_BlueLaser = GameObject.FindGameObjectsWithTag("BlueLaser");
		if(SwitchScript03.bl_Activate)
		{
			foreach(GameObject go in go_BlueLaser)
			{
				//Obtain the particle system and if the laser is playing then stop it
				ps_Laser03 = go.GetComponent<ParticleSystem>();
				if(ps_Laser03.isPlaying)
				{
					ps_Laser03.Stop ();
				}
			}
		}
		else
		{
			foreach(GameObject go in go_BlueLaser)
			{
				//Obtain the particle system and if the laser is stopped then play it
				ps_Laser03 = go.GetComponent<ParticleSystem>();
				if(ps_Laser03.isStopped)
				{
					ps_Laser03.Play ();
				}
			}
		}

		//Finds the gameobject with the tag "GreenLaser"
		GameObject[] go_GreenLaser = GameObject.FindGameObjectsWithTag("GreenLaser");
		if(SwitchScript04.bl_Activate)
		{
			foreach(GameObject go in go_GreenLaser)
			{
				//Obtain the particle system and if the laser is playing then stop it
				ps_Laser04 = go.GetComponent<ParticleSystem>();
				if(ps_Laser04.isPlaying)
				{
					ps_Laser04.Stop ();
				}
			}
		}
		else
		{
			foreach(GameObject go in go_GreenLaser)
			{
				//Obtain the particle system and if the laser is stopped then play it
				ps_Laser04 = go.GetComponent<ParticleSystem>();
				if(ps_Laser04.isStopped)
				{
					ps_Laser04.Play ();
				}
			}
		}

		//Finds the gameobject with the tag "OrangeLaser"
		GameObject[] go_OrangeLaser = GameObject.FindGameObjectsWithTag("OrangeLaser");
		if(SwitchScript05.bl_Activate)
		{
			foreach(GameObject go in go_OrangeLaser)
			{
				//Obtain the particle system and if the laser is playing then stop it
				ps_Laser05 = go.GetComponent<ParticleSystem>();
				if(ps_Laser05.isPlaying)
				{
					ps_Laser05.Stop ();
				}
			}
		}
		else
		{
			foreach(GameObject go in go_OrangeLaser)
			{
				//Obtain the particle system and if the laser is stopped then play it
				ps_Laser05 = go.GetComponent<ParticleSystem>();
				if(ps_Laser05.isStopped)
				{
					ps_Laser05.Play ();
				}
			}
		}

	}

}
