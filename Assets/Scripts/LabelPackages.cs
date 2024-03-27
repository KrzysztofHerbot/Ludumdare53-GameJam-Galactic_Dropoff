using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelPackages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PackageCount;
    LaunchPackageOnClick AmmoStore;

    void Start()
    {
        //GameObject PMObject = GameObject.Find("Progress Manager"); // If name of progress manager in a scene will be changed it will mess up
        ProgressManager InitialAmmoStore = FindObjectOfType<ProgressManager>();//PMObject.GetComponent<ProgressManager>();
        PackageCount.text = InitialAmmoStore.initialAmmo.ToString();
    }

    void Update()
    {
        if(AmmoStore != null)
        {
            PackageCount.text = AmmoStore.ammo.ToString();
        }
    }

    public void SetAmmoCountOnLaunch()
    {
        // If name of progress manager in a scene will be changed it will mess up
        AmmoStore =  FindObjectOfType<LaunchPackageOnClick>();//GameObject.Find("Ship").GetComponent<LaunchPackageOnClick>();
    }

}