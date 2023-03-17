using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the wizard in m/s")]
    [SerializeField]
    private float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the wizard in m/s")]
    [SerializeField]
    private float SprintSpeed = 5.2f;

    [Tooltip("How fast the wizard turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [SerializeField]
    private float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField]
    private float SpeedChangeRate = 10.0f;

    [Tooltip("The height the wizard can jump")]
    [SerializeField]
    private float JumpHeight = 1.2f;

    [Tooltip("The wizard uses its own gravity value. The unity engine default is -9.81f")]
    [SerializeField]
    private float Gravity = -15.0f;

    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    [SerializeField]
    private float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Helps when walking down stairs")]
    [SerializeField]
    private float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    [SerializeField]
    private bool Grounded = true;

    [Tooltip("Controls grounded sphere position")]
    [SerializeField]
    private float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    [SerializeField]
    private float GroundedRadius = 0.28f;

    [Tooltip("What layers the wizard uses as ground")]
    [SerializeField]
    private LayerMask GroundLayers;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    private float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    private float BottomClamp = -30.0f;

    // cinemachine
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;

    // player
    private float speed;
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;

    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    // animation IDs
    private int animIDSpeed;
    private int animIDGrounded;
    private int animIDJump;
    private int animIDFreeFall;
    private int animIDMotionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
