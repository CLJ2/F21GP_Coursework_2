using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class wizardSwitching : MonoBehaviour
{

    private PlayerMovementInputs input;
    private GameObject playerFollowCamera;

    //Awake is called when the script instance is first loaded
    private void Awake()
    {
        if (playerFollowCamera == null)
        {
            playerFollowCamera = GameObject.FindGameObjectWithTag("FollowCamera");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerMovementInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharacter();
    }

    /*
 * Switches the character if a change happens
 */
    private void SwitchCharacter()
    {
        if (input.GetSwitchNeeded())
        {
            GetComponent<AiAgent>().active = true;
            input.SetSwitchNeeded(false);
            GetComponent<PlayerInput>().enabled = false; //Disable control of current character

            playerFollowCamera.gameObject.SendMessage("switchCharacter", input.GetCharacterSelection()); //Request control of next character
        }
    }
}
