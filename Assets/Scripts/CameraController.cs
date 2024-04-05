using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float zDistance;

    private void Awake()
    {
        if(target != null)
        {
            zDistance = target.position.z - transform.position.z;
        }
    }

    private void LateUpdate()
    {
        //target이 존재하지 않으면 실행 하지 않는다
        if (target == null) return;

        //카메라의 위치(Position) 정보 갱신
        Vector3 position = transform.position;
        position.z = target.position.z - zDistance;
        transform.position = position;
    }
}
