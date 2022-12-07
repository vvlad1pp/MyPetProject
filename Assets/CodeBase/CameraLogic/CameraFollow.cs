using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform following;
        [SerializeField] private float rotationAngleX;
        [SerializeField] private int  angleZ,angleY,distanceZ;
        public float offsetY;

        private void LateUpdate()
        {
            if (following == null)
                return;

            Quaternion rotation = Quaternion.Euler(rotationAngleX, angleY, angleZ);

            Vector3 position = rotation * new Vector3(0, 0, -distanceZ) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject follow) => following = follow.transform;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
    }
}