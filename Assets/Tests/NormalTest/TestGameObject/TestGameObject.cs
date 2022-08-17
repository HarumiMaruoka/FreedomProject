using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObject : MonoBehaviour
{
    [SerializeField] GameObject TestB;
    void Start()
    {
        TestB = this.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("aaa");
        }
    }
}
