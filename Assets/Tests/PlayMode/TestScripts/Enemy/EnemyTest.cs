using NUnit.Framework;
using UnityEngine;

public class EnemyTest
{
    private GameObject _enemyObject;
    private Enemy _enemy;
    private Rigidbody _rigidbody;

    [SetUp]
    public void Setup()
    {
        _enemyObject = new GameObject("EnemyTest");
        _enemy = _enemyObject.AddComponent<Enemy>();
        _rigidbody = _enemy.GetComponent<Rigidbody>();
    }

    [TearDown]
    public void TearDown()
        => Object.Destroy(_enemyObject);

    [Test]
    public void TestAwake()
        => Assert.IsTrue(_rigidbody.freezeRotation);

    [Test]
    public void TestActivateKinematic()
    {
        _rigidbody.isKinematic = false;

        _enemy.ActivateKinematic();

        Assert.IsTrue(_rigidbody.isKinematic);
    }

    [Test]
    public void TestDeactivateKinematic()
    {
        _rigidbody.isKinematic = true;

        _enemy.DeactivateKinematic();

        Assert.IsFalse(_rigidbody.isKinematic);
    }

    [Test]
    public void TestSetLooking()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 90, 0));

        _enemy.SetLooking(rotation);

        float angleDifference = Quaternion.Angle(rotation, _enemy.transform.rotation);
        Assert.Less(angleDifference, 0.1);
    }
}
