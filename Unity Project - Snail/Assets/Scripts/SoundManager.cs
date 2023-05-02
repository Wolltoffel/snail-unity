using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]AudioClip[] audioClips;
    public static SoundManager soundManager;

    private void Awake()
    {
        if (soundManager != null && soundManager != this)
            Destroy(this);
        else
            soundManager = this;

        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    public void PlaySound (string name)
    {
        AudioClip selectedClip=null;
        foreach(AudioClip audioClip in audioClips)
        {
            if (audioClip.name == name)
                selectedClip = audioClip;
        }

        if (selectedClip != null)
            audioSource.PlayOneShot(selectedClip);
    }

}
