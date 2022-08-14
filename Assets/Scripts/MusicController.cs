using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioSource> musicList;
    private static AudioSource currentTrack = null;

    private void Start()
    {
        StartCoroutine("TryStartNewTrack");
    }

    private void StartNewTrack()
    {
        int trackId;
        //Чтобы одна и та же музыка не проигрывалась 2 раза подряд
        do
        {
            trackId = Random.Range(0, musicList.Count);
        } while (currentTrack == musicList[trackId]);
        currentTrack = musicList[trackId];

        currentTrack.Play();

    }

    IEnumerator TryStartNewTrack()
    {
        yield return new WaitForSeconds(1);
        if (!currentTrack || !currentTrack.isPlaying)
            StartNewTrack();
        StartCoroutine("TryStartNewTrack");
    }

}
