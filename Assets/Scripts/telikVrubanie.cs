#nullable disable

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class telikVrubanie : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    public VideoPlayer video;
    public AudioSource music;
    public bool canChangeState;
    public bool isPlaying;
    public bool videoType;
    public bool musicType;
    public Animator anim;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (videoType)
        {
            if (isPlaying) { video.Play(); }
            if (!isPlaying) { video.Pause(); }
        }

        if (musicType)
        {
            if (isPlaying) { music.UnPause(); }
            if (!isPlaying) { music.Pause(); }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { canChangeState = true; }
    }
    private void OnTriggerExit(Collider other)
    {
        canChangeState = false;
    }
}
