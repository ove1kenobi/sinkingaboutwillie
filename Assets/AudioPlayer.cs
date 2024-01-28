using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    public AudioClip [] clips;

    private AudioSource audioSource;
    private void Start ()
    {
        audioSource = GetComponent<AudioSource> ();
        if (clips.Length > 0)
        {
            int randomIndex = Random.Range (0, clips.Length);
            AudioClip randomClip = clips [randomIndex];
            audioSource.clip = randomClip;
            //audioSource.PlayOneShot (randomClip);
        }
    }

    private void Update ()
    {
        //if (Input.GetKeyDown (KeyCode.K))
        //{
        //    PlaySound ();
        //}
    }

    public void PlaySound ()
    {

        if (clips.Length > 0)
        {
            int randomIndex = Random.Range (0, clips.Length);
            AudioClip randomClip = clips [randomIndex];
            audioSource.PlayOneShot (randomClip);
        }
    }
}
