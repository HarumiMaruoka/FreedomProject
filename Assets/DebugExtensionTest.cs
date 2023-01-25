using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugExtensionTest : MonoBehaviour
{
    [SerializeField]
    private int importance;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebagExtension.DebugLog("Ž„‚ÍLog‚Å‚·", importance, LogType.Player);
        }
    }
}