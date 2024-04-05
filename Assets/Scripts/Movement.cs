using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameController gameController; //IsGameStart가 true일 때 움직이기 위해

    //X축 이동
    private float moveXWidth = 1.5f; //1회 이동시 이동 거리(x축)
    private float moveTimeX = 0.1f; //1회 이동에 소요되는 시간(x축)
    private bool isXMove = false; //true:이동중,false:이동가능
    //Y축이동
    private float originY = 0.55f; //점프 및 착지하는 y축 값
    private float gravity = -9.81f; //중력
    private float moveTimeY = 0.3f; //1회 이동에 소요되는 시간(y축)
    private bool isJump = false; //true:점프중,false:점프가능
    //z축이동
    [SerializeField]
    private float moveSpeed = 20.0f; //이동속도(z축)
    //회전
    private float rotateSpeed = 300.0f; //회전속도

    private float limitY = -1.0f; //플레이어가 사망하는 y위치

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //현재 상태가 게임 시작이 아니면 Update()를 실행하지 않는다.
        if (gameController.IsGameStart == false) return;

        //z축 이동
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        //오브젝트 회전(x축)
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        //낭떠러지로 떨어지면 플레이어 사망
        if(transform.position.y < limitY)
        {
            Debug.Log("게임 오버");
        }
    }

    public void MoveToX(int x)
    {
        //현재 x축 이동 중으로 이동 불가능
        if (isXMove == true) return;

        if(x > 0 && transform.position.x < moveXWidth)
        {
            //우측이동의 경우에는 1.5f길이값 이하 위치였던 경우만 이동가능.
            StartCoroutine(OnMoveToX(x));
        }
        else if( x < 0 && transform.position.x > -moveXWidth)
        {
            //좌측이동의 경우에는 -1.5f길이값 이상 위치였던경우만 이동가능
            StartCoroutine(OnMoveToX(x));
        }
    }

    public void MoveToY()
    {
        //현재 점프 중으로 점프 불가능
        if (isJump == true) return;

        StartCoroutine(OnMoveToY());
    }

    private IEnumerator OnMoveToX(int direction)
    {
        float current = 0;
        float percent = 0;
        float start = transform.position.x;
        float end = transform.position.x + direction * moveXWidth;

        isXMove = true;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeX;

            float x = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
        }

        isXMove = false;
    }

    private IEnumerator OnMoveToY()
    {
        float current = 0;
        float percent = 0;
        float v0 = -gravity;

        isJump = true;
        rigidbody.useGravity = false;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeY;

            //시간 경과에 따라 오브젝트의 y위치를 바꿔준다
            //포물선 운동: 시작위치 + 초기속도*시간 + 중력*시간제곱
            float y = originY + (v0 * percent) + (gravity * percent * percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield return null;
        }

        isJump = false;
        rigidbody.useGravity = true;
    }
}
