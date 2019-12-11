using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrapperInput : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public PlayerStats playerStats;
    public EnemyStats enemyStats;

    [Header("Enviromental change")]
    [SerializeField] private Transform enviroment;

    //PC
    [Header("Player stats")]
    [Header("PC")]
    [SerializeField] private int healthPC;
    [SerializeField] private float walkSpeedPC;
    [SerializeField] private float sprintSpeedPC;
    [SerializeField] private float cameraSensitivityPC;
    [Header("Enemy stats")]
    [SerializeField] private int damagePC;
    [SerializeField] private float enemyMovementSpeedPC;

    //Controller
    [Header("Player stats")]
    [Header("Controller")]
    [SerializeField] private int healthController;
    [SerializeField] private float walkSpeedController;
    [SerializeField] private float sprintSpeedController;
    [SerializeField] private float cameraSensitivityController;
    [Header("Enemy stats")]
    [SerializeField] private int damageController;
    [SerializeField] private float enemyMovementSpeedController;

    //Walking
    [HideInInspector] public string horizontalMovement;
    [HideInInspector] public string verticalMovement;

    //Camera
    [HideInInspector] public string horizontalCamera;
    [HideInInspector] public string verticalCamera;

    //Buttons
    [HideInInspector] public KeyCode jumpKey;
    [HideInInspector] public KeyCode sprintKey;
    [HideInInspector] public KeyCode shootKey;
    [HideInInspector] public KeyCode reloadKey;
    [HideInInspector] public KeyCode pickUpKey;

    //instance for singleton
    private static WrapperInput instance;

    private void Awake()
    {
#if UNITY_EDITOR
        PCInput();
#endif

        //Als USE_CONTROLLER in de "scripting define symbol" staat dan voert ie dit uit
#if USE_CONTROLLER
        ControllerInput();
#endif //USE_CONTROLLER

        //Als USE_KEY_BOARD in de "scripting define symbol" staat dan voert ie dit uit
#if USE_KEY_BOARD
        PCInput();
#endif //USE_KEY_BOARD

        instance = this;
    }

    private void PCInput()
    {
        MeshRenderer[] enviromentalColor = enviroment.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < enviroment.childCount; i++)
        {
            enviromentalColor[i].material.color = Color.blue;
        }

        //Player Stats
        playerStats.health = healthPC;
        playerStats.walkSpeed = walkSpeedPC;
        playerStats.sprintSpeed = sprintSpeedPC;
        playerStats.cameraSensitivity = cameraSensitivityPC;

        //Enemy Stats
        enemyStats.damage = damagePC;
        enemyStats.movementSpeed = enemyMovementSpeedPC;


        //Inputs
        horizontalMovement = "HorizontalPC";
        verticalMovement = "VerticalPC";

        horizontalCamera = "MouseX";
        verticalCamera = "MouseY";

        jumpKey = KeyCode.Space;
        shootKey = KeyCode.Mouse0;
        reloadKey = KeyCode.R;
        sprintKey = KeyCode.LeftShift;
        pickUpKey = KeyCode.E;
    }

    private void ControllerInput()
    {

        MeshRenderer[] enviromentalColor = enviroment.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < enviroment.childCount; i++)
        {
            enviromentalColor[i].material.color = Color.red;
        }

        //Player Stats
        playerStats.health = healthController;
        playerStats.walkSpeed = walkSpeedController;
        playerStats.sprintSpeed = sprintSpeedController;
        playerStats.cameraSensitivity = cameraSensitivityController;

        //Enemy Stats
        enemyStats.damage = damageController;
        enemyStats.movementSpeed = enemyMovementSpeedController;

        //Inputs
        horizontalMovement = "HorizontalController";
        verticalMovement = "VerticalController";

        horizontalCamera = "MouseXController";
        verticalCamera = "MouseYController";

        jumpKey = KeyCode.Joystick1Button0;
        shootKey = KeyCode.Joystick1Button5;
        reloadKey = KeyCode.Joystick1Button2;
        sprintKey = KeyCode.Joystick1Button8;
        pickUpKey = KeyCode.Joystick1Button4;
    }

    #region Singleton
    public static WrapperInput Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new WrapperInput();
            }

            return instance;
        }
    }
    #endregion
}
