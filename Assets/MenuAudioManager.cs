using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using URPGlitch.Runtime.AnalogGlitch;
using static UnityEditor.Rendering.CameraUI;

public class MenuAudioManager : MonoBehaviour
{
    
    public PlayableDirector director;
    public int audioTrackIndex = 1; // Index of the 2nd audio track (starting from 0)

    private AudioSource audioSource;
    public AudioClip audioclipz;

    void Start ()
    {
        // Get the audio source of the 2nd audio track
        var timelineAsset = (TimelineAsset) director.playableAsset;
        var audioTrack = (AudioTrack) timelineAsset.GetOutputTrack (audioTrackIndex);
        //var clipPlayableAsset = audioTrack.GetClips ().FirstOrDefault ()?.asset as AudioClip;
        audioclipz = audioTrack.GetClips ().FirstOrDefault ()?.asset as AudioClip;
        //AudioSource audioSource = clipPlayableAsset.GetComponentInParent<AudioSource>();

    }

    void Update ()
    {
        // Read the volume of the audio source
       //float volume =  audioclipz.;

        //Debug.Log ("Volume of 2nd audio track: " + volume);
    }
}
/*
//public GameObject m_postProcessing = null;
//public AnalogGlitchVolume m_analogGlitch = null;
public AudioSource m_interference = null;

    //private bool m_done = false;

    [SerializeField] public Volume m_volume;

    public AnalogGlitchVolume m_analogGlitchVolume;



    private void Start ()

    {

        m_volume.profile.TryGet<AnalogGlitchVolume> (out m_analogGlitchVolume);



        // Get the PlayableDirector component on the GameObject
        var director = GetComponent<PlayableDirector> ();

        TimelineAsset timelineAsset = (TimelineAsset) director.playableAsset;
        AudioTrack audioTrack = (AudioTrack) timelineAsset.GetOutputTrack (1);

        var clipPlayableAsset = audioTrack.GetClips () [0].asset as AudioClipPlayableAsset;
        audioSource = clipPlayableAsset.sourceGameObject.GetComponent<AudioSource> ();




        //director.playableAsset.outputs
        //foreach (var output in director.playableAsset.outputs)


        // Get the Audio Track from the Timeline
        //var audioTrack = (AudioTrack) director.playableAsset. (0);

        // Get the current audio clip being played
        var currentClip = audioTrack.GetDefaultPlayable (director).GetInput (0).GetPlayableAsset () as AudioClip;

        // Get the AudioSource component on the same GameObject
        var audioSource = GetComponent ();

        // Get the current audio volume
        var currentVolume = audioSource.volume;

        // Use the current volume to change the material brightness

    }

    private void Update ()
    {
        //m_analogGlitchVolume.scanLineJitter.value = m_interference.volume;
        Debug.Log (m_interference.volume);

    }



    //public void ChangeEffect ()

    //{

    //    analogGlitchVolume.scanLineJitter.value = 0.25f;

    //    analogGlitchVolume.verticalJump.value = ;

    //    analogGlitchVolume.horizontalShake.value = 0.25f;

    //    analogGlitchVolume.colorDrift.value = 0.25f;

    //}




}

*/