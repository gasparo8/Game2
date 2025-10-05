using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    private bool isCursorLocked = true;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (isCursorLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }

    private void ToggleCursorLock()
    {
        if (isCursorLocked)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }
    }
    public void OnPlayerDeathforCursor()
    {
        StartCoroutine(DelayedCursorUnlock(4f));  // Start the coroutine with a 4-second delay
        Debug.Log("Player has died. Cursor will unlock after 4 seconds.");
    }

    private IEnumerator DelayedCursorUnlock(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        UnlockCursor();  // Unlock the cursor after the delay
    }

    public void OnGameEndforCursor()
    {
        StartCoroutine(DelayedCursorUnlockEndGame(20f));  // Start the coroutine with a 20-second delay
        Debug.Log("Player completed game. Cursor will unlock after 18 seconds.");
    }
    private IEnumerator DelayedCursorUnlockEndGame(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        UnlockCursor();  // Unlock the cursor after the delay
    }
}
