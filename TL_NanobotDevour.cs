using UnityEngine;
using System.Collections;

public class TL_NanobotDevour : MonoBehaviour {

	//Variables
	public GameObject go_Nanobots;
	public bool bl_ContactWithNanobots = false;
	private float fl_DecayRate = 0.1f;
	private float fl_DecayCooldown = 1f;
	private TL_NanobotParticles NanobotScript;



	void Start()
	{
		NanobotScript = GameObject.Find ("PC").GetComponentInChildren<TL_NanobotParticles>();
	}

	void Update()
	{
		if(bl_ContactWithNanobots)
		{
			if(fl_DecayCooldown < Time.realtimeSinceStartup)
			{ 
				//Alters the local scale of an object at a time interval
				transform.localScale = new Vector3 (transform.localScale.x - 0.05f, transform.localScale.y - 0.05f, transform.localScale.z - 0.05f);

				//If the local scale becomes very small, it will destroy the object
				if(transform.localScale.x < 0.10f)
				{
					Destroy (gameObject);
				}

				//Adds the decay rate with the time as a cooldown
				fl_DecayCooldown = fl_DecayRate + Time.realtimeSinceStartup;
			}
		}
	}

	void OnParticleCollision(GameObject other)
	{
		//If the gameobject has been hit by a particle effect from a gameobject tagged as Emitter and the Nanobot Mode is 1
		if(other.tag == "Emitter" && !bl_ContactWithNanobots && NanobotScript.in_NanobotMode == 1)
		{
			//Instantiate the nanobot effect onto the affected object
			GameObject go_NanoEffect;
			go_NanoEffect = Instantiate (go_Nanobots, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
			go_NanoEffect.transform.Rotate(-90, 0, 0);
			bl_ContactWithNanobots = true;
		}
	}

}
