using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrapperInput : MonoBehaviour
{
    //Walking
    public float horizontalMovement;
    public float verticalMovement;

    //Camera
    public float horizontalCamera;
    public float verticalCamera;

    //Buttons
    public static KeyCode jumpKey;
    public static KeyCode shootKey;
    public static KeyCode sprintKey;

#if UNITY_EDITOR

#endif //UNITY_EDITOR

#if USE_KEY_BOARD

#endif //USE_KEY_BOARD

#if USE_CONTROLLER

#endif //USE_CONTROLLER

    private void Start()
    {
#if USE_CONTROLLER  
        ControllerInput();
#endif //USE_CONTROLLER

#if USE_KEY_BOARD
        PCInput();
#endif //USE_KEY_BOARD
    }

    public static void ControllerInput()
    {
        jumpKey = KeyCode.Joystick1Button0;
        shootKey = KeyCode.Joystick1Button10;
        sprintKey = KeyCode.Joystick1Button8;
    }

    public static void PCInput()
    {
        jumpKey = KeyCode.Space;
        shootKey = KeyCode.Mouse0;
        sprintKey = KeyCode.LeftShift;
    }

    #region Singleton

    private static WrapperInput instance;

    private void Awake()
    {
        instance = this;
    }

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
