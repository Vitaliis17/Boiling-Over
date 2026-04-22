using System.Collections.Generic;
using System;
using R3;

public class MovementActionSetter : IDisposable
{
    private readonly Dictionary<Type, MovementActions> _movementActions;
    private readonly Subject<MovementActions> _currentMovementAction = new();

    public MovementActionSetter()
    {
        _movementActions = new Dictionary<Type, MovementActions>()
        {
            { typeof(Player), MovementActions.Running },
            { typeof(Seat), MovementActions.Walking },
            { typeof(StayingPlace), MovementActions.Walking }
        };
    }

    public Observable<MovementActions> Setted => _currentMovementAction;

    public void Set(Type type)
    {
        if(_movementActions.TryGetValue(type, out MovementActions action))
            _currentMovementAction.OnNext(action);
    }

    public void Dispose()
        => _currentMovementAction?.Dispose();
}
