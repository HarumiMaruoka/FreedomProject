using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UniTaskExample : MonoBehaviour
{
    private async void Start()
    {
        Debug.LogWarning("�O���J�n");
        await Example(this.GetCancellationTokenOnDestroy());
        Debug.LogWarning("�O���I��");
    }

    private async UniTask Example(CancellationToken test)
    {
        Debug.Log("�J�n");
        await UniTask.Delay(3000);
        Debug.Log("����");
    }
}
