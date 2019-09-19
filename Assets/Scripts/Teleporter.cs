using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
    {
        public Teleporter destination;

        bool active = true;

        void OnTriggerEnter(Collider other)
        {
            if (active == true)
            {
                if (other.tag == "Player")
                {
                    destination.active = false;
                    other.transform.position = destination.transform.position;
                }
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                active = true;
            }
        }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}