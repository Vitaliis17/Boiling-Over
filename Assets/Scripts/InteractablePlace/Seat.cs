public class Seat : InteractablePlace
{
    private void Awake()
        => State = MovementActions.Sitting;
}