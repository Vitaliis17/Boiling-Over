using UnityEngine;
using System.Collections.Generic;

public class AnimationStateMachine
{
    private readonly Dictionary<States, State> _animationStates;

    private State _currentState;

    public AnimationStateMachine(Animator animator, AgentAnimationData animationData)
    {
        _animationStates = new Dictionary<States, State>
        {
            { States.Idle, new State(animator, animationData.IdleHash) },
            { States.Walking, new State(animator, animationData.WalkingHash)},
            { States.Running, new State(animator, animationData.RunningHash)},
            { States.Sitting, new State(animator, animationData.SittingHash)}
        };

        _currentState = _animationStates[States.Idle];
        _currentState.Start();
    }

    public void ChangeState(States state)
    {
        _currentState = _animationStates[state];
        _currentState.Start();
    }
}