using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    Zombie zombie;

    private void Start()
    {
        zombie = GetComponentInParent<Zombie>();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("Player"))
    //    {
    //        zombie.SteppedHead();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            zombie.SteppedHead();
        }
        
    }
}
