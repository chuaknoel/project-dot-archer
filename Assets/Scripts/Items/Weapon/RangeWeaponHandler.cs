using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeWeaponHandler : WeaponHandler
{
    private ProjectileManager projectileManager;
    public IObjectPool<Projectile> connectedPool;

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectilePivot;

    public InGameUpgradeData UpgradeData { get { return upgradeData; } }
    private InGameUpgradeData upgradeData;

    public override void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask)
    {
        base.Init(weapon, ownerStat, targetMask);
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

        connectedPool = projectileManager.FindPool(projectilePrefab.name); //풀링 매니저에서 등록된 발사체 풀을 찾아와서 핸들 매니저에 연결
       
        for (int i = 0; i < projectilePrefab.MaxSize; i++) //한번에 관리될 오브젝트 개수를 풀링에 등록
        {
            Projectile projectiles = CreateProjectile();       
            connectedPool.Release(projectiles);            //미리 만들어 놓은 오브젝트이기 때문에 회수하여 저장
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

    public override void AttackAction()
    {
        base.AttackAction();
        connectedPool.Get().SetProjectile(this, projectilePivot , targetMask);
    }

    public void ApplyUpgrade(InGameUpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;
    }
}
