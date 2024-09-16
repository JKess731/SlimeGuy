using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MachineGun : Attacks
{
    private Rigidbody2D _rb;
    private float _bulletSpeed;
    private float _range;
    private float _fireRate;
    private float _nextFireTime;

    [SerializeField] private int _bulletCount = 5;      
    [SerializeField] private float _spreadAngle = 30f;   

    private Vector3 _startPosition;
    private GameObject _player;

    [SerializeField] private Transform firePoint;        
    [SerializeField] private GameObject _bulletPrefab; 
    private BulletStruct _bulletStruct;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("player");
    }

    private void Update()
    {
        if (Time.time >= _nextFireTime)
        {
            FireBullets();
            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    private void FireBullets()
    {
        
        float angleOffset = -_spreadAngle / 2;
        float angleStep = _spreadAngle / (_bulletCount - 1);

        for (int i = 0; i < _bulletCount; i++)
        {
            float currentAngle = angleOffset + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, currentAngle) * firePoint.rotation;

            
            GameObject newBullet = Instantiate(_bulletPrefab, firePoint.position, bulletRotation);
            newBullet.GetComponent<Bullet>().SetBulletStruct(_bulletStruct);
        }
    }

    
    public void SetMachineGunStruct(MachineGunStruct machineGunStruct)
    {
        _damage = machineGunStruct.Damage;
        _range = machineGunStruct.Range;
        _bulletSpeed = machineGunStruct.BulletSpeed;
        _knockback = machineGunStruct.Knockback;
    }

    
    public void SetBulletStruct(BulletStruct bulletStruct)
    {
        _bulletStruct = bulletStruct;
    }
}
