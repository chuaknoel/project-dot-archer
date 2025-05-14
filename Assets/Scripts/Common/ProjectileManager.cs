using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//�߻�ü�� �����ϴ� �Ŵ��� Ŭ����
public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //������ �߻�ü ������ ������ �ִ� ��ųʸ�. �߻�ü�̸� - �߻�ü Ǯ�� ����
    public Dictionary<string, IObjectPool<Projectile>> projectilePools = new Dictionary<string, IObjectPool<Projectile>>();

    public Transform projectileHolder;

    //����� �߻�ü ������Ʈ Ǯ�� ���
    public void RegisterPoolObject(string poolName, IObjectPool<Projectile> projectilePrefab, int _count = 10)
    {
        if (projectilePools.ContainsKey(poolName)) return;

        projectilePools.Add(poolName, projectilePrefab);
    }

    //�ܺο��� �߻�ü Ǯ�� ����Ҷ� �߻�ü �̸����κ��� �ش� �߻�ü Ǯ�� ã���ش�.
    public IObjectPool<Projectile> FindPool(string _foolName)
    {
        if (projectilePools.TryGetValue(_foolName, out IObjectPool<Projectile> projectile))
        {
            return projectile;
        }

        return null;
    }

    //�߻�ü ����
    public void OnGet(Projectile _poolObj)
    {
        _poolObj.gameObject.SetActive(true);
    }

    //�߻�ü ���߱�
    public void OnRelease(Projectile _poolObj)
    {
        _poolObj.gameObject.SetActive(false);
    }

    //�߻�ü �ı�
    public void OnDes(Projectile _poolObj)
    {
        Destroy(_poolObj.gameObject);
    }
}
