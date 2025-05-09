using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float cameraHalfHeight;
    private float cameraHalfWidth;

    private Vector2 areaMin;
    private Vector2 areaMax;

    private float cameraLimitsHeight;
    private float cameraLimitsWidth;

    private Vector3 cameraOffset = new Vector3(0, 0, -10);

    private Vector3 cameraPos;

    [SerializeField] private float chaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        //target = GameMangaer 구현시 GameManager로부터 플레이어 인스턴스 호출
    }
    private void LateUpdate()
    {
        if (true) //GameManager로부터 Room에서 전투가 시작됐을때 Apply 적용
        {
            ApplyCameraBounds();
        }

        transform.position = Vector3.Lerp(transform.position, cameraPos, Time.fixedDeltaTime * chaseSpeed);
    }

    public void SetCameraBounds(TilemapRenderer tilemapRenderer)
    {
        Bounds bounds = tilemapRenderer.bounds;
        areaMin = bounds.min;
        areaMax = bounds.max;
    }

    private void ApplyCameraBounds()
    {
        cameraLimitsHeight = Mathf.Clamp(target.position.y, areaMin.y + cameraHalfHeight, areaMax.y - cameraHalfHeight);
        cameraLimitsWidth = Mathf.Clamp(target.position.x, areaMin.x + cameraHalfWidth, areaMax.x - cameraHalfWidth);

        cameraPos.x = cameraLimitsWidth;
        cameraPos.y = cameraLimitsHeight;
        cameraPos.z = cameraOffset.z;
    }
}
