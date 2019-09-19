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

    [Header("Bullet settings")]
    [SerializeField] Bullet bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float damage = 10f;
    [SerializeField] int maxReflections = 2;

    ThirdPersonController player;
    float prevTime;
    float oldTime;

    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
        oldTime = Time.time;
    }

    void Update()
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

    private void TankAI()
    {
        switch (enemyType) {
            case EnemyType.stationary:
                prevTime = Time.time;
                if (prevTime - oldTime >= timeBetweenShots && !Physics.Linecast(transform.position,player.transform.position)) {
                    var newBullet = Instantiate(bullet, cannon.transform.position + cannon.transform.up, cannon.transform.rotation);
                    newBullet.speed = bulletSpeed;
                    newBullet.maxDistance = maxDistance;
                    newBullet.damage = damage;
                    newBullet.maxReflections = maxReflections;
                    oldTime = prevTime;
                }

                break;
            case EnemyType.basic:
                break;
        }
    }
}
