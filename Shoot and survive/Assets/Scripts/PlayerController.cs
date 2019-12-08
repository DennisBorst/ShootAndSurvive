using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;

    public static int playerHealthStatic;
    public int playerHealth;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed; 
    [SerializeField] private float jumpHeight;
    private float currentMoveSpeed;
    public static CharacterController playerController;

    [Header("Camera Settings")]
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Transform camera;
    private float yCameraDirection;

    [Header("General")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float circleRadius;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private Vector3 velocity;

    //Input
    private float horizontalMovement;
    private float verticalMovement;
    private float horizontalCamera;
    private float verticalCamera;

    private KeyCode jumpKey;
    private KeyCode sprintKey;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerController = GetComponent<CharacterController>();

        //playerHealthStatic = playerHealth;
        playerHealth = playerStats.health;
        playerHealthStatic = playerStats.health;
        moveSpeed = playerStats.walkSpeed;
        sprintSpeed = playerStats.sprintSpeed;
        cameraSensitivity = playerStats.cameraSensitivity;

        //Input
        horizontalMovement = Input.GetAxis(WrapperInput.Instance.horizontalMovement);
        verticalMovement = Input.GetAxis(WrapperInput.Instance.verticalMovement);
        horizontalCamera = Input.GetAxis(WrapperInput.Instance.horizontalCamera);
        verticalCamera = Input.GetAxis(WrapperInput.Instance.verticalCamera);

        jumpKey = WrapperInput.Instance.jumpKey;
        sprintKey = WrapperInput.Instance.sprintKey;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis(WrapperInput.Instance.horizontalMovement);
        verticalMovement = Input.GetAxis(WrapperInput.Instance.verticalMovement);
        horizontalCamera = Input.GetAxis(WrapperInput.Instance.horizontalCamera);
        verticalCamera = Input.GetAxis(WrapperInput.Instance.verticalCamera);

        UIManager.Instance.HealthAmount(playerHealthStatic);
        isGrounded = Physics.CheckSphere(groundCheck.position, circleRadius, groundLayer);

        CameraRotation();
        PlayerMovement();
        Gravity();
    }

    private void CameraRotation()
    {
        float mouseX = horizontalCamera * cameraSensitivity * Time.deltaTime;
        float mouseY = verticalCamera * cameraSensitivity * Time.deltaTime;

        yCameraDirection -= mouseY;
        yCameraDirection = Mathf.Clamp(yCameraDirection, -70f, 70f);

        camera.transform.localRotation = Quaternion.Euler(yCameraDirection, 0f, 0f);
        this.transform.Rotate(Vector3.up * mouseX);
    }

    private void PlayerMovement()
    {
        float x = horizontalMovement;
        float z = verticalMovement;

        if (Input.GetKey(sprintKey))
        {
            currentMoveSpeed = sprintSpeed;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        playerController.Move(move * currentMoveSpeed * Time.deltaTime);

        
    }

    private void Gravity()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    public static void TakeDamage(int damage)
    {
        playerHealthStatic -= damage;
        UIManager.Instance.TakeDamage();

        if(playerHealthStatic <= 0)
        {
            playerHealthStatic = 0;
            UIManager.Instance.HealthAmount(playerHealthStatic);
            GameManager.Instance.Death();
            Debug.Log("Game over, you died");
        }
    }
}
