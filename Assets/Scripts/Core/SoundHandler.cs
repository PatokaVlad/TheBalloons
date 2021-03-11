using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private List<AudioClip> balloonClips = new List<AudioClip>();

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip audio) => _audioSource.PlayOneShot(audio);

    public void PlayBalloonClip() => _audioSource.PlayOneShot(balloonClips[Random.Range(0, balloonClips.Count)]);

    public void WaitAndPlayClip(AudioClip audio) => StartCoroutine(WaitToPlay(audio));

    private IEnumerator WaitToPlay(AudioClip audio)
    {
        while (_audioSource.isPlaying)
        {
            yield return null;
        }

        PlayClip(audio);
    }
}
