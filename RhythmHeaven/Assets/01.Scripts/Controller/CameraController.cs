using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform thePlayer = null;
    [SerializeField] float followSpeed = 15;
    float hitDistance = 0;


    Vector3 playerDistance = new Vector3();
    [SerializeField] float zoomDistance = -1.00f;

    // Start is called before the first frame update
    void Start()
    {
        playerDistance = transform.position - thePlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t_destPos = thePlayer.position + playerDistance + (hitDistance * transform.forward);
        transform.position = Vector3.Lerp(transform.position, t_destPos, Time.deltaTime * followSpeed);

        OnCusor();
    }

    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f);

        hitDistance = 0;
    }

    void OnCusor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
