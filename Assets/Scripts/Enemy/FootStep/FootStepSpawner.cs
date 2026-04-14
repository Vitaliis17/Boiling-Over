using UnityEngine;

public class FootStepSpawner : Spawner<FootStep>
{
    public FootStepSpawner(FootStep prefab, Transform container) : base(prefab, container)
    { }
}