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

    // async �C���q��t���� �߂�l�� Task�^ ���邢��Task<>�^�̃��\�b�h�� �񓯊��ōs�������Ӗ�����B
    // ���̃��\�b�h��await ��������܂œ����������Aawait�Ŏw�肳�ꂽ���\�b�h��񓯊��Ŏ��s����B
    // await�Ŏw�肳�ꂽ���\�b�h���I��������Ăѓ����������J�n����B
    // await���w��ł��郁�\�b�h�͖߂�l��Task�^ ���邢��Task<>�^�̃��\�b�h�ł���B

    // �܂�Aasync�C���q�́A�����őҋ@���������邱�Ƃ������ׂɎg�p����B
    // await�C���q�́A�w�肵�����\�b�h�őҋ@���邱�Ƃ��������߂Ɏg�p����B
    private async Task OnStop()
    {
        // ���̂悤�Ɏ��s���邱�ƂŒʏ�̃��\�b�h���ʃX���b�h�Ŏ��s���邱�Ƃ��ł���B
        await Task.Run(() => Thread.Sleep(5000));


    }
}