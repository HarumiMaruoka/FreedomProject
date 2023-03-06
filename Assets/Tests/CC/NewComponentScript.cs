using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewComponentScript : MonoBehaviour
{
    [SerializeField]
    private bool _isHitWall = false;
    [SerializeField]
    private bool onStop = false;

    private bool _goingToRight = true;

    private void Start()
    {
        Debug.Log("�E�ɐi��ł���");
    }
    IEnumerator aa;
    [SerializeField]
    private bool isStartCoroutine = false;
    private void Update()
    {
        if (isStartCoroutine)
        {
            isStartCoroutine = false; aa = Test2();
            StartCoroutine(aa);
        }
        if (onStop)
        {
            StopCoroutine(aa);
        }
    }
    private IEnumerator Test()
    {
        isStartCoroutine = false;
        yield return new WaitUntil(() => _isHitWall);
        if (_goingToRight)
        {
            Debug.Log("���ɐi�݂܂�");
            _goingToRight = false;
        }
        else
        {
            Debug.Log("�E�ɐi�݂܂�");
            _goingToRight = true;
        }
        _isHitWall = false;
        isStartCoroutine = true;
    }

    private IEnumerator Test2()
    {
        Debug.Log("10�b�҂�");
        yield return new WaitForSeconds(10f);
        Debug.Log("�I��");
    }
}