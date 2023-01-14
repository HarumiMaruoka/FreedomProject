using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UniTaskExampleSecond : MonoBehaviour
{
    System.Func<UniTask> _testFunc = default;

    private async void Start()
    {
        Debug.LogWarning("外側開始");
        await Test(this.GetCancellationTokenOnDestroy());
        Debug.LogWarning("外側終了");
    }
    private async UniTask Test(CancellationToken token)
    {
        Debug.Log("テスト開始");
        // 条件が成立するまで下記の行で待機する。
        try
        {
            await UniTask.WaitUntil(() => transform.position.y < 0, cancellationToken: token);
        }
        catch (Exception e)
        {
            Debug.Log($"{e.GetType()} 例外をcatch");
        }
        Debug.Log("テスト終了");
    }
}
