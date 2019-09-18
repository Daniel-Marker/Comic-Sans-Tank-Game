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

        Debug.DrawRay(transform.position, transform.up, Color.green, 10f);
        print(transform.up);
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
                RaycastHit hit;
                if (Physics.Linecast(transform.position, 0.2f*transform.up, out hit)) {
                    //float theta = Mathf.Rad2Deg * Mathf.Atan2(hit.point.z - transform.position.z, hit.point.x - transform.position.x);

                    //print("Transform x: " + transform.position.x);
                    //print("Transform z: " + transform.position.z);

                    //print("hit x: " + hit.point.x);
                    //print("hit x: " + hit.point.z);

                    //print("Theta: " + theta);

                    //Vector3 currRot = transform.rotation.eulerAngles;
                    //currRot.z = theta;

                    //transform.rotation = Quaternion.Euler(currRot);
                    //Debug.DrawLine(transform.position, hit.point);

                    //transform.up = -Vector3.Reflect(transform.up, Vector3.right);

                    //Vector2 direction = new Vector2();
                    //direction.x = transform.up.x;
                    //direction.y = transform.up.z;
                    //direction = Vector2.Perpendicular(direction);
                    ////direction = Vector2.Reflect(direction, hit.point);
                    //transform.up = new Vector3(direction.x, 0, direction.y);

                    //Debug.DrawRay(hit.point, transform.up);

                    //Vector3 currRot = transform.rotation.eulerAngles;
                    //currRot.x = 0;

                }
            }
        }

        if (destroyWhenDone) {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
