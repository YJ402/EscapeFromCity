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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            zombie.SteppedHead();
        }
    }
}
