using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnKeyPress : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoPlayer finalVideo;
    public string videoFileName;
    public string finalVideoFileName;
    public Camera mainCam;
    public Camera cutsceneCam1;

    public GameObject sound;
    //public Transform cam;

    void Start()
    {
        mainCam.enabled = true;
        cutsceneCam1.enabled = false;
        // Assign the VideoPlayer component from the GameObject
        videoPlayer = GetComponent<VideoPlayer>();
        finalVideo = GetComponent<VideoPlayer>();


        // Set the video clip to play
        videoPlayer.url = "Assets/Cutscenes/" + videoFileName + ".mp4";
        finalVideo.url = "Assets/Cutscenes/" + finalVideoFileName + ".mp4";

        // Prepare the video to avoid a delay when playing
        videoPlayer.Prepare();
        finalVideo.Prepare();

        videoPlayer.loopPointReached += VideoEndReached; // Subscribe to the loopPointReached event
        finalVideo.loopPointReached += FinalVideoEndReached; // Subscribe to the loopPointReached event

    }

    void Update()
    {
        // Check if the "T" key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Check if the video is ready
            if (videoPlayer.isPrepared)
            {
                mainCam.enabled = !mainCam.enabled;
                cutsceneCam1.enabled = !cutsceneCam1.enabled;
                //cam.position = new Vector3()
                // Play the video
                Debug.Log("Play Cutscene 1");
                videoPlayer.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Check if the video is ready
            if (finalVideo.isPrepared)
            {
                mainCam.enabled = !mainCam.enabled;
                cutsceneCam1.enabled = !cutsceneCam1.enabled;
                //cam.position = new Vector3()
                // Play the video
                Debug.Log("Play Cutscene 1");
                finalVideo.Play();
            }
        }
    }

    void VideoEndReached(VideoPlayer vp)
    {
        // You can disable the VideoPlayer or hide the GameObject here
        // For example, disabling the VideoPlayer component:
        videoPlayer.Stop();
        videoPlayer.targetCamera = null; // This may prevent the last frame from staying on the screen
        videoPlayer.enabled = false;
        mainCam.enabled = !mainCam.enabled;
        cutsceneCam1.enabled = !cutsceneCam1.enabled;
        sound.GetComponent<SoundScript>().playMusic();
        // If you want to hide the GameObject (assuming it's the one this script is attached to):
        // gameObject.SetActive(false);
    }

    void FinalVideoEndReached(VideoPlayer vp)
    {
        // You can disable the VideoPlayer or hide the GameObject here
        // For example, disabling the VideoPlayer component:
        finalVideo.Stop();
        finalVideo.targetCamera = null; // This may prevent the last frame from staying on the screen
        finalVideo.enabled = false;
        mainCam.enabled = !mainCam.enabled;
        cutsceneCam1.enabled = !cutsceneCam1.enabled;
        //sound.GetComponent<SoundScript>().playMusic();
        // If you want to hide the GameObject (assuming it's the one this script is attached to):
        // gameObject.SetActive(false);
    }
}
