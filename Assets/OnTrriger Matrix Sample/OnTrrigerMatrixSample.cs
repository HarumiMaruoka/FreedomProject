using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrrigerMatrixSample : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name}が{other.name}に接触しました！");
    }
}
