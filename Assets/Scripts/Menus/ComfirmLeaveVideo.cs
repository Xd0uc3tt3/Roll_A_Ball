using UnityEngine;
using UnityEngine.Video;

public class ComfirmLeaveVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip firstVideo;
    public VideoClip loopVideo;

    private bool firstVideoPlayed = false;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.clip = firstVideo;
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (!firstVideoPlayed)
        {
            firstVideoPlayed = true;
            vp.clip = loopVideo;
            vp.isLooping = true;
            vp.Play();
        }
    }
}

