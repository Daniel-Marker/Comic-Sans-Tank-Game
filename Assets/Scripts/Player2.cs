using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpPower = 100.0f;
    Rigidbody rigidBody;

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddForce((Vector3.forward * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidBody.AddForce((-Vector3.forward * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.AddForce((Vector3.left * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.AddForce((Vector3.right * moveSpeed) * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            rigidBody.AddForce(Vector3.up * jumpPower);
        }
    }
}