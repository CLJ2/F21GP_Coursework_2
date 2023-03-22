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
    private GameObject[] characters = new GameObject[2];

    private CinemachineVirtualCamera virtualCamera;
    private PlayerMovementInputs input;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void switchCharacter(int selection)
    {
        characters[selection].GetComponent<PlayerInput>().enabled = true;
        characters[selection].GetComponent<PlayerMovementInputs>().SetCharacterSelection(selection);
        virtualCamera.Follow = characters[selection].transform.GetChild(2);
    }
}
