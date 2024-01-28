using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;

    //public List<AudioSource> m_musicStems = null;
   
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    
    public void StartTimeline()
    {
        director.Play();
    }

    public void Update ()
    {

        //if (Input.GetKeyDown ("space"))
        //{
        //    StartTimeline();
        //}
    }
} 