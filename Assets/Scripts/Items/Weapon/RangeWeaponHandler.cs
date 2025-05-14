using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class RangeWeaponHandler : WeaponHandler
{
    private ProjectileManager projectileManager;
    public IObjectPool<Projectile> connectedPool;

    [SerializeField] private Projectile projectilePrefab;
    public Transform projectilePivot;

    [SerializeField] private int projectileCount =1;
    [SerializeField] private int burstCount;
    [SerializeField] private float burstDelay;

    [SerializeField] private float spread;
    [SerializeField] private float angle; 

    public InGameUpgradeData UpgradeData { get { return upgradeData; } }
    private InGameUpgradeData upgradeData= new();

    public override void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask, Collider2D ownerColler)
    {
        base.Init(weapon, ownerStat, targetMask, ownerColler);
        SetProjectile();
    }

    //무기의 발사체 정보를 오브젝트 풀링으로 관리
    public void SetProjectile()
    {
        projectileManager = ProjectileManager.Instance;

        //발사체 프리팹 정보를 이용하여 오브젝트 풀에 등록
        projectileManager.RegisterPoolObject(projectilePrefab.name,

            new ObjectPool<Projectile>
            (
                CreateProjectile,                   //발사체 오브젝트 생성 로직
                projectileManager.OnGet,            //생성된 오브젝트를 소환
                projectileManager.OnRelease,        //생성된 오브젝트 회수
                projectileManager.OnDes,            //생성된 오브젝트 파괴
                maxSize: projectilePrefab.MaxSize   //한번에 관리될 오브젝트 갯수
            ));

        connectedPool = projectileManager.FindPool(projectilePrefab.name);   //풀링 매니저에서 등록된 발사체 풀을 찾아와서 핸들 매니저에 연결
       
        for (int i = 0; i < projectilePrefab.MaxSize; i++)                   //한번에 관리될 오브젝트 개수를 풀링에 등록
        {
            Projectile projectiles = CreateProjectile();       
            connectedPool.Release(projectiles);                               //미리 만들어 놓은 오브젝트이기 때문에 회수하여 저장
        }
    }

    public Projectile CreateProjectile()
    {
        Projectile projectilePool = Instantiate(projectilePrefab,projectileManager.projectileHolder);
        projectilePool.Init();
        return projectilePool;
    }

    public override void Rotate(Vector3 angle)
    {
        base.Rotate(angle);
        transform.rotation = Quaternion.Euler(angle);
    }

    public override float GetAttackDamage()
    {
     //   return base.GetAttackDamage() + UpgradeData.addWeaponDamage
             return base.GetAttackDamage();
    }

    public int GetProjectileCount()
    {
        return projectileCount + upgradeData.addProjectileCount;
    }

    public int GetBurstCount()
    {
        return burstCount + upgradeData.addBurstCount;
    }

    public override void AttackAction()
    {
        base.AttackAction();
        MultiProjectile();
        BurstProjectile();
    }

    public void MultiProjectile()
    {
        int count = GetProjectileCount();
        int halfCount = count / 2;
        float angle = 0;

        for (int i = -halfCount; i <= halfCount; i++)
        {
            if (count % 2 == 0 && i == -halfCount)    //발사체가 짝수 일때 첫번째 발사체는 생성에서 제외
            {                                         //발사체 개수가 4개와 5개일때 둘다 haltCount는 2로 고정. for문의 범위가 -2~2로 같아짐
                continue;                             //그래서 짝수 발사일때는 첫발을 생성제외하여 발사체 갯수를 맞춰준다. 꼭 첫발이 아니여도 상관없다.
            }

            spread = Random.Range(0, 0.5f);           //발사체에 랜덤 확산값을 주어 공격 마다 살짝씩 랜덤한 값을 준다.

            angle = (i * this.angle) + spread;

            connectedPool.Get().SetProjectile(this, projectilePivot, Quaternion.Euler(0, 0, angle), targetMask, ownerCollider);
        }
    }

    public void BurstProjectile()
    {
        StartCoroutine(eBurstProjectile());
    }

    private IEnumerator eBurstProjectile()
    {
        yield return new WaitForSeconds(burstDelay); ;

        for (int i = 0; i < burstCount; i++)
        {
            MultiProjectile();
            yield return new WaitForSeconds(burstDelay);
        }
    }

    public void ApplyUpgrade(InGameUpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;
    }

    public override float GetWeaponDelay()
    {
        return weapon.ItemData.attackDelay - UpgradeData.addAttackDelay;
    }

    public override float GetWeaponCooldown()
    {
        return weapon.ItemData.attackCooldown - UpgradeData.addAttackCooldown;
    }

    public Item GetWeapon()
    {
        return weapon;
    }
}
