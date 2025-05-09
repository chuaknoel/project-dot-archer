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
    private Vector3 cameraVelocity = Vector3.zero;

    [SerializeField] private float chaseSpeed;

    private bool isApply;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        cameraPos.z = cameraOffset.z;
        //target = GameMangaer 구현시 GameManager로부터 플레이어 인스턴스 호출
    }
    private void LateUpdate()
    {
        CameraMove();
    }

    public void CameraMove()
    {
        ApplyCameraBounds();

        //transform.position = Vector3.Lerp(transform.position, cameraPos, chaseSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref cameraVelocity, chaseSpeed);
    }

    public void SetCameraBounds(TilemapRenderer tilemapRenderer)
    {
        Bounds bounds = tilemapRenderer.bounds;
        areaMin = bounds.min;
        areaMax = bounds.max;
    }

    private void ApplyCameraBounds()
    {
        if (isApply)
        {
            cameraLimitsHeight = Mathf.Clamp(target.position.y, areaMin.y + cameraHalfHeight, areaMax.y - cameraHalfHeight);
            cameraLimitsWidth = Mathf.Clamp(target.position.x, areaMin.x + cameraHalfWidth, areaMax.x - cameraHalfWidth);

            cameraPos.x = cameraLimitsWidth;
            cameraPos.y = cameraLimitsHeight;
        }
        else
        {
            cameraPos.x = target.position.x;
            cameraPos.y = target.position.y;
        }
    }
}
