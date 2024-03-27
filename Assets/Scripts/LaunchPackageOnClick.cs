using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Ship))]
public class LaunchPackageOnClick : MonoBehaviour
{

    ProgressManager progressManager;
    LabelPackages labelPackages;
    public int ammo = int.MaxValue;

    public GameObject packagePrefab;
    public float launchStrength = 1f;

    private Rigidbody2D rb;

    ShipAimIndicator sai;
    Ship ship;
    [SerializeField] AudioClip throwSound;

    AudioSource ac;

    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        ac = GetComponent<AudioSource>();
        if (progressManager != null) ammo = progressManager.initialAmmo;
        
        //added by Kaech - not efficient code that will probably work -01.05.23, 14.00
        FindObjectOfType<LabelPackages>().SetAmmoCountOnLaunch();
        //end

        rb = GetComponent<Rigidbody2D>();
        sai = GetComponentInChildren<ShipAimIndicator>();
        ship = GetComponent<Ship>();

        if (sai==null)
        {
            Debug.LogError("LaunchPackageOnClick: No ship aim indicator could be found in higherarchy.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) LaunchPackage();
    }

    void LaunchPackage() {
    
        // Return early if out of ammo.
        if (ammo <= 0)
        {
            OutOfAmmo();
            return;
        }

        ship.packagesLaunched++;
        ammo--;

        GameObject packageInstance = Instantiate(packagePrefab, transform.position, Quaternion.identity);
        ac.PlayOneShot(throwSound);
        Rigidbody2D packageBody = packageInstance.GetComponent<Rigidbody2D>();
        if (packageBody != null)
        {
            Debug.Log(sai.GetDirection());
            Vector2 launchForce = sai.GetDirection() * launchStrength;
            packageBody.AddForce(launchForce, ForceMode2D.Impulse);
            rb.AddForce(-launchForce, ForceMode2D.Impulse);
        }
    }

    void OutOfAmmo()
    {
        LabelReset resetLabel = FindObjectOfType<LabelReset>();
        if (resetLabel != null) resetLabel.Show();
    }

}