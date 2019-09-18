using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxDistance = 5f;
    public float damage = 10f;

    Vector3 initPos;

    //todo, add bullet reflections

    private void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        transform.Translate(-transform.forward * speed);

        if (Vector3.Distance(initPos, transform.position) >= maxDistance) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        String tag = other.tag;
        bool destroyWhenDone = false;

        if (tag == "Player")
        {
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
            health.DecreaseHealth(damage);
            destroyWhenDone = true;
        }
        else {
            if (tag == "Enemy")
            {
                EnemyHealth health = other.GetComponentInParent<EnemyHealth>();
                health.DecreaseHealth(damage);
                destroyWhenDone = true;
            }
            else {
                print("Collided with something that isn't the player or an enemy");
            }
        }

        if (destroyWhenDone) {
            Destroy(gameObject);
        }
    }
}
