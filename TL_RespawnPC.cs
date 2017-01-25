using UnityEngine;

public class TL_RespawnPC : MonoBehaviour {

	
	public TL_PCMove PCScript;


    
	void Start()
	{
        //Find the PC and obtain the script
		PCScript = GameObject.Find ("PC").GetComponent<TL_PCMove>();
	}

	void OnTriggerEnter(Collider col_Obj)
	{
        //If the collided gameobject is the PC
		if(col_Obj.gameObject.tag == "PC")
		{
            //Set the PC's position to the spawn point
			col_Obj.transform.position = PCScript.v3_Respawn;
		}
	}

}
