using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Shotgun}


    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;
    public float rotation = 1f;
    public float rotationSpeed = 1f;
    public float angleStep = 20;
    public float burstCount = 1;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotation);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + rotationSpeed);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }


    private void Fire()
    {
        if (bullet)
        {
            if (spawnerType == SpawnerType.Shotgun)
            {

                float angleStart = transform.eulerAngles.z;
                for (int i = 0; i < burstCount; i++)
                {

                    spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                    spawnedBullet.GetComponent<Bullet>().speed = speed;
                    spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                    spawnedBullet.transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + (angleStep * i+ angleStep/2) - ((angleStep * burstCount)/2));
                }
            }
            else
            {
                spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                spawnedBullet.transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z);
            }
                
        }
    }
}
