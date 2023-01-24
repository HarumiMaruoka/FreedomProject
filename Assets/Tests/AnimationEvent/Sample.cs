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


    // ���w�ŏo����]�̌v�Z
    private float deg = 2 * Mathf.PI / 360f;
    private float radius = 1f;

    private void Update()
    {
        transform.position =
            new Vector3(
                transform.position.x * radius * Mathf.Cos(deg) - transform.position.z * radius * Mathf.Sin(deg),
                transform.position.y,
                transform.position.x * radius * Mathf.Sin(deg) + transform.position.z * radius * Mathf.Cos(deg)
                );
    }
}
