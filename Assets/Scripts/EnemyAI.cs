using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { stationary, basic };

    [Header("Enemy properties")]
    [SerializeField] EnemyType enemyType;
    [SerializeField] float timeBetweenShots = 2f;

    [Header("Enemy parts")]
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject tankHead;
    [SerializeField] GameObject tankBody;

    ThirdPersonController player;

    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
    }

    void Update()
    {
        if (!Physics.Linecast(transform.position, player.transform.position)) {
            tankHead.transform.up = player.transform.position - transform.position;
            tankHead.transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            Vector3 currRot = tankHead.transform.rotation.eulerAngles;
            currRot.x += 90f;
            currRot.z = 0;
            tankHead.transform.rotation = Quaternion.Euler(currRot);
        }
    }
}
