using System.Collections.Generic;
using UnityEngine;

public class WorkingStateMachine
{
    private readonly Dictionary<Transform, IInteractablePlace> _places;

    private IInteractablePlace _interactablePlace;

    public WorkingStateMachine()
    {
        _places = new Dictionary<Transform, IInteractablePlace>
        {
            { }
        }
    }
}