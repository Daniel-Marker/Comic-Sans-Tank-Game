using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
    {
    [SerializeField] float waitTime = 2f;
    [SerializeField] GameObject teleporterDoor;
    [SerializeField] int noOfEnemies;
    [SerializeField] Text thanksForPlaying;
    [SerializeField] int enemiesLeft;

        bool active = true;

    private void Start()
    {
        enemiesLeft = noOfEnemies;
    }

    public void EnemyDeath() {
        enemiesLeft--;

        if (enemiesLeft == 0)
        {
            if (SceneManager.GetActiveScene().name == "Boss")
            {
                thanksForPlaying.enabled = true;
            }
            else {
                MoveDoor();
            }

           
        }
    }

    private void Update()
    {
        if (enemiesLeft == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                thanksForPlaying.gameObject.active = true;
            }


        }
    }

    private void MoveDoor()
    {
        teleporterDoor.SetActive(false);
        StartCoroutine(DoorTextFunc());
    }

    IEnumerator DoorTextFunc()
    {
        var UI = GameObject.Find("UI");
        var DoorText = UI.transform.FindChild("DoorText");
        DoorText.gameObject.SetActive(true);
        var text = DoorText.GetComponent<Text>();
        text.canvasRenderer.SetAlpha(0f);
        text.CrossFadeAlpha(1f, 2f, false);
        yield return new WaitForSeconds(2f);
        text.CrossFadeAlpha(0f, 2f, false);

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