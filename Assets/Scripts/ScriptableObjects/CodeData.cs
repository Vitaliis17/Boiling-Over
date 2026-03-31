using UnityEngine;

[CreateAssetMenu(fileName = "SafeCode", menuName = "ScriptableObjects/Code")]
public class CodeData : ScriptableObject
{
    [SerializeField, Min(1)] private int _codeIndexAmount;
    [SerializeField, Min(1)] private int _stepTurningAmount;

    private int[] _code;

    public int CodeIndexAmount => _codeIndexAmount;

    private void OnEnable()
    {
        GenerateCode();
        OutputCode();
    }

    public bool IsCorrectValue(int value, int index)
    {
        if(index < 0 || index >= _code.Length)
            return false;

        return _code[index] == value;
    }

    private void GenerateCode()
    {
        const int Negative = -1;
        const int Positive = 1;

        const int MinTurningAmount = 1;

        _code = new int[_codeIndexAmount];
        int[] signs = new int[] { Negative, Positive };

        int turningAmount;
        int signIndex;

        for (int i = 0; i < _code.Length; i++)
        {
            signIndex = Random.Range(0, signs.Length);
            turningAmount = Random.Range(MinTurningAmount, _stepTurningAmount) * signs[signIndex];

            _code[i] = turningAmount;
        }
    }

    private void OutputCode()
    {
        foreach(int number in _code)
            Debug.Log(number);
    }
}