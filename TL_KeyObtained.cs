using UnityEngine;
using UnityEngine.UI;

public class TL_KeyObtained : MonoBehaviour {

    //Variables
    public GameObject PickupMessage;
	public GameObject go_Key01;
	public GameObject go_Key02;
	public GameObject go_Door01;
	public GameObject go_Door02;
    public bool bl_KeyObtained01 = false;
    public bool bl_KeyObtained02 = false;

    

    void Update()
    {
        //Function for picking up keycards
        PickupKeyCard();

        //Display the pickup message
        DisplayPopup();

        //Function for unlocking doors
        UnlockDoor();
    }

    void DisplayPopup()
    {
        //Obtain the text component from the child gameobject
        Text Popup_Text = PickupMessage.GetComponentInChildren<Text>();

        GameObject[] Interactable = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject go in Interactable)
        {
            //If the PC isn't in range of any interactable object then hide the message
            if (Vector3.Distance(go.transform.position, transform.position) > 3)
            {
                PickupMessage.SetActive(false);
            }
        }

        if (go_Key01 != null)
        {
            if (Vector3.Distance(go_Key01.transform.position, transform.position) <= 3)
            {
                //If the PC is in range of the key, display the message
                PickupMessage.SetActive(true);

                //Change the text
                Popup_Text.text = "Press E to pickup";
            }
        }

        if (go_Key02 != null)
        {
            if (Vector3.Distance(go_Key02.transform.position, transform.position) <= 3)
            {
                //If the PC is in range of the key, display the message
                PickupMessage.SetActive(true);

                //Change the text
                Popup_Text.text = "Press E to pickup";
            }
        }

        if (go_Door01 != null && bl_KeyObtained01)
        {
            if (Vector3.Distance(go_Door01.transform.position, transform.position) <= 3)
            {
                //If the PC is in range of the door, display the message
                PickupMessage.SetActive(true);

                //Change the text
                Popup_Text.text = "Press E to open";
            }
        }

        if (go_Door02 != null && bl_KeyObtained02)
        {
            if (Vector3.Distance(go_Door02.transform.position, transform.position) <= 3)
            {
                //If the PC is in range of the door, display the message
                PickupMessage.SetActive(true);

                //Change the text
                Popup_Text.text = "Press E to open";
            }
        }

    }

	void PickupKeyCard()
	{
		//If the key isn't destroyed
		if(go_Key01 != null)
		{
			//If the distance between the PC and the key is equal to 2 or less and the E key is pressed then set the bool to true and destroy the key
			if(Vector3.Distance (go_Key01.transform.position, transform.position) <= 2f && Input.GetKeyDown(KeyCode.E))
			{
				bl_KeyObtained01 = true;
				Destroy (go_Key01);
			}
		}

		//If the key isn't destroyed
		if(go_Key02 != null)
		{
			//If the distance between the PC and the key is equal to 2 or less and the E key is pressed then set the bool to true and destroy the key
			if(Vector3.Distance (go_Key02.transform.position, transform.position) <= 2f && Input.GetKeyDown(KeyCode.E))
			{
				bl_KeyObtained02 = true;
				Destroy (go_Key02);
			}
		}

	}

	void UnlockDoor()
	{
		//If the door hasn't been destroyed
		if (go_Door01 != null)
		{
			//If the distance between the PC and the door is close enough to press E and the purple keycard has been obtained
			if(Vector3.Distance (go_Door01.transform.position, transform.position) <= 4 && Input.GetKeyDown(KeyCode.E) && bl_KeyObtained01)
			{
				Destroy(go_Door01);
			}
		}

		//If the door hasn't been destroyed
		if (go_Door02 != null)
		{
			//If the distance between the PC and the door is close enough to press E and the orange keycard has been obtained
			if(Vector3.Distance (go_Door02.transform.position, transform.position) <= 4 && Input.GetKeyDown(KeyCode.E) && bl_KeyObtained02)
			{
				Destroy(go_Door02);
			}
		}
	}

}
