using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDelay : MonoBehaviour
{
    [SerializeField] int delay = 3;
    [SerializeField] Text countDownText;

    EnemyAI[] enemies;
    ThirdPersonController player;


    void Start()
    {
        enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemies) {
            enemy.enabled = false;
        }

        player = FindObjectOfType<ThirdPersonController>();
        player.enabled = false;

        StartCoroutine(Countdown(delay));
    }

    IEnumerator Countdown(int delay)
    {
        while (delay != 0) {
            countDownText.text = delay + "!";
            yield return new WaitForSeconds(1f);
            delay--;
        }

        foreach (EnemyAI enemy in enemies)
        {
            enemy.enabled = true;
        }

        player.enabled = true;
        countDownText.enabled = false;
    }
}
