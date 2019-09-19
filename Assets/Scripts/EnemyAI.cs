using UnityEngine;
using UnityEngine.AI;

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

    [Header("Bullet settings")]
    [SerializeField] Bullet bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxReflections = 2;

    ThirdPersonController player;
    float prevTime;
    float oldTime;
    NavMeshAgent agent;


    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
        oldTime = Time.time;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player)
        {
            if (!Physics.Linecast(transform.position, player.transform.position))
            {
                tankHead.transform.up = player.transform.position - transform.position;
                tankHead.transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                Vector3 currRot = tankHead.transform.rotation.eulerAngles;
                currRot.x += 90f;
                currRot.z = 0;
                tankHead.transform.rotation = Quaternion.Euler(currRot);
            }

            TankAI();
        }
    }

    private void TankAI()
    {
        switch (enemyType) {
            case EnemyType.stationary:
                ShootAtPlayer();
                Destroy(agent);
                break;

            case EnemyType.basic:
                ShootAtPlayer();
                agent.SetDestination(player.transform.position + Vector3.forward*2);
                break;
        }
    }

    private void ShootAtPlayer()
    {
        prevTime = Time.time;
        if (prevTime - oldTime >= timeBetweenShots && !Physics.Linecast(transform.position, player.transform.position))
        {
            var newBullet = Instantiate(bullet, cannon.transform.position + cannon.transform.up, cannon.transform.rotation);
            newBullet.speed = bulletSpeed;
            newBullet.maxDistance = maxDistance;
            newBullet.damage = damage;
            newBullet.maxReflections = maxReflections;
            oldTime = prevTime;
        }
    }
}
