using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    private void ASecondsElapsed(int second)
    {
        Debug.Log($"A : {second}�o��");
    }
    private void BSecondsElapsed(int second)
    {
        Debug.Log($"B : {second}�o��");
    }
    private void CSecondsElapsed(int second)
    {
        Debug.Log($"C : {second}�o��");
    }
    private void AnimStart(string stateName)
    {
        Debug.Log(stateName + " : �J�n");
    }
    private void AnimEnd(string stateName)
    {
        Debug.Log(stateName + " : �I��");
    }
}
