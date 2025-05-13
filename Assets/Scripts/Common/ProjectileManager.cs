using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//발사체를 관리하는 매니저 클래스
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

    //생성한 발사체 정보를 가지고 있는 딕셔너리. 발사체이름 - 발사체 풀로 연결
    public Dictionary<string, IObjectPool<Projectile>> projectilePools = new Dictionary<string, IObjectPool<Projectile>>();

    public Transform projectileHolder;

    //사용할 발사체 오브젝트 풀을 등록
    public void RegisterPoolObject(string poolName, IObjectPool<Projectile> projectilePrefab, int _count = 10)
    {
        if (projectilePools.ContainsKey(poolName)) return;

        projectilePools.Add(poolName, projectilePrefab);
    }

    //외부에서 발사체 풀을 사용할때 발사체 이름으로부터 해당 발사체 풀을 찾아준다.
    public IObjectPool<Projectile> FindPool(string _foolName)
    {
        if (projectilePools.TryGetValue(_foolName, out IObjectPool<Projectile> projectile))
        {
            return projectile;
        }

        return null;
    }

    //발사체 생성
    public void OnGet(Projectile _poolObj)
    {
        _poolObj.gameObject.SetActive(true);
    }

    //발사체 감추기
    public void OnRelease(Projectile _poolObj)
    {
        _poolObj.gameObject.SetActive(false);
    }

    //발사체 파괴
    public void OnDes(Projectile _poolObj)
    {
        Destroy(_poolObj.gameObject);
    }
}
