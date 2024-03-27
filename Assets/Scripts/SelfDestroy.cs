using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField]private float TimeToDestroy = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("selfDestruct",TimeToDestroy);
    }

    void selfDestruct()
    {
        Destroy(gameObject);
    }
}
