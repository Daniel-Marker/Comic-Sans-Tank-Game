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
    [SerializeField] GameObject player;
    float sphereAngleX = 60f;
    float sphereAngleY = 180f;
    Camera camera;
    Vector3 initCameraPos;
    bool onGround = false;

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

        if (onGround) {
            if (rawMouseY < 0) {
                rawMouseY = 0;
            }
        }

        //using mouseX and mouseY, move the camera about a sphere around the player with radius sphereRadius
        Vector3 newPos = new Vector3();
        Vector3 oldPos = camera.transform.position;

        float oldAngleX = sphereAngleX;
        float oldAngleY = sphereAngleY;

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

        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(oldPos, camera.transform.position, out hit))
        {
            camera.transform.position = oldPos;
            onGround = true;
            //currently only just lets the camera into the ground
            //so need to make the angle of the linecast slightly steeper
            //also doesn't work with ceilings, so need to fix for that

        }
        else {
            onGround = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
