using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    //Other components
    private Animator animator;
    private CharacterController controller;
    private PlayerMovementInputs input;
    private GameObject mainCamera;

    //Constants
    private const float THRESHOLD = 0.01f; //For camera movement

    //Awake is called when the script instance is first loaded
    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cinemachineTargetYaw = CinemachineCameraTarget.transform.roation.eulerAngles.y;
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerMovementInputs>();

        AssignAnimationIDs();

        //Reset timeouts at the start
        jumpTimeoutDelta = JumpTimeout;
        fallTimeoutDelta = FallTimeout;
    }

    // Update is called once per frame
    void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    //Called after update has finished
    private void LateUpdate()
    {
        CameraRotation();
    }

    //Sets all animation parameters to ID's for faster comparison
    private void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("Speed");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDJump = Animator.StringToHash("Jump");
        animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    /*
	*Check if on ground by checking if character collides with ground
	*This is done using a spherical collider at the base of the character
	*/
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        // update animator
        animator.SetBool(animIDGrounded, Grounded);
    }

    /*
	* Rotates the camera based on mouse input using the cinemachine package
    */
    private void CameraRotation()
    {
        // if there is an input
        if (input.look.sqrMagnitude >= threshold)
        {
            cinemachineTargetYaw += input.look.x;
            cinemachineTargetPitch += input.look.y;
        }

        // clamp our rotations so our values are limited 360 degrees
        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0.0f);
    }

    /*Calculates speed and direction for characters movement
	* Looks at current speed and applies acceleration or decelration if neccesarry
	* Then looks at direction of speed and moves the player
	*/
    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = input.sprint ? SprintSpeed : MoveSpeed;

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (input.move == Vector2.zero)
        {
            targetSpeed = 0.0f;
        }

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one which provides a more natural speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f)
        {
            animationBlend = 0f;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (input.move != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        // move the player
        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

        // update animator
        animator.SetFloat(animIDSpeed, animationBlend);
        animator.SetFloat(animIDMotionSpeed, inputMagnitude);
    }
}
