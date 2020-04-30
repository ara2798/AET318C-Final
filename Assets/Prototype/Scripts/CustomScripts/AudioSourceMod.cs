using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceMod : MonoBehaviour
{
    AudioSource audioSource;
    public float startTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.time = startTime;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
