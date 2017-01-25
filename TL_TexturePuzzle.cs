using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TL_TexturePuzzle : MonoBehaviour {

	//Variables
	private GameObject go_SequencePad;
	public Texture[] tx_SequencePad = new Texture[9];			//Texture array that goes through 3 different illuminated tiles in a sequence
	public Collider[] col_SquarePad = new Collider[9];			//Collider array that coresponds with the index of the ColoredPad array

	private GameObject go_ColoredPad;
	public Texture[] tx_ColoredPad = new Texture[9];		//Texture array that holds up to 9 different illuminated tiles once a player selects them
	public Texture tx_NoColor;

    public GameObject DisplayText;
    private GameObject go_PC;
    private GameObject go_GreenLight;
	private GameObject go_RedLight;

	private Camera cam_Puzzle;	
	private TL_PCMove PCScript;
	private TL_NanobotParticles NanobotScript;
	private float fl_Distance;
	private bool bl_Activate = false;
	private bool bl_SequenceRunning = true;

	private bool[] bl_1stSeq = new bool[3];		//Boolean array that holds 3 elements
	private bool bl_Activate2ndSeq = false;
	private bool[] bl_2ndSeq = new bool[4];		//Boolean array that holds 4 elements
	private bool bl_Activate3rdSeq = false;
	private bool[] bl_3rdSeq = new bool[5];		//Boolean array that holds 5 elements

	private bool bl_WrongButton = false;
	private bool bl_GreenFlash;

	private float fl_TimeInterval = 0.7f;
	private float fl_TimeCooldown;

	private float fl_LightInterval = 0.25f;
	private float fl_LightCooldown;

	private int in_TextureIndex;
	public int in_IndexSeq = 0;
	private int in_Increment = 0;
	private int in_LimitIndex = 0;
	private int in_Limit = 3;
	public List<int> lt_Index = new List<int>();


	void Start()
	{
		go_PC = GameObject.Find ("PC");
		PCScript = go_PC.GetComponent<TL_PCMove>();
		NanobotScript = go_PC.GetComponent<TL_NanobotParticles>();

		//Finds the transform gameobject within the child
		go_SequencePad = transform.FindChild("Sequence_Pad").gameObject;

		//Sets the texture material with one of no colour
		go_SequencePad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_NoColor);

		go_ColoredPad = transform.FindChild("Colored_Pad").gameObject;
		go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_NoColor);

		cam_Puzzle = GetComponentInChildren<Camera>();

		//Obtains the Light component from the gameobject it finds
		go_GreenLight = GameObject.Find ("Green_Flash").GetComponent<Light>().gameObject;
		go_RedLight = GameObject.Find ("Red_Flash").GetComponent<Light>().gameObject;
	}

	void Update()
	{
		ActivatePuzzle();

		//This function checks if the player has selected all of the correct tiles and if the player has then a green light flashes and the door opens
		StartCoroutine(GreenLight());
	}

	IEnumerator Sequence()
	{
		//Sets the illuminated texture as a different colour based on the index
		go_SequencePad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_SequencePad[in_TextureIndex]);
		yield return new WaitForSeconds(0.6f);
		go_SequencePad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_NoColor);
	}

	void ActivatePuzzle()
	{
        //Calculate distance between PC and this gameobject
		fl_Distance = Vector3.Distance(go_PC.transform.position, transform.position);
        if (fl_Distance <= 3f)
        {
            //Activate the display text
            DisplayText.SetActive(true);

            //Once the key has been pressed, it de-activates the PC's scripts to prevent other functionalities from working during the puzzle
            if (Input.GetKeyDown(KeyCode.Q))
            {
                go_PC.SetActive(bl_Activate);
                bl_Activate = !bl_Activate;
                PCScript.enabled = !bl_Activate;
                NanobotScript.enabled = !bl_Activate;
                cam_Puzzle.enabled = bl_Activate;
            }
        }
        else
        {
            //Deactivate the display text
            DisplayText.SetActive(false);
        }

		if (bl_Activate)
		{
			if(bl_SequenceRunning)
			{
				if(fl_TimeCooldown < Time.realtimeSinceStartup)
				{
					if (in_Increment < in_Limit)
					{
						in_TextureIndex = Random.Range (0, 9);
						lt_Index.Add (in_TextureIndex);

						StartCoroutine(Sequence());

						//Increments the texture index
						in_Increment++;

						//Adds the interval with the time as a cooldown for the sequence to run
						fl_TimeCooldown = fl_TimeInterval + Time.realtimeSinceStartup;
					}
					else
					{
						//Once the texture index is more than 3, it sets the illuminated texture as default and the sequence ends
						go_SequencePad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_NoColor);
						bl_SequenceRunning = false;
					}
				}
			}
			else
			{
				//Casts a ray from a camera with using the mouse positions
				Ray ry_Ray = cam_Puzzle.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
				RaycastHit rh_RayHit;
				if(Physics.Raycast(ry_Ray, out rh_RayHit, 20f))
				{
					//Debugging purposes only
					Debug.DrawRay(ry_Ray.origin, ry_Ray.direction * 20f, Color.red);

					//If the left mouse button is clicked and the raycast hits a certain tile
					if(Input.GetMouseButtonDown(0))
					{
                        //if the 2nd sequence hasn't been activated yet
						if(!bl_Activate2ndSeq)
						{
							if(rh_RayHit.collider.gameObject.name == col_SquarePad[lt_Index[0]].gameObject.name && !bl_1stSeq[0])
							{
                                //If the tile selected is correct, then the boolean is true and the index increments
                                bl_1stSeq[in_IndexSeq] = true;								
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[0]]);
								in_IndexSeq++;
							}						
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[1]].gameObject.name && bl_1stSeq[0] && !bl_1stSeq[1])
							{
								//If the first and second tile has been selected correctly then the index increments
								bl_1stSeq[in_IndexSeq] = true;
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[1]]);
								in_IndexSeq++;
							}
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[2]].gameObject.name && bl_1stSeq[0] && bl_1stSeq[1] && !bl_1stSeq[2])
							{
								//If the first and second tile has been selected correctly then the index increments
								bl_1stSeq[in_IndexSeq] = true;
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[2]]);
								StartCoroutine(GreenFlash());
								in_IndexSeq++;
							}
							else
							{
								TileFlash(rh_RayHit);
							}
						}

						if(bl_Activate2ndSeq && !bl_Activate3rdSeq)
						{
                            if (rh_RayHit.collider.gameObject.name == col_SquarePad[lt_Index[0]].gameObject.name && !bl_2ndSeq[0])
							{
								//If the tile selected is correct, then the boolean is true and the index increments
								bl_2ndSeq[in_IndexSeq] = true;
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[0]]);
								in_IndexSeq++;
							}						
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[1]].gameObject.name && bl_2ndSeq[0] && !bl_2ndSeq[1])
							{
								//If the first and second tile has been selected correctly then the index increments
								bl_2ndSeq[in_IndexSeq] = true;
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[1]]);
								in_IndexSeq++;
							}
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[2]].gameObject.name && bl_2ndSeq[0] && bl_2ndSeq[1] && !bl_2ndSeq[2])
							{
								//If the second and third tile has been selected correctly then the index increments
								bl_2ndSeq[in_IndexSeq] = true;
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[2]]);
								in_IndexSeq++;
							}
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[3]].gameObject.name && bl_2ndSeq[0] && bl_2ndSeq[1] && bl_2ndSeq[2] && !bl_2ndSeq[3])
							{
								//If the third and fourth tile has been selected correctly then the index increments
								bl_2ndSeq[in_IndexSeq] = true;
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[3]]);
                                bl_GreenFlash = false;
                                StartCoroutine(GreenFlash());
                                in_IndexSeq++;
							}
							else
							{
								TileFlash(rh_RayHit);
							}
                            
						}

						if(bl_Activate3rdSeq)
						{
							if(rh_RayHit.collider.gameObject.name == col_SquarePad[lt_Index[0]].gameObject.name && !bl_3rdSeq[0])
							{
								bl_3rdSeq[in_IndexSeq] = true;
								Debug.Log (col_SquarePad[lt_Index[0]].gameObject.name + " " + bl_3rdSeq[in_IndexSeq]);
								
								//If the tile selected is correct, then the boolean is true and the index increments
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[0]]);
								in_IndexSeq++;
							}						
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[1]].gameObject.name && bl_3rdSeq[0] && !bl_3rdSeq[1])
							{
								//If the first and second tile has been selected correctly then the index increments
								bl_3rdSeq[in_IndexSeq] = true;
								Debug.Log (col_SquarePad[lt_Index[1]].gameObject.name + " " + bl_3rdSeq[in_IndexSeq]);
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[1]]);
								in_IndexSeq++;
							}
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[2]].gameObject.name && bl_3rdSeq[0] && bl_3rdSeq[1] && !bl_3rdSeq[2])
							{
								//If the second and third tile has been selected correctly then the index increments
								bl_3rdSeq[in_IndexSeq] = true;
								Debug.Log (col_SquarePad[lt_Index[2]].gameObject.name + " " + bl_3rdSeq[in_IndexSeq]);
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[2]]);
								in_IndexSeq++;
							}
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[3]].gameObject.name && bl_3rdSeq[0] && bl_3rdSeq[1] && bl_3rdSeq[2] && !bl_3rdSeq[3])
							{
								//If the third and fourth tile has been selected correctly then the index increments
								bl_3rdSeq[in_IndexSeq] = true;
								Debug.Log (col_SquarePad[lt_Index[3]].gameObject.name + " " + bl_3rdSeq[in_IndexSeq]);
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[3]]);
								in_IndexSeq++;
							}
							else if(rh_RayHit.transform.gameObject.name == col_SquarePad[lt_Index[4]].gameObject.name && bl_3rdSeq[0] && bl_3rdSeq[1] && bl_3rdSeq[2] && bl_3rdSeq[3] && !bl_3rdSeq[4])
							{
								//If the fifth tile has been selected correctly then it goes into the last sequence
								bl_3rdSeq[in_IndexSeq] = true;
								Debug.Log (col_SquarePad[lt_Index[4]].gameObject.name + " " + bl_3rdSeq[in_IndexSeq]);
								go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[lt_Index[4]]);
                                StartCoroutine(GreenFlash());
                                in_IndexSeq++;
							}
							else
							{
								TileFlash(rh_RayHit);
							}
						}

					}
				}		
			}
		}
		else
		{
			ResetTilePad();
		}
	}

	void TileFlash(RaycastHit rh_Ray)
	{
		if(rh_Ray.collider.gameObject.name == "BlueSquare")
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[8]);
		}

		if(rh_Ray.transform.gameObject.name == "YellowSquare")
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[4]);
		}

		if(rh_Ray.transform.gameObject.name == "GraySquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[5]);
		}

		if(rh_Ray.transform.gameObject.name == "OrangeSquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[7]);
		}

		if(rh_Ray.transform.gameObject.name == "GreenSquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[6]);
		}

		if(rh_Ray.transform.gameObject.name == "BrownSquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[1]);
		}

		if(rh_Ray.transform.gameObject.name == "PurpleSquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[3]);
		}

		if(rh_Ray.transform.gameObject.name == "PinkSquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[2]);
		}

		if(rh_Ray.transform.gameObject.name == "RedSquare" && !bl_WrongButton)
		{
				go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_ColoredPad[0]);
		}
		StartCoroutine(RedFlash());
	}

	IEnumerator GreenLight()
	{
		//If the tiles are selected correctly in a sequence then the green light flashes
		if(bl_3rdSeq[0] && bl_3rdSeq[1] && bl_3rdSeq[2] && bl_3rdSeq[3] && bl_3rdSeq[4])
		{
			go_GreenLight.GetComponent<Light>().enabled = true;

			//Pauses the code for an allocated amount of time before it continues
			yield return new WaitForSeconds(1f);

			go_PC.SetActive(true);
			PCScript.enabled = true;
			NanobotScript.enabled = true;
			cam_Puzzle.enabled = false;

			//Moves the door away from the PC
			transform.Translate(6f * Time.deltaTime, 0, 0);

			//If the door moves away from the PC more than a given distance, it destroys itself
			if(Vector3.Distance(go_PC.transform.position, transform.position) > 7f)
			{
				Destroy (gameObject);
			}
		}
	}

	IEnumerator GreenFlash()
	{
		if(!bl_GreenFlash)
		{
			//The green light blinks 3 times
			go_GreenLight.GetComponent<Light>().enabled = true;
			yield return new WaitForSeconds(0.25f);
			go_GreenLight.GetComponent<Light>().enabled = false;
			yield return new WaitForSeconds(0.25f);
			go_GreenLight.GetComponent<Light>().enabled = true;
			yield return new WaitForSeconds(0.25f);
			go_GreenLight.GetComponent<Light>().enabled = false;
			yield return new WaitForSeconds(0.25f);
			
            //Set index to 0
			in_IndexSeq = 0;

            //Set the variable to 0
			in_Increment = 0;

            //Increment the index
            in_Limit++;

            //CLear the list
			lt_Index.Clear();

            //Set value to 0 to reset cooldown
			fl_TimeCooldown = 0;

            //Set bool to true
			bl_SequenceRunning = true;

            //Set bool to false
			bl_WrongButton = false;

            //If the 1st sequence have been selected correctly then activate the 2nd sequence
			if(bl_1stSeq[0] && bl_1stSeq[1] && bl_1stSeq[2])
			{
				bl_Activate2ndSeq = true;
			}

            //if the 2nd sequence have been selected correctly then activate the 3rd sequence
			if(bl_2ndSeq[0] && bl_2ndSeq[1] && bl_2ndSeq[2] && bl_2ndSeq[3])
			{
				bl_Activate3rdSeq = true;
			}
            //Set bool to true to stop the green flash
			bl_GreenFlash = true;
		}

	}

	IEnumerator RedFlash()
	{
		bl_WrongButton = true;

		//The red light blinks 3 times
		go_RedLight.GetComponent<Light>().enabled = true;
		yield return new WaitForSeconds(0.25f);
		go_RedLight.GetComponent<Light>().enabled = false;
		yield return new WaitForSeconds(0.25f);
		go_RedLight.GetComponent<Light>().enabled = true;
		yield return new WaitForSeconds(0.25f);
		go_RedLight.GetComponent<Light>().enabled = false;
		yield return new WaitForSeconds(0.25f);

		//Resets the illuminated textures on the pad
		go_ColoredPad.GetComponent<Renderer>().material.SetTexture("_Illum", tx_NoColor);
		ResetTilePad();
	}

	void ResetTilePad()
	{
		//Resets the selection
		bl_1stSeq[0] = false;
		bl_1stSeq[1] = false;
		bl_1stSeq[2] = false;

		bl_2ndSeq[0] = false;
		bl_2ndSeq[1] = false;
		bl_2ndSeq[2] = false;
		bl_2ndSeq[3] = false;

		bl_3rdSeq[0] = false;
		bl_3rdSeq[1] = false;
		bl_3rdSeq[2] = false;
		bl_3rdSeq[3] = false;
		bl_3rdSeq[4] = false;

		//Restarts the entire sequence and plays it again
		in_IndexSeq = 0;
		in_Increment = 0;
		in_TextureIndex = Random.Range (0, 9);
		fl_TimeCooldown = 0;
		lt_Index.Clear ();
		bl_SequenceRunning = true;
		bl_WrongButton = false;
	}

}
