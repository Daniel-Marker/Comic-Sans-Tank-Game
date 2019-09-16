using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    //todo, clip camera movement if the camera would enter an object by moving


    [SerializeField] float sphereRadius = 2f;
    [SerializeField] [Range(0f, 5f)] float horizontalSensitivity = 1f;
    [SerializeField] [Range(0f, 5f)] float verticalSensitivity = 1f;
    [SerializeField] float maxVerticalAngle = 60f;
    [SerializeField] float minVerticalAngle = 60f;
    float sphereAngleX = 60f;
    float sphereAngleY = 180f;
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

        float oldAngleX = sphereAngleX;

        sphereAngleX -= rawMouseY * verticalSensitivity;
        sphereAngleY += rawMouseX * horizontalSensitivity;

        newPos.x = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Sin(Mathf.Deg2Rad * sphereAngleY);
        newPos.y = sphereRadius * Mathf.Cos(Mathf.Deg2Rad * sphereAngleX);
        newPos.z = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Cos(Mathf.Deg2Rad * sphereAngleY);

        camera.transform.position = newPos;
        camera.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);

        Vector3 angles = camera.transform.rotation.eulerAngles;

        if (angles.x > minVerticalAngle && angles.x < 360 - maxVerticalAngle) {
            if (angles.x < 180f)
            {
                angles.x = minVerticalAngle;
                sphereAngleX = oldAngleX;
                newPos.x = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Sin(Mathf.Deg2Rad * sphereAngleY);
                newPos.y = sphereRadius * Mathf.Cos(Mathf.Deg2Rad * sphereAngleX);
                newPos.z = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Cos(Mathf.Deg2Rad * sphereAngleY);
            }
            else {
                angles.x = 360 - maxVerticalAngle;
                sphereAngleX = oldAngleX;
                newPos.x = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Sin(Mathf.Deg2Rad * sphereAngleY);
                newPos.y = sphereRadius * Mathf.Cos(Mathf.Deg2Rad * sphereAngleX);
                newPos.z = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Cos(Mathf.Deg2Rad * sphereAngleY);
            }
        }
        camera.transform.position = newPos;
        camera.transform.rotation = Quaternion.Euler(angles);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
