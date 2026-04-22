using NUnit.Framework;
using UnityEngine;

public class MinigameInputReaderTest
{
    private GameObject _inputObject;
    private MinigameInputReader _reader;

    [SetUp]
    public void Setup()
    {
        _inputObject = new GameObject("MinigameInputReaderObject");
        _reader = _inputObject.AddComponent<MinigameInputReader>();
    }

    [TearDown]
    public void TearDown()
        => Object.Destroy(_inputObject);

    [Test]
    public void AwakeTest()
        => Assert.AreEqual("Minigame", _reader.Name);
}
