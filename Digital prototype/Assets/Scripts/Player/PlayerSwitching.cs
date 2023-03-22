using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSwitching : MonoBehaviour
{
    [Header("Characters available for switching")]
    [Tooltip("This contains the transforms of all characters that can be switched to")]
    [SerializeField]
    private Transform[] characterTransforms = new Transform[2];

    private CinemachineVirtualCamera virtualCamera;
    private PlayerMovementInputs input;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        input = GetComponent<PlayerMovementInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
