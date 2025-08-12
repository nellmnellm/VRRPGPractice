using UnityEngine;
using Unity.XR.CoreUtils; // XROrigin

public class XRFollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset = new Vector3(0f, 1.6f, -2.0f); // 타겟 기준 카메라 위치

    private XROrigin _origin;
    private Camera _xrCamera;

    void Awake()
    {
        _origin = GetComponent<XROrigin>();
        _xrCamera = _origin.Camera;
    }

    void LateUpdate()
    {
        if (!target || !_origin || !_xrCamera) return;

        // 타겟 기준 오프셋의 "월드 좌표"로 카메라를 위치시키고 싶다.
        Vector3 desiredCamWorldPos = target.TransformPoint(cameraOffset);

        // HMD 트래킹을 망치지 않으면서, 카메라의 세계 좌표를 지정 위치로 맞춤
        _origin.MoveCameraToWorldLocation(desiredCamWorldPos);

        // (선택) 타겟을 바라보도록 리그의 전/상 방향을 정렬하고 싶다면 아래 라인 추가
        // _origin.MatchOriginUpCameraForward(target.up, target.forward);
    }
}