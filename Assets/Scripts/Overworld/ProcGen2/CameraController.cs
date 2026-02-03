using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Smoothing")]
    public float smoothOffset = 2f;
    public float smoothSpeed = 8f;

    [Header("Overview Mode")]
    public Vector3 overviewPosition = new Vector3(0f, 0f, -20f);
    public float overviewSize = 10f;
    
    [Header("Follow Mode")]
    public Transform player;
    public Vector3 followPosition = new Vector3(0, 0, -10f);
    public float followSize = 5f;
    public float followSpeed = 5f;
    
    private Camera cam;
    private Vector3 currentPosition;
    private bool isOverviewMode = true;
    private bool isReturningToOverview = false;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        
        SetCameraMode(isOverviewMode);
    }
    
    void Update()
    {
        // switch modes when C is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            isOverviewMode = !isOverviewMode;
            SetCameraMode(isOverviewMode);
        }
        
        // different camera movement depending on the active mode
        if (isOverviewMode && isReturningToOverview)
        {
            ReturnToOverview();
        }
        else if (!isOverviewMode && player != null)
        {
            FollowPlayer();
        }

        // camera smoothing, follow mouse around a little so the camera isn't static (improves game feel)
        CameraSmoothing();
    }
    
    void SetCameraMode(bool mode)
    {
        if (mode)
        {
            cam.orthographicSize = overviewSize;
            isReturningToOverview = true;
            currentPosition = overviewPosition;
        }
        else
        {
            cam.orthographicSize = followSize;
            isReturningToOverview = false;
            currentPosition = followPosition;
        }
    }

    void ReturnToOverview()
    {
        transform.position = Vector3.Lerp(transform.position, overviewPosition, followSpeed * Time.deltaTime);
        
        // stop lerping once camera is close enough
        if (Vector3.Distance(transform.position, overviewPosition) < 0.01f)
        {
            transform.position = overviewPosition;
            isReturningToOverview = false;
        }
    }
    
    void FollowPlayer()
    {
        Vector3 targetPosition = player.position + followPosition;
        
        // smooth camera movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void CameraSmoothing()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(cam.transform.position.z);

        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);

        Vector3 dir = mouseWorld - currentPosition;

        // Normalize so it doesn't scale too much
        Vector3 offset = Vector3.ClampMagnitude(dir, smoothOffset);

        Vector3 targetPos = currentPosition + offset;

        // keep camera z unchanged
        targetPos.z = transform.position.z;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}