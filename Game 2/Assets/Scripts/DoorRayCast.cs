
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRayCast : MonoBehaviour
{
    [SerializeField] private int rayLength = 4;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private DoorController rayCastedObj;

    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

    [SerializeField] private Image crosshair = null;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                DoorController hitDoor = hit.collider.gameObject.GetComponent<DoorController>();

                if (rayCastedObj != hitDoor)
                {
                    rayCastedObj = hitDoor;
                    CrosshairChange(true);
                }

                if (Input.GetKeyDown(openDoorKey))
                {
                    rayCastedObj.PlayAnimation();
                }
            }
            else
            {
                if (rayCastedObj != null)
                {
                    CrosshairChange(false);
                    rayCastedObj = null;
                }
            }
        }
        else
        {
            if (rayCastedObj != null)
            {
                CrosshairChange(false);
                rayCastedObj = null;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if (on)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
        }
    }
}
// from OPENING a DOOR in UNITY with a RAYCAST