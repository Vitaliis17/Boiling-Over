using UnityEngine;
using System.Collections;

public class Timer
{
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}