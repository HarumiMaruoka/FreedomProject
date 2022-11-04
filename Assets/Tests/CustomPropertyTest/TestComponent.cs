using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    [TagName,SerializeField]
    string tag;


    [Free,SerializeField]
    int freeInteger;

    [SerializeField]
    int aaa;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
