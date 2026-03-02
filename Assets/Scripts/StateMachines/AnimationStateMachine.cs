using UnityEngine;
using System.Collections.Generic;

public class AnimationStateMachine
{
    private readonly Dictionary<MovementActions, AnimationState> _animationStates;
    private int[] _layers;

    private AnimationState _currentState;

    public AnimationStateMachine(Animator animator, AgentAnimationData animationData)
    {
        _animationStates = new Dictionary<MovementActions, AnimationState>
        {
            { MovementActions.Idle, new AnimationState(animator, animationData.IdleHash) },
            { MovementActions.Walking, new AnimationState(animator, animationData.WalkingHash) },
            { MovementActions.Running, new AnimationState(animator, animationData.RunningHash) },
            { MovementActions.Sitting, new AnimationState(animator, animationData.SittingHash) },
            { MovementActions.Attack, new AnimationState(animator, animationData.AttackHash) }
        };

        GenerateLayers();

        ChangeState(MovementActions.Idle);
    }

    public void ChangeState(MovementActions state)
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