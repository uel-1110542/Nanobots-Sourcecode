using UnityEngine;

public class TL_RevealPlatform : MonoBehaviour {

	//Variables
	private Color col_Opacity;
    

	void OnParticleCollision(GameObject go_obj)
	{
        //If the collided particle has the Reveal tag
		if(go_obj.tag == "Reveal")
		{
            //Obtain the color from this gameobject
			col_Opacity = transform.gameObject.GetComponent<Renderer>().material.color;

            //Change the opacity over time.deltatime
			col_Opacity.a += 1f * Time.deltaTime;

            //Set the changed opacity to the color of this gameobject
			transform.gameObject.GetComponent<Renderer>().material.color = col_Opacity;
		}
	}

}
