using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioClip[] audioClips;
    public static SoundManager instance;

    private void Awake()
    {
            instance = this;
    }
    public void PlaySound(int index)
    {
        AudioSource.PlayClipAtPoint(audioClips[index], Camera.main.transform.position);
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
            AudioSource.PlayClipAtPoint(selectedClip, Camera.main.transform.position);
    }

    public IEnumerator WaitUntilSoundIsOver(string name)
    {
        AudioClip selectedClip = null;
        foreach (AudioClip audioClip in audioClips)
        {
            if (audioClip.name == name)
                selectedClip = audioClip;
        }

        if (selectedClip != null)
            yield return new WaitForSeconds(selectedClip.length);
    }

}
