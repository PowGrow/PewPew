using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Following;
    [SerializeField]
    private float RotationAngleX;
    [SerializeField]
    private int Distance;
    [SerializeField]
    private float OffsetY;

    private void LateUpdate()
    {
        if (Following == null)
            return;

        var rotation = Quaternion.Euler(RotationAngleX, 0, 0);
        var position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
        transform.rotation = rotation;
        transform.position = position;
    }

    public void Follow(GameObject following)
    {
        Following = following.transform;
    }

    private Vector3 FollowingPointPosition()
    {
        Vector3 followingPosition = Following.position;
        followingPosition.y += OffsetY;
        return followingPosition;
    }
}
