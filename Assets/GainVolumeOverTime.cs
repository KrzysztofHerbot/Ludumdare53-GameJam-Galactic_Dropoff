using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainVolumeOverTime : MonoBehaviour
{
    [SerializeField] private float volumeFadeDuration = 4f;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float volumeGainRate = 1f / volumeFadeDuration;
        audioSource.volume += volumeGainRate * Time.deltaTime;
    }
}
