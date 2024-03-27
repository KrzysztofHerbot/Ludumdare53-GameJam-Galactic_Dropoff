using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float killDistance = 2f;
    [SerializeField] private float killTime = 2f;
    private ParticleSystem shootParticle;
    [SerializeField] private GameObject prefabPlayerExplosion;
    [SerializeField] private GameObject prefabPackageExplosion;
    [SerializeField] private Transform pivotPoint;

    [SerializeField] private bool isAbleToAttackPlayer;
    [SerializeField] private bool isAbleToAttackPackage;
    [SerializeField] AudioClip explosionSound;

    AudioSource ac;
    LineScript lr;
    GameObject player;
    GameObject package;

    bool isAttacking;
    
    private void Start()
    {
        shootParticle = GetComponentInChildren<ParticleSystem>();
        ac = GetComponent<AudioSource>();
        lr = GetComponentInChildren<LineScript>();
        isAttacking = false;
    }

    private void Update()
    {
        // Check for player within kill distance
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, killDistance);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider is a player
            if (collider.gameObject.CompareTag("Player") && isAbleToAttackPlayer && !isAttacking)
            {
                isAttacking = true;
                player = collider.gameObject;
                // Aim at the player
                LookAtTarget(player);

                // Kill the player
                Invoke("DestroyTarget",killTime);
                
            }
            else if(collider.gameObject.CompareTag("Package") && isAbleToAttackPackage && !isAttacking)
            {
                isAttacking = true;
                package = collider.gameObject;
                // Aim at the package
                LookAtTarget(package);

                // Kill the package
                Invoke("DestroyPackage",killTime);
            }
        }
    }

    // Draw a gizmo to show the kill radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killDistance);
    }

    private void LookAtTarget(GameObject target)
    {
        // Get the direction from the current position to the target position
        Vector3 direction = target.transform.position - transform.position;

        // Calculate the angle of rotation needed to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the sprite to face the target
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void DestroyTarget()
    {
        if(Vector2.Distance(transform.position,player.transform.position)<killDistance)
        {
            GameObject explosion = Instantiate(prefabPlayerExplosion,player.transform.position, Quaternion.identity, this.transform);
            ac.PlayOneShot(explosionSound);
            Destroy(player);
            FindObjectOfType<LabelReset>().Show();
            Transform[] pivotPoints = new Transform[] { pivotPoint, explosion.transform };
            lr.SetUpLine(pivotPoints,0.6f,0.6f);
            Invoke("DestroyLaserLine",1f);
        }
        else
        {
            isAttacking = false;
        }
        
    }

    private void DestroyPackage()
    {
        if(Vector2.Distance(transform.position,package.transform.position)<killDistance)
        {
            GameObject explosion = Instantiate(prefabPackageExplosion,package.transform.position, Quaternion.identity, this.transform);
            ac.PlayOneShot(explosionSound);
            Destroy(package);
            Transform[] pivotPoints = new Transform[] { pivotPoint, explosion.transform };
            lr.SetUpLine(pivotPoints,0.6f,0.6f);
            Invoke("DestroyLaserLine",1f);
        }
        else
        {
            isAttacking = false;
        }
        
    }

    private void DestroyLaserLine()
    {
        if (lr !=null)
        {
            lr.DestroyLine();
            isAttacking = false;
        }
    }
}
