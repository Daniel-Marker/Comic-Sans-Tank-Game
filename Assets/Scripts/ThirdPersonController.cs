using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Camera settings")]
    [SerializeField] float sphereRadius = 2f;
    [SerializeField] [Range(0f, 5f)] float horizontalSensitivity = 1f;
    [SerializeField] [Range(0f, 5f)] float verticalSensitivity = 1f;
    [SerializeField] float maxVerticalAngle = 60f;
    [SerializeField] float minVerticalAngle = 60f;

    [Header("Aiming settings")]
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject tankHead;
    [SerializeField] GameObject tankBody;
    [SerializeField] float cannonOffsetAngle = 20f;
    [SerializeField] float cannonRotateSpeed = 20f;

    [Header("Movement settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;

    [Header("Bullet settings")]
    [SerializeField] Bullet bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxReflections = 2;

    float sphereAngleX = 60f;
    float sphereAngleY = 180f;
    Camera camera;
    Vector3 initCameraPos;
    bool onGround = false;
    bool againstWallForward = false;
    bool againstWallBackward = false;
    [SerializeField] float headAngle = 180f;

    public GameObject GetTankHead() {
        return tankHead;
    }

    void Start()
    {
        camera = Camera.main;
        initCameraPos = camera.transform.position;
    }

    void Update()
    {
        float rawVertical = Input.GetAxis("Vertical");
        float rawHorizontal = Input.GetAxis("Horizontal");
        RotateHeadAndCannon();

        Vector3 movement = new Vector3(0,0,rawVertical*moveSpeed*Time.deltaTime);
        transform.Translate(movement);

        transform.Rotate(transform.up, rotateSpeed * rawHorizontal * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) {
            var newBullet = Instantiate(bullet, cannon.transform.position + cannon.transform.up, cannon.transform.rotation);
            newBullet.speed = bulletSpeed;
            newBullet.maxDistance = maxDistance;
            newBullet.damage = damage;
            newBullet.maxReflections = maxReflections;
        }

        Vector3 currRot = transform.rotation.eulerAngles;
        currRot.x = 0;
        currRot.z = 0;
        transform.rotation = Quaternion.Euler(currRot);

    }

    private void RotateHeadAndCannon()
    {
        float rawVertical = 0;
        float rawHorizontal = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rawHorizontal -= 1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rawHorizontal += 1f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rawVertical += 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rawVertical -= 1f;
        }

        headAngle += cannonRotateSpeed * rawHorizontal * Time.deltaTime;
        tankHead.transform.rotation = Quaternion.Euler(90, headAngle, 0);
    }

    private void RotateTankBody() {
        float rawVertical = 0;
        float rawHorizontal = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            rawHorizontal -= 1f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rawHorizontal += 1f;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            rawVertical += 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            rawVertical -= 1f;
        }

        transform.Rotate(transform.up, rotateSpeed * rawHorizontal * Time.deltaTime);
    }

    private void CameraStuff()
    {
        float rawMouseX = Input.GetAxis("Mouse X");
        float rawMouseY = Input.GetAxis("Mouse Y");

        if (onGround)
        {
            if (rawMouseY < 0)
            {
                rawMouseY = 0;
            }
        }

        if (againstWallForward)
        {
            if (sphereAngleY > 180)
            {
                if (rawMouseX > 0)
                {
                    rawMouseX = 0;
                }
            }
            else
            {
                if (rawMouseX < 0)
                {
                    rawMouseX = 0;
                }
            }
        }

        if (againstWallBackward)
        {
            if (sphereAngleY > 180)
            {
                if (rawMouseX < 0)
                {
                    rawMouseX = 0;
                }
            }
            else
            {
                if (rawMouseX > 0)
                {
                    rawMouseX = 0;
                }
            }
        }

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

        if (angles.x > minVerticalAngle && angles.x < 360 - maxVerticalAngle)
        {
            if (angles.x < 180f)
            {
                angles.x = minVerticalAngle;
                sphereAngleX = oldAngleX;
                newPos.x = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Sin(Mathf.Deg2Rad * sphereAngleY);
                newPos.y = sphereRadius * Mathf.Cos(Mathf.Deg2Rad * sphereAngleX);
                newPos.z = sphereRadius * Mathf.Sin(Mathf.Deg2Rad * sphereAngleX) * Mathf.Cos(Mathf.Deg2Rad * sphereAngleY);
            }
            else
            {
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
        if (Physics.Linecast(oldPos, camera.transform.position - Vector3.up, out hit))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (Physics.Linecast(oldPos, camera.transform.position + Vector3.forward, out hit))
        {
            againstWallForward = true;
        }
        else
        {
            againstWallForward = false;
        }

        if (Physics.Linecast(oldPos, camera.transform.position - Vector3.forward))
        {
            againstWallBackward = true;
        }
        else
        {
            againstWallBackward = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Camera.main.transform.position, (Camera.main.transform.position - Vector3.up));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Vector3.forward);
    }
}
