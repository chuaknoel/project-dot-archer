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

    //������ �߻�ü ������ ������Ʈ Ǯ������ ����
    public void SetProjectile()
    {
        projectileManager = ProjectileManager.Instance;

        //�߻�ü ������ ������ �̿��Ͽ� ������Ʈ Ǯ�� ���
        projectileManager.RegisterPoolObject(projectilePrefab.name,

            new ObjectPool<Projectile>
            (
                CreateProjectile,                   //�߻�ü ������Ʈ ���� ����
                projectileManager.OnGet,            //������ ������Ʈ�� ��ȯ
                projectileManager.OnRelease,        //������ ������Ʈ ȸ��
                projectileManager.OnDes,            //������ ������Ʈ �ı�
                maxSize: projectilePrefab.MaxSize   //�ѹ��� ������ ������Ʈ ����
            ));

        connectedPool = projectileManager.FindPool(projectilePrefab.name); //Ǯ�� �Ŵ������� ��ϵ� �߻�ü Ǯ�� ã�ƿͼ� �ڵ� �Ŵ����� ����
       
        for (int i = 0; i < projectilePrefab.MaxSize; i++) //�ѹ��� ������ ������Ʈ ������ Ǯ���� ���
        {
            Projectile projectiles = CreateProjectile();       
            connectedPool.Release(projectiles);            //�̸� ����� ���� ������Ʈ�̱� ������ ȸ���Ͽ� ����
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
