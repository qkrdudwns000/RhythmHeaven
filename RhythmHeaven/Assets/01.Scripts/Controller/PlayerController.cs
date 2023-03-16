using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();
    string move = "Move";
    bool canMove = true;

    [SerializeField] float SpinSpeed = 180f;
    Quaternion destRot = new Quaternion();

    [SerializeField] Transform fakeSlime = null;
    [SerializeField] Transform realSlime = null;

    TimingManager theTimingManager;
    CameraController theCam;
    Animator myAnim;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (canMove)
            {
                Calc();
                if (theTimingManager.CheckTiming())
                {
                    StartAction();
                }
            }
        }
    }
    void Calc()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));

        // 이동 목표값 계산
        destPos = transform.position + new Vector3(dir.x, 0, dir.z);

        // 회전 목표값 계산
        fakeSlime.rotation = Quaternion.LookRotation(dir);
        destRot = fakeSlime.rotation;
    }

    void StartAction()
    {
        myAnim.ResetTrigger(move);
        myAnim.SetTrigger(move);
        StartCoroutine(MoveCo());
        StartCoroutine(RotCo());
        StartCoroutine(theCam.ZoomCam());
    }

    IEnumerator MoveCo()
    {
        while(Vector3.SqrMagnitude(transform.position - destPos) != 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destPos;
        canMove = true;
    }

    IEnumerator RotCo()
    {
        while(Quaternion.Angle(realSlime.rotation, destRot) > 0.5f)
        {
            realSlime.rotation = Quaternion.RotateTowards(realSlime.rotation, destRot, SpinSpeed * Time.deltaTime);
            yield return null;
        }

        realSlime.rotation = destRot;
    }
}
