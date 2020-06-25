public class Button : InteractableObject
{
    protected new void Update()
    {
        if (ObjectActionTriggered()) gameObject.SendMessageUpwards("ButtonPressed", UnityEngine.SendMessageOptions.DontRequireReceiver);
        base.Update();
    }
}
