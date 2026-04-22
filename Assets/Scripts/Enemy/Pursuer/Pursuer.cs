using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

public class Pursuer : MonoBehaviour
{
    private readonly Subject<Unit> _reached = new();
    private readonly Subject<Vector3> _transfering = new();
    
    private NavMeshAgent _agent;

    private Vector3 _targetPosition;
    private bool _havePath;

    private CancellationTokenSource _source;

    public Observable<Unit> Reached => _reached;
    public Observable<Vector3> Transfering => _transfering;

    private void Awake()
        => _havePath = false;

    private void OnEnable()
        => _source = new();

    private void OnDisable()
    {
        _source?.Cancel();
        _source?.Dispose();
    }

    private void OnDestroy()
    {
        _reached?.Dispose();
        _transfering?.Dispose();
    }

    public void Initialize(NavMeshAgent agent)
    {
        _agent = agent;

        Check(_source.Token).Forget();
    }

    public void SetDestination(Vector3 destination)
    {
        _targetPosition = destination;
        _havePath = _agent.SetDestination(_targetPosition);
    }

    private void UpdatePathStatus()
        => _havePath = _agent.pathPending == false && _agent.remainingDistance > _agent.stoppingDistance;

    private async UniTaskVoid Check(CancellationToken token)
    {
        const int WaitingMilliseconds = 100;

        bool isCancel = false;

        while (enabled)
        {
            UpdatePathStatus();

            if (_havePath == false)
            {
                _reached.OnNext(Unit.Default);
                _transfering.OnNext(_targetPosition);

                isCancel = await UniTask.WaitWhile(() => _havePath == false, cancellationToken: token).SuppressCancellationThrow();

                if (isCancel)
                    return;
            }

            isCancel = await UniTask.Delay(WaitingMilliseconds, cancellationToken: token).SuppressCancellationThrow();

            if(isCancel)
                return;
        }
    }
}