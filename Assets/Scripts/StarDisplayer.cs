using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplayer : MonoBehaviour
{

    [Range(0,3)]
    public int starsUnlocked = 0;

    [SerializeField] private Image[] stars;
    [SerializeField] AudioClip StarDing;

    AudioSource ac;

    private void Start()
    {
        Hide();
        ac = GetComponent<AudioSource>();
    }

    public void AddStar()
    {
        starsUnlocked++;
        Show();
    }

    public void Hide()
    {
        foreach (Image star in stars) star.enabled = false;
    }

    public void Show()
    {
        for (int i = 0; i < starsUnlocked; i++) stars[i].enabled = true;
        ac.PlayOneShot(StarDing);
    }
}
