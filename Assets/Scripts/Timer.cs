using Cysharp.Threading.Tasks;
using System.Threading;

public class Timer
{
    public async UniTask WaitSeconds(float time, CancellationToken token)
    {
        await UniTask.WaitForSeconds(time, cancellationToken: token);
    }
}