using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class btnFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip HighlightedFX;
    public AudioClip ClickFX;

    public void HighlightedSound()
    {
        myFX.PlayOneShot(HighlightedFX);
    }
    public void ClickSound()
    {
        myFX.PlayOneShot(ClickFX);
    }

}