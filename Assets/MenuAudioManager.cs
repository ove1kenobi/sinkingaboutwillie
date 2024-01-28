using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;

public class MenuAudioManager : MonoBehaviour
{
    //public GameObject m_postProcessing = null;
    //public AnalogGlitchVolume m_analogGlitch = null;
    public AudioSource m_interference = null;

    //private bool m_done = false;

    [SerializeField] public Volume m_volume;

    public AnalogGlitchVolume m_analogGlitchVolume;



    private void Start ()

    {

        m_volume.profile.TryGet<AnalogGlitchVolume> (out m_analogGlitchVolume);

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

