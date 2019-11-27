using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    [Header("Attributes")]

    public float range = 15f;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Setup field")]

    public string enemyTag = ("Enemy");

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void Update()
    {
        if (target == null)
            return;
        //Target lock on
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotatoin = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotatoin, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Gets an array of all the enemies
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies) //Foreach loops
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //Returns distance in units
            if (distanceToEnemy < shortestDistance) //sees if distance is shorter than distance that is found before
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform; //finds the closest target
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); //range of the turret to attack enemy
    }

    void Shoot()
    {
        GameObject bullet60 = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bullet60.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target); //bullet follows target
        Debug.Log("Shoot");
    }
}
