using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UniTaskExample : MonoBehaviour
{
    private async void Start()
    {
        Debug.LogWarning("外側開始");
        await Example(this.GetCancellationTokenOnDestroy());
        Debug.LogWarning("外側終了");
    }

    private async UniTask Example(CancellationToken test)
    {
        Debug.Log("開始");
        await UniTask.Delay(3000);
        Debug.Log("完了");
    }
}
