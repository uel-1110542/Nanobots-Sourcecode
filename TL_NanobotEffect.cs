using UnityEngine;

public class TL_NanobotEffect : MonoBehaviour {

	//Variables
	public GameObject go_Nanobots;
	public bool bl_ContactWithNanobots = false;
	private float fl_DecayRate = 0.25f;
	private float fl_DecayCooldown = 1f;
    

	void Update()
	{
        //If this gameobject has been touched by the nanobots
		if(bl_ContactWithNanobots)
		{
            //If the cooldown is less than the time.realtimesincestartup
			if(fl_DecayCooldown < Time.realtimeSinceStartup)
			{
                //Decay the gameobject by scaling it down
				transform.localScale = new Vector3 (transform.localScale.x - 0.05f, transform.localScale.y - 0.10f, transform.localScale.z - 0.05f);

                //If the gameobject is small enough, destroy it
                if (transform.localScale.x < 0 && transform.localScale.y < 0 && transform.localScale.z < 0)
				{
					Destroy (gameObject);
				}
                //Add the cooldown
				fl_DecayCooldown = fl_DecayRate + Time.realtimeSinceStartup;
			}
		}
	}

	void OnParticleCollision(GameObject other)
	{
        //If this gameobject has collided with the gameobject with the tag Emitter and hasn't touched the nanobots before
		if(other.tag == "Emitter" && !bl_ContactWithNanobots)
		{
            //Create the nanobots
			GameObject go_NanoEffect = Instantiate(go_Nanobots, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;

            //Rotate the nanobots
            go_NanoEffect.transform.Rotate(-90, 0, 0);

            //Set bool to true
			bl_ContactWithNanobots = true;
		}
	}

}
