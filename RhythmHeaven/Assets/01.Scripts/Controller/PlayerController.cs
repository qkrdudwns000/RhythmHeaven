using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isCanPressKey = true;

    [SerializeField] float moveSpeed = 3f;
    Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();
    Vector3 tempY = new Vector3(0, 0.5f, 0); // Ray������� ���� y��
    Vector3 orgPos = new Vector3(); // �߶����� ��� ���� ��ġ�� ���ƿ��� ���� ��ġ��.

    string move = "Move";
    bool canMove = true;
    bool isFalling = false; // �߶� ���� ��

    [SerializeField] float SpinSpeed = 180f;
    Quaternion destRot = new Quaternion();

    [SerializeField] Transform fakeSlime = null;
    [SerializeField] Transform realSlime = null;

    TimingManager theTimingManager;
    CameraController theCam;
    StatusManager theStatusManager;
    //AudioManager theAudioManager = AudioManager.inst;
    Animator myAnim;
    Rigidbody myRigid;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theStatusManager = FindObjectOfType<StatusManager>();
        theCam = FindObjectOfType<CameraController>();
        myRigid = GetComponentInChildren<Rigidbody>();
        orgPos = transform.position;

        canMove = true;
        isFalling = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(GameManager.inst.isStartGame)
        {
            FallingCheck();

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (canMove && isCanPressKey && !isFalling)
                {
                    orgPos = destPos;
                    Calc();
                    if (theTimingManager.CheckTiming())
                    {
                        StartAction();
                    }
                }
            }
        }
    }
    void Calc()
    {
        // ���� ���
        dir.Set(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));

        // �̵� ��ǥ�� ���
        destPos = transform.position + new Vector3(dir.x, 0, dir.z);

        // ȸ�� ��ǥ�� ���
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

    void FallingCheck()
    {
        if (!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position + tempY, Vector3.down, 1.1f))
            {
                Falling();
            }
        }
    }

    void Falling()
    {
        isFalling = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
    }

    public void ResetFalling()
    {
        theStatusManager.DecreaseHp(25);
        AudioManager.inst.PlaySFX("Falling");

        if (!theStatusManager.IsDead()) // ���� �ʾ��� ��쿡�� ����.
        {
            isFalling = false;
            myRigid.useGravity = false;
            myRigid.isKinematic = true;

            transform.position = orgPos;
            destPos = orgPos;
            realSlime.localPosition = new Vector3(0, 0, 0);
        }
    }
}
