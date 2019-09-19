using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    ThirdPersonController player;

    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
    }

    void Update()
    {
        if (player)
        {
            RaycastHit hit;
            Debug.DrawLine(transform.position, player.GetTankHead().transform.position, Color.red);
            if (Physics.Linecast(transform.position, 0.2f * (transform.position - player.GetTankHead().transform.position) + player.GetTankHead().transform.position, out hit))
            {
                hit.transform.gameObject.GetComponent<Wall>().BecomeTransparent();
            }
        }
    }
}
