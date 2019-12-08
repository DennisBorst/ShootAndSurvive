using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject weaponPlaceHolder;
    [SerializeField] private float maxPickUpRange;

    private Camera cam;

    //key
    private KeyCode pickUpKey;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        pickUpKey = WrapperInput.Instance.pickUpKey;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxPickUpRange))
        {
            Gun gun = hit.transform.GetComponent<Gun>();
            PickUpItem pickUpItem = hit.transform.GetComponent<PickUpItem>();

            if (gun != null)
            {
                Debug.Log("looking at a paint gun");
                PickUpWeapon(gun.gameObject, gun);
            }
            else if (pickUpItem != null)
            {
                Debug.Log("Looking at a pickup item");
                PickUpItem(pickUpItem);
            }
        }
    }

    private void PickUpWeapon(GameObject gunObject, Gun gun)
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            gunObject.transform.parent = weaponPlaceHolder.transform;
            gunObject.transform.localPosition = Vector3.zero;
            gunObject.transform.localRotation = Quaternion.identity;

            gun.PickedUp();
        }
    }

    private void PickUpItem(PickUpItem pickUpItem)
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            if (pickUpItem.isAmmo)
            {
                Gun gun = GetComponentInChildren<Gun>();
                if (gun != null)
                {
                    gun.IncreaseAmmo(pickUpItem.ammoAmount);
                    gun.ShowAmountOfBullets();
                    Destroy(pickUpItem.gameObject);
                }
            }
            else if (pickUpItem.isHealth)
            {
                //Health health = GetComponent<Health>();
                PlayerController.playerHealthStatic += pickUpItem.healthAmount;
                Destroy(pickUpItem.gameObject);
            }
        }
    }
}
