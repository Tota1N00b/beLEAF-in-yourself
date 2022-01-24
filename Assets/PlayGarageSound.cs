using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGarageSound : MonoBehaviour
{
    public AudioClip _audioClip;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PublicVars.GarageOpen)
        {
            _audioSource.PlayOneShot(_audioClip);
            PublicVars.GarageOpen = false;
        }
    }
}