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
        Debug.Log($"{name}��{other.name}�ɐڐG���܂����I");
    }
}
