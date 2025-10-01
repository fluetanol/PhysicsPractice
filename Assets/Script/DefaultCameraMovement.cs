using System;
using UnityEngine;

public class DefaultCameraMovement : ICameraMovement
{
    private Transform _target;

    public DefaultCameraMovement(Transform target)
    {
        this._target = target;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LookTarget()
    {

        Camera.main.transform.LookAt(_target);
    }

    public void CameraFollow(Vector3 dir, float length, float degree)
    {
        float rad = degree * Mathf.Deg2Rad;
        float sin = (float)Math.Sin(rad);
        float cos = (float)Math.Cos(rad);

        float newx = dir.x * cos + sin * dir.z;
        float newz = -dir.x * sin + cos * dir.z;

        Vector3 rotateVec = new Vector3(newx, dir.y, newz).normalized;

        Camera.main.transform.position = _target.transform.position + rotateVec * length;
    }

    public void CameraCollision(float length)
    {
        if (Physics.Raycast(new Ray(_target.transform.position, Camera.main.transform.position - _target.transform.position),
            out RaycastHit hit, length))
        {
            Vector3 hitpoint = hit.point;
            Camera.main.transform.position = hitpoint;
        }
    }

    public void CameraOffset(Vector3 offset)
    {
        Camera.main.transform.position += offset;
    }
}

