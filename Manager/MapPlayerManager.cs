using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class MapPlayerManager : MonoBehaviour
{
    public GameObject MapPlayer;
    private Transform PlayerCameraTransform;
    private Camera PlayerCamera;
    private Vector3 MoveForward;
    private Rigidbody PlayerRd;
    private float RunSpeed = 5.0f,
                  XRotateSpeed = 300.0f,
                  YRotateSpeed = 240.0f,
                  MinYAngle = -30.0f,
                  MaxYAngle = 60.0f,
                  cameraDistance = 3.0f,
                  RotetionX = 0.0f,
                  RotetionY = 0.0f;
    private Ray ray;

    public MapData MD;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCameraTransform = MapPlayer.transform.Find("MainCamera");
        PlayerCamera = PlayerCameraTransform.GetComponent<Camera>();
        PlayerRd = MapPlayer.GetComponent<Rigidbody>();

        Vector3 angles = PlayerCameraTransform.eulerAngles;
        RotetionX = angles.x;
        RotetionY = angles.y;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        PlayerMovement();
        LookObject();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * XRotateSpeed * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * YRotateSpeed * Time.deltaTime;

        RotetionX -= MouseY;
        RotetionY += mouseX;
        RotetionX = Mathf.Clamp(RotetionX, MinYAngle, MaxYAngle);

        Quaternion rotetion = Quaternion.Euler(RotetionX, RotetionY, 0);
        Vector3 offset = rotetion * new Vector3(0, 0, -cameraDistance);
        PlayerCameraTransform.position = MapPlayer.transform.position + offset;
        PlayerCameraTransform.rotation = rotetion;

        float cameraYAngle = PlayerCamera.transform.eulerAngles.y;
        MapPlayer.transform.rotation = Quaternion.Euler(0, cameraYAngle, 0);
    }

    public void PlayerMovement()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        MoveForward = MapPlayer.transform.forward * dy + MapPlayer.transform.right * dx;
        PlayerRd.velocity = RunSpeed * MoveForward.normalized;
    }

    public void LookObject()
    {
        ray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50.0f, 1 << 6))
        {
            Debug.Log($"RayHitObject：{hit.collider.gameObject.name}");
            MD.LoocObj = hit.collider.gameObject;
        }
        else
        {
            MD.LoocObj = null;
        }
    }
}
