using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{

    [Header("------Audio-------")]
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource sfxSource;

    [Header("------Clips------")]
    public AudioClip backgroundClip;
    public AudioClip shootClip;
    public AudioClip hitClip;


    // Start is called before the first frame update
    void Start()
    {
        backgroundSource.clip = backgroundClip;
        backgroundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
