using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioSource> musicList;
    private AudioSource currentTrack = null;

    private void Start()
    {
        StartNewTrack();
    }

    private void StartNewTrack()
    {
        int trackId;
        //����� ���� � �� �� ������ �� ������������� 2 ���� ������
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
        if (!currentTrack.isPlaying)
            StartNewTrack();
    }

}
