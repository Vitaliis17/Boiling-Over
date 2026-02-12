using UnityEngine;

[CreateAssetMenu(fileName = "AgentAnimationData", menuName = "ScriptableObject/AnimationData/Agent")]
public class AgentAnimationData : ScriptableObject
{
    public int IdleHash { get; private set; }
    public int WalkingHash { get; private set; }
    public int RunningHash { get; private set; }
    public int SittingHash { get; private set; }

    private void Awake()
    {
        IdleHash = SetHash(nameof(States.Idle));
        WalkingHash = SetHash(nameof(States.Walking));
        RunningHash = SetHash(nameof(States.Running));
        SittingHash = SetHash(nameof(States.Sitting));
    }

    private int SetHash(string animationName)
        => Animator.StringToHash(animationName);
}