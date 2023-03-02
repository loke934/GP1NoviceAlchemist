using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSFX : MonoBehaviour
{
    [SerializeField] private AudioSource pickUpSound;

    public void PlayPickUpSFX()
    {
        if (!pickUpSound.isPlaying)
        {
            pickUpSound.Play();
        }
    }
}
