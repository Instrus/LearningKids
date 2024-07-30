using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClip : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;

    public void PlayAudioClip()
    {
        if (audioClip != null)
        {
            AudioManager.instance.PlayClip(audioClip);
        }
        
    }
}
