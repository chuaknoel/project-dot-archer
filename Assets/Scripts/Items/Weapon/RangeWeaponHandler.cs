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

        connectedPool = projectileManager.FindPool(projectilePrefab.name);   //Ǯ�� �Ŵ������� ��ϵ� �߻�ü Ǯ�� ã�ƿͼ� �ڵ� �Ŵ����� ����
       
        for (int i = 0; i < projectilePrefab.MaxSize; i++)                   //�ѹ��� ������ ������Ʈ ������ Ǯ���� ���
        {
            Projectile projectiles = CreateProjectile();       
            connectedPool.Release(projectiles);                               //�̸� ����� ���� ������Ʈ�̱� ������ ȸ���Ͽ� ����
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
            if (count % 2 == 0 && i == -halfCount)    //�߻�ü�� ¦�� �϶� ù��° �߻�ü�� �������� ����
            {                                         //�߻�ü ������ 4���� 5���϶� �Ѵ� haltCount�� 2�� ����. for���� ������ -2~2�� ������
                continue;                             //�׷��� ¦�� �߻��϶��� ù���� ���������Ͽ� �߻�ü ������ �����ش�. �� ù���� �ƴϿ��� �������.
            }

            spread = Random.Range(0, 0.5f);           //�߻�ü�� ���� Ȯ�갪�� �־� ���� ���� ��¦�� ������ ���� �ش�.

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
