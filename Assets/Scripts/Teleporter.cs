using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
    {
    [SerializeField] float waitTime = 2f;
    [SerializeField] GameObject teleporterDoor;
    [SerializeField] int noOfEnemies;
    int enemiesLeft;

        bool active = true;

    private void Start()
    {
        enemiesLeft = noOfEnemies;
    }

    public void EnemyDeath() {
        enemiesLeft--;

        if (enemiesLeft == 0)
        {
            MoveDoor();
        }
    }

    private void MoveDoor()
    {
        teleporterDoor.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
        {
            if (active == true)
            {
                if (other.tag == "Player")
                {
                FindObjectOfType<LevelManager>().NextLevel(waitTime);
                }
            }
        }
}