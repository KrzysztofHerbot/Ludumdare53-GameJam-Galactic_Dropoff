using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PackageGoal : MonoBehaviour
{
    bool completedDelivery;
    [SerializeField] AudioClip deliveredSound;

    AudioSource ac;

    Material mat;
    Color startColor;
    Color completeColor;

    public bool IsCompleted()
    {
        return completedDelivery;
    }

    private void Start()
    {
        completedDelivery = false;
        ac = GetComponent<AudioSource>();
        mat = GetComponent<MeshRenderer>().material;
        startColor = mat.GetColor("_EmissionColor");
        completeColor = new Color(0f, 0.3f, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (completedDelivery) return;

        if (collision.gameObject.tag == "Package")
        {
            // Get a reference to the package component from the children of the other collider
            Package package = collision.gameObject.GetComponentInChildren<Package>();

            // Do something with the package component
            package.BeCollectedBy(this);
            CompleteDelivery();
        }
    }

    private void CompleteDelivery()
    {
        completedDelivery = true;
        ac.PlayOneShot(deliveredSound);
        mat.SetColor("_EmissionColor", completeColor);
    }
}