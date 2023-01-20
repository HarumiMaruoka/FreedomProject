using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    private void ASecondsElapsed(int second)
    {
        Debug.Log($"A : {second}経過");
    }
    private void BSecondsElapsed(int second)
    {
        Debug.Log($"B : {second}経過");
    }
    private void CSecondsElapsed(int second)
    {
        Debug.Log($"C : {second}経過");
    }
    private void AnimStart(string stateName)
    {
        Debug.Log(stateName + " : 開始");
    }
    private void AnimEnd(string stateName)
    {
        Debug.Log(stateName + " : 終了");
    }
}
