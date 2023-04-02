using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerSwitching : MonoBehaviour
{
    [Header("Characters available for switching")]
    [Tooltip("This contains the transforms of all characters that can be switched to. Element 0 corresponds to number 1, etc")]
    [SerializeField]
    private GameObject[] characters = new GameObject[3];

    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    //Switches characters based on input and array of characters set in unity
    public void switchCharacter(int selection)
    {
        characters[selection].GetComponent<AiAgent>().active = false;
        characters[selection].GetComponent<PlayerInput>().enabled = true; //Enalbe control of character swapping too
        characters[selection].GetComponent<PlayerMovementInputs>().SetCharacterSelection(selection); //Let character know its selected (for swapping purposes)
        
        virtualCamera.Follow = characters[selection].transform.GetChild(2); //Tell camera to follow new character
    }
}
