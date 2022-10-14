using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MoveStop : MonoBehaviour
{
    #region Properties
    #endregion

    #region Inspector Variables
    [SerializeField]
    int _stopTime = 1000;
    #endregion

    #region Unity Methods
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    #endregion

    // async 修飾子を付けた 戻り値が Task型 あるいはTask<>型のメソッドは 非同期で行う事を意味する。
    // このメソッドはawait を見つけるまで同期処理し、awaitで指定されたメソッドを非同期で実行する。
    // awaitで指定されたメソッドが終了したら再び同期処理を開始する。
    // awaitを指定できるメソッドは戻り値がTask型 あるいはTask<>型のメソッドである。

    // つまり、async修飾子は、内部で待機が発生することを示す為に使用する。
    // await修飾子は、指定したメソッドで待機することを示すために使用する。
    private async Task OnStop()
    {
        // このように実行することで通常のメソッドも別スレッドで実行することができる。
        await Task.Run(() => Thread.Sleep(5000));


    }
}