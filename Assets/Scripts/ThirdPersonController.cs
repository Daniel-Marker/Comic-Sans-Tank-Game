using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    [SerializeField] float sphereRadius = 2f;
    [SerializeField] [Range(0f, 5f)] float sensitivity = 1f;
    float sphereAngleX = 0f;
    float sphereAngleY = 60f;
    Camera camera;
    Vector3 initCameraPos;

    void Start()
    {
        camera = Camera.main;
        initCameraPos = camera.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float rawVertical = Input.GetAxis("Vertical");
        float rawHorizontal = Input.GetAxis("Horizontal");

        float rawMouseX = Input.GetAxis("Mouse X");
        float rawMouseY = Input.GetAxis("Mouse Y");

        print("Mouse X: " + rawMouseX);
        print("Mouse Y: " + rawMouseY);

        //using mouseX and mouseY, move the camera about a sphere around the player with radius sphereRadius
        Vector3 newPos = new Vector3();

        sphereAngleX -= rawMouseY * sensitivity;
        sphereAngleY += rawMouseX * sensitivity;

        newPos.x = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Sin(Mathf.Deg2Rad * sphereAngleY);
        newPos.y = sphereRadius * Mathf.Cos(Mathf.Deg2Rad * sphereAngleX);
        newPos.z = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Cos(Mathf.Deg2Rad * sphereAngleY);

        camera.transform.position = newPos;
        camera.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
