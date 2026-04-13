public class StayingPlace : InteractablePlace
{
    private void Awake()
        => State = MovementActions.Idle;
}