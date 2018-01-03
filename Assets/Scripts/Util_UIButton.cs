using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_UIButton : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip select;
    public AudioClip hover;
    public AudioClip disable;

    void OnMouseOver()
    {
        audioSource.clip = hover;
        audioSource.Play();
    }

    public void ButtonSound()
    {
        if (enabled)
        {
            SFXSelect();
        } else
        {
            SFXDisabled();
        }
    }

    private void SFXSelect()
    {
        audioSource.clip = select;
        audioSource.Play();
    }

    private void SFXDisabled()
    {
        audioSource.clip = disable;
        audioSource.Play();
    }

}
