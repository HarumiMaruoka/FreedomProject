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
        Debug.LogWarning("�O���J�n");
        await Test(this.GetCancellationTokenOnDestroy());
        Debug.LogWarning("�O���I��");
    }
    private async UniTask Test(CancellationToken token)
    {
        Debug.Log("�e�X�g�J�n");
        // ��������������܂ŉ��L�̍s�őҋ@����B
        try
        {
            await UniTask.WaitUntil(() => transform.position.y < 0, cancellationToken: token);
        }
        catch (Exception e)
        {
            Debug.Log($"{e.GetType()} ��O��catch");
        }
        Debug.Log("�e�X�g�I��");
    }
}
