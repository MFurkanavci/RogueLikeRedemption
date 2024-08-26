using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyLaser : Enemy
{
    [Header("Laser Enemy")]

    public GameObject laser;
    public GameObject laserPreviewPrefab;
    public Transform firePoint;
    public float previewDuration = 1f;
    public float nextFireTime = 2f;
    public float laserCooldown = 5f;
    public float rotationSpeed = 5f; 
    bool isAttack = false;

    protected override void Start()
    {
        float random = UnityEngine.Random.Range(-0.25f, 0.25f);
        nextFireTime += random;
        laser.SetActive(false);
        laserPreviewPrefab.SetActive(false);
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Move()
    {
        base.Move();
    }
    protected override void Die()
    {
        base.Die();
    }
    void FixedUpdate()
    {
        if (!isAttack)
        {
            LookAt();
        }
        if (Time.time >= nextFireTime)
        {
            isAttack = true;
            nextFireTime = Time.time + laserCooldown;
            StartCoroutine(AttackLaserEnemy());
        }
    }
    void LookAt()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    void PreviewLaserToLaser()
    {
        laserPreviewPrefab.SetActive(true);
    }
    void ShootLaser()
    {
        laserPreviewPrefab.SetActive(false);
        laser.SetActive(true);

    }
    void StopLaser()
    {
        laser.SetActive(false);
    }
    IEnumerator AttackLaserEnemy()
    {

        PreviewLaserToLaser();
        yield return new WaitForSeconds(1f);
        ShootLaser();
        yield return new WaitForSeconds(1f);
        StopLaser();
        isAttack = false;
    }

}