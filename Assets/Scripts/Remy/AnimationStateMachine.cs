using UnityEngine;
using System.Collections.Generic;

public class AnimationStateMachine
{
    private readonly Dictionary<States, State> _animationStates;
    private int[] _layers;

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

        GenerateLayers();

        _currentState = _animationStates[States.Idle];
        _currentState.Start(1);
        _currentState.Start(2);
    }

    public void ChangeState(States state)
    {
        _currentState = _animationStates[state];
        _currentState.Start(1);
        _currentState.Start(2);
    }

    private void GenerateLayers()
    {
        const int FirstIndex = 1;

        _layers = new int[3];

        for (int i = 0; i < _layers.Length; i++)
            _layers[i] = i + FirstIndex;
    }
}