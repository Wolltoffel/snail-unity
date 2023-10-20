using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the playing of audio clips in the game.
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioClip[] audioClips;
    public static SoundManager instance;

    private void Awake()
    {
            instance = this;
    }

    /// <summary>
    /// Plays the audio clip at the specified index.
    /// </summary>
    /// <param name="index">The index of the audio clip to play.</param>
    public void PlaySound(int index)
    {
        AudioSource.PlayClipAtPoint(audioClips[index], Camera.main.transform.position);
    }

    /// <summary>
    /// Plays the audio clip with the specified name.
    /// </summary>
    /// <param name="name">The name of the audio clip to play.</param>
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

    /// <summary>
    /// Waits until the audio clip with the specified name is over.
    /// </summary>
    /// <param name="name">The name of the audio clip.</param>
    /// <returns>Coroutine to wait until the sound is over.</returns>
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
