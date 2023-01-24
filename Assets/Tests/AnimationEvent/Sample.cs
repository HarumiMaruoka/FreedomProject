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


    // 数学で出た回転の計算
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
