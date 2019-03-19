using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip squashed;
    public AudioClip shoot;
    public AudioClip angryCustomer;
    public AudioClip jumpSound;

    public void PlaySquashedSound()
    {
        AudioSource.PlayClipAtPoint(squashed, Vector3.zero);
    }

    public void PlayShootSound()
    {
        AudioSource.PlayClipAtPoint(shoot, Vector3.zero);
    }

    public void PlayAngryCustomerSound()
    {
        AudioSource.PlayClipAtPoint(angryCustomer, Vector3.zero);
    }
    
    public void PlayJumpSound()
    {
        AudioSource.PlayClipAtPoint(jumpSound, Vector3.zero);
    }
}
