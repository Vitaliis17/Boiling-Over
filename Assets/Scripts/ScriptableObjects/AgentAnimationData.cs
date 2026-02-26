using UnityEngine;

[CreateAssetMenu(fileName = "AgentAnimationData", menuName = "ScriptableObject/AnimationData/Agent")]
public class AgentAnimationData : ScriptableObject
{
    public int IdleHash { get; private set; }
    public int WalkingHash { get; private set; }
    public int RunningHash { get; private set; }
    public int SittingHash { get; private set; }
    public int AttackHash { get; private set; }

    private void Awake()
    {
        IdleHash = SetHash(nameof(MovementActions.Idle));
        WalkingHash = SetHash(nameof(MovementActions.Walking));
        RunningHash = SetHash(nameof(MovementActions.Running));
        SittingHash = SetHash(nameof(MovementActions.Sitting));
        AttackHash = SetHash(nameof(MovementActions.Attack));
    }

    private int SetHash(string animationName)
        => Animator.StringToHash(animationName);
}