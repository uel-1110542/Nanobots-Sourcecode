using UnityEngine;

public class TL_HookScript : MonoBehaviour {

	//Variables
	private GameObject go_PC;
	private TL_PCMove PCScript;
	private TL_NanobotParticles NanobotScript;


	void Start()
	{
        //Find the PC
		go_PC = GameObject.Find ("PC");

        //Obtain the script from the PC
		NanobotScript = go_PC.GetComponent<TL_NanobotParticles>();
	}

	void OnParticleCollision(GameObject go_obj)
	{
		if(go_obj.tag == "Hook")
		{
			//Turns the boolean to true to activate the hooking function
			NanobotScript.bl_Hooked = true;

			//References this instance only as the gameobject target
			NanobotScript.go_Target = gameObject;
		}
	}

}
