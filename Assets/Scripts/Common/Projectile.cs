using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projectileName;
    public int MaxSize;
    public float duration;
    public float projectileSpeed;
    public int reflection;

    private RangeWeaponHandler rangeWeaponHandler;
    private Vector2 direction;

    private bool isActive;
    private float currentDuration;
    private Rigidbody2D proejectileRigidbody;
    private Collider2D myCollider;
    private Collider2D ownerCollider;

    private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    private int currentRelfection;

    public void Init()
    {
        isActive = false;
        currentDuration = 0f;
        proejectileRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    public void SetProjectile(RangeWeaponHandler rangeWeaponHandler, Transform pivot, Quaternion rotate, LayerMask targetMask, Collider2D ownerCollider)
    {
        this.rangeWeaponHandler = rangeWeaponHandler;
        this.ownerCollider = ownerCollider;
        this.targetMask = targetMask;

        transform.position = pivot.position;
        transform.rotation = pivot.rotation * rotate;

        direction = transform.right;

        isActive = true;
        currentDuration = 0;

        currentRelfection = reflection + rangeWeaponHandler.UpgradeData.addReflection;

        Physics2D.IgnoreCollision(myCollider, ownerCollider, true);
    }

    private void Update()
    {
        if (!isActive) return;

        currentDuration += Time.deltaTime;

        if (currentDuration >= duration)
        {
            ReleaseProjectile();
            return;
        }

        proejectileRigidbody.velocity = direction * (projectileSpeed + rangeWeaponHandler.UpgradeData.addProjectileSpeed);
    }

    private void ReleaseProjectile()
    {
        rangeWeaponHandler.connectedPool.Release(this);
        Physics2D.IgnoreCollision(myCollider, ownerCollider, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((targetMask & (1 << collision.gameObject.layer)) != 0) //Ÿ�ٿ� �浹�� �������� �ְ� �����
        {
            if (collision.gameObject.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage(rangeWeaponHandler.GetAttackDamage());
                //Debug.Log($"{collision.gameObject.name} ���� {rangeWeaponHandler.GetAttackDamage()} ������!");
            }
            ReleaseProjectile();
        }
        else if ((obstacleMask & (1 << collision.gameObject.layer)) != 0) //��ֹ��� �浹
        {
            if (currentRelfection == 0) //ƨ�� �� �ִ� Ƚ���� ������ �߻�ü ȸ��
            {
                ReleaseProjectile();
            }
            else
            {
                currentRelfection--;
                if (direction == Vector2.Reflect(direction, collision.contacts[0].normal))  //�浹ó�� �� �Ի簢�� �ݻ簢�� �����϶� ���� ���� ������ ���� ��찡 �ִ�. �ݻ簢�� ����� ������ ����.
                {
                    direction = -direction;                                                 //����ó�� �� �Ի簢�̴� -�� �ٿ� �ݴ� �������� �ٲ�����                                            
                }
                else
                {
                    //Debug.Log($"{collision.contacts[0].normal} : {direction}");
                    direction = Vector2.Reflect(direction, collision.contacts[0].normal);   //���� ������ �Ի簢���� �浹ü�κ��� �ݻ簢�� ���Ͽ� ƨ�ܳ��� ���� ����
                }
                float RotZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;          //���ο� �������� �����̼��� ��Ű�� ���� ��ũ ź��Ʈ�� ȸ���� ���ϱ�
                transform.rotation = Quaternion.Euler(0, 0, RotZ);                          //ȸ���� ����
            }
        }
    }
}
