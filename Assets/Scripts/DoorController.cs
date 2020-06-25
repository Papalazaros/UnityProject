using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool doorOpen;

    private void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorOpen = true;
            SetDoorEvent("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (doorOpen && other.gameObject.CompareTag("Player"))
        {
            doorOpen = false;
            SetDoorEvent("Close");
        }
    }

    private void ButtonPressed()
    {
        doorOpen = !doorOpen;
        SetDoorEvent(doorOpen ? "Open" : "Close" );
    }

    private void SetDoorEvent(string direction)
    {
        animator.SetTrigger(direction);
    }
}
