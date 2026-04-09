using UnityEngine;
using System.Collections.Generic;

public class AnimationStrategySelector
{
    private readonly Dictionary<MovementActions, AnimationState> _animationStates;
    private int[] _layers;

    private AnimationState _currentState;

    public AnimationStrategySelector(Animator animator, AgentAnimationData animationData)
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

        foreach (int layer in _layers)
            _currentState.Start(layer);
    }

    private void GenerateLayers()
    {
        const int FirstIndex = 1;
        const int LayerAmount = 2;

        _layers = new int[LayerAmount];

        for (int i = 0; i < _layers.Length; i++)
            _layers[i] = i + FirstIndex;
    }
}