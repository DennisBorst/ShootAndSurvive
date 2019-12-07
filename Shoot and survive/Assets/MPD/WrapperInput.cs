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
    public KeyCode jumpKey;
    public KeyCode sprintKey;
    public KeyCode shootKey;
    public KeyCode reloadKey;
    public KeyCode pickUpKey;

    private void Start()
    {
#if UNITY_EDITOR
        PCInput();
#endif

#if USE_CONTROLLER
        ControllerInput();
#endif //USE_CONTROLLER

#if USE_KEY_BOARD
        PCInput();
#endif //USE_KEY_BOARD
    }

    private void ControllerInput()
    {
        jumpKey = KeyCode.Joystick1Button0;
        shootKey = KeyCode.Joystick1Button7;
        reloadKey = KeyCode.Joystick1Button2;
        sprintKey = KeyCode.Joystick1Button8;
        pickUpKey = KeyCode.Joystick1Button6;
    }

    private void PCInput()
    {
        jumpKey = KeyCode.Space;
        shootKey = KeyCode.Mouse0;
        reloadKey = KeyCode.R;
        sprintKey = KeyCode.LeftShift;
        pickUpKey = KeyCode.E;
    }

    /*
    private void ControllerInputAxis()
    {
        horizontalMovement = Input.GetAxis("HorizontalController");
        verticalMovement = Input.GetAxis("VerticalController");

        horizontalCamera = Input.GetAxis("Mouse X Controller");
        verticalMovement = Input.GetAxis("Mouse Y Controller");
    }
    */


    /*
    private void PCInputAxis()
    {
        horizontalMovement = Input.GetAxis("HorizontalPC");
        verticalMovement = Input.GetAxis("VerticalPC");

        //horizontalCamera = Input.GetAxis("Mouse X PC");
        //verticalMovement = Input.GetAxis("Mouse Y PC");
    }
    */

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
