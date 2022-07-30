using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _clip;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void Play00()
    {
        _audioSource.clip = _clip[0];
        _audioSource.Play();
    }

    public void Play01()
    {
        _audioSource.clip = _clip[1];
        _audioSource.Play();
    }

    public void Play02()
    {
        _audioSource.clip = _clip[2];
        _audioSource.Play();
    }
}
