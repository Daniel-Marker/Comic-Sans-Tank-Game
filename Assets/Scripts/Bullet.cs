using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxDistance = 5f;
    public float damage = 10f;
    public int maxReflections = 2;

    int noOfReflections = 0;
    float oldRotation = 0f;

    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        if (noOfReflections == maxReflections) {
            Destroy(gameObject);
        }

        transform.Translate(-transform.forward * speed);

        if (Vector3.Distance(initPos, transform.position) >= maxDistance) {
            Destroy(gameObject);
        }

        Debug.DrawRay(transform.position, transform.up, Color.green, 10f);
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
            else
            {
                Vector3 currRot = transform.rotation.eulerAngles;

                if (currRot.y < 90) currRot.y += 90f;
                else if (currRot.y < 180) currRot.y -= 90f;
                else if (currRot.y < 270) currRot.y += 90f;
                else if (currRot.y < 360) currRot.y -= 90f;

                transform.rotation = Quaternion.Euler(currRot);
                noOfReflections++;
            }
        }

        if (destroyWhenDone) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 currRot = transform.rotation.eulerAngles;
        currRot.y -= 45/2f;
        transform.rotation = Quaternion.Euler(currRot);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
