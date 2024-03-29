﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float explosionRadius = 0f;
    public float speed = 70f;

    public void Seek(Transform _target) //sets up everything for bullet, Instantiates effect for bullet
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
        Destroy(target.gameObject);
        Destroy(gameObject);
        Debug.Log("WeHitSomething");
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

