using System.Linq;

public class PriorityTargetsContainer
{
    private readonly IRemyInteractable[] _targets;

    public PriorityTargetsContainer()
        => _targets = new IRemyInteractable[] { new Player() };

    public bool IsTarget(IRemyInteractable interactable)
        => _targets.Any(element => interactable.GetType() == element.GetType());
}
