using System;

public class Seat : InteractablePlace
{
    public event Action Interaction;

    public override void Interact()
    {
        Interaction?.Invoke();
    }
}