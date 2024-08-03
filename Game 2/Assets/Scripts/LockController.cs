using UnityEngine;

public class LockController : MonoBehaviour
{
    private Animator lockAnim;
    private bool isLocked = false;
    [SerializeField] private TaskManager taskManager;
     
    private void Awake()
    {
        lockAnim = gameObject.GetComponent<Animator>();
        // Ensure the animator is in sync with the initial state
        lockAnim.SetBool("isLocked", isLocked);
    }

    public void ToggleLock()
    {
        isLocked = !isLocked;
        lockAnim.SetBool("isLocked", isLocked);
        Debug.Log("Lock state changed: " + isLocked);
        taskManager.DoorLockedStateChanged(isLocked); // Notify the TaskManager
    }

    public bool IsLocked()
    {
        return isLocked;
    }
}
