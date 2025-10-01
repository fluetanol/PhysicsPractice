using UnityEngine;

public interface ICameraMovement
{
    void SetTarget(Transform target);
    void LookTarget();
    void CameraFollow(Vector3 vec, float length, float degree);
    void CameraCollision(float length);

    void CameraOffset(Vector3 offset);

}