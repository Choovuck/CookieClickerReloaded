using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    //  RightClick
    private AudioClip clickClip;
    private AudioClip errorClip;

    void Start() {
        clickClip = Resources.Load("mouse_left_click") as AudioClip;
        errorClip = Resources.Load("error") as AudioClip;
    }

    public void PlayClickSound()
    {
        GetComponent<AudioSource>().PlayOneShot(clickClip);
    }

    public void PlayErrorSound() 
    {
        GetComponent<AudioSource>().PlayOneShot(errorClip);
    }
}
