using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;


public enum ECameraType
{
    DefaultMovement,
    AnotherMovement
}

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    private ICameraMovement _cameraMovement;
    private Dictionary<ECameraType, ICameraMovement> d_CameraMovementPool;

    [SerializeField] private ECameraType _cameraType;
    public ECameraType CameraType
    {
        get
        {
            return _cameraType;
        }
        set
        {
            _cameraType = value;
            _cameraMovement = d_CameraMovementPool[value];
        }
    }

    [SerializeField] private Transform _targettransform;
    [SerializeField] private Vector3 _dirvec;
    [SerializeField] private Vector3 _offsetvec;
    [SerializeField] private float _degree;
    [SerializeField] private float _length;
    [SerializeField] private bool _isLookAt;
    [SerializeField] private bool _isFollowAt;
    [SerializeField] private bool _isCollisionAt;

    void Awake()
    {
        _cameraMovement = new DefaultCameraMovement(_targettransform);
        print("초기화");
    }

    void OnEnable()
    {
        if (_cameraMovement == null)
            _cameraMovement = new DefaultCameraMovement(_targettransform);

    }

    void Start()
    {
        print("ㅇ?");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //print(_targettransform + " " + _cameraMovement);
        _cameraMovement.SetTarget(_targettransform);
        if (_isLookAt)
        {
            _cameraMovement.LookTarget();
        }
        if (_isFollowAt)
        {
            _cameraMovement.CameraFollow(_dirvec.normalized, _length, _degree);
        }
        if (_isCollisionAt)
        {
            _cameraMovement.CameraCollision(_length);
        }

        _cameraMovement.CameraOffset(_offsetvec);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawLine(_targettransform.position, _targettransform.position + _dirvec.normalized * _length);
    }


    public void debugEVT()
    {
        print("신상혁");
    }
}
