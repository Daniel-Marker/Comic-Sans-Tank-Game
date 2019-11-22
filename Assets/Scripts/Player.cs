using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpPower = 100.0f;
    Rigidbody rigidBody;
 
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce((Vector3.forward * moveSpeed) * Time.deltaTime);
        }   
        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce((-Vector3.forward * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce((Vector3.left * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce((Vector3.right * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector3.up * jumpPower);
        }
    }       
   }
