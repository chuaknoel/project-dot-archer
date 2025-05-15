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
        if ((targetMask & (1 << collision.gameObject.layer)) != 0) //타겟에 충돌시 데미지를 주고 사라짐
        {
            if (collision.gameObject.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage(rangeWeaponHandler.GetAttackDamage());
                //Debug.Log($"{collision.gameObject.name} 에게 {rangeWeaponHandler.GetAttackDamage()} 데미지!");
            }
            ReleaseProjectile();
        }
        else if ((obstacleMask & (1 << collision.gameObject.layer)) != 0) //장애물에 충돌
        {
            if (currentRelfection == 0) //튕길 수 있는 횟수가 없으면 발사체 회수
            {
                ReleaseProjectile();
            }
            else
            {
                currentRelfection--;
                if (direction == Vector2.Reflect(direction, collision.contacts[0].normal))  //충돌처리 중 입사각과 반사각이 진각일때 물리 엔진 오류가 나는 경우가 있다. 반사각을 제대로 구하지 못함.
                {
                    direction = -direction;                                                 //진각처리 된 입사각이니 -를 붙여 반대 방향으로 바꿔주자                                            
                }
                else
                {
                    //Debug.Log($"{collision.contacts[0].normal} : {direction}");
                    direction = Vector2.Reflect(direction, collision.contacts[0].normal);   //현재 방향을 입사각으로 충돌체로부터 반사각을 구하여 튕겨나갈 방향 변경
                }
                float RotZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;          //새로운 방향으로 로테이션을 시키기 위해 아크 탄젠트로 회전각 구하기
                transform.rotation = Quaternion.Euler(0, 0, RotZ);                          //회전각 적용
            }
        }
    }
}
