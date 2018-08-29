﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{

    GameObject standing;
    GameObject crouching;
    GameObject crouchHead;
    //CapsuleCollider crouchCollider;
    GameObject player;
    float standHeight = 2.5f;
    float crouchHeight;
    float smooth = 0;
    public bool isCrouched;
    GameObject cubeCamera;
    Transform stand;
    private Vector3 currentRot, targetRot;
    private bool rotating = false;
    move movement;
    UIManager uimanager;
    GvrReticlePointer pointer;
    public GameObject Inventory;
    public Vector3 crouchPos;
     


    private void Start()
    {
        uimanager = GameObject.Find("UI_System").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        cubeCamera = player.transform.GetChild(0).gameObject;
        pointer = cubeCamera.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<GvrReticlePointer>();
        isCrouched = false;
        currentRot = transform.eulerAngles; //플레이어가 바라보는 각도
        movement = player.GetComponent<move>();

    }

    void Update()
    {
        //Fire1은 컨트롤러의 x버튼이다, 다시 일어선다
        if(Input.GetButtonUp("Fire1") && isCrouched && !Inventory.GetComponent<inventory>().inven)
        {
            if(!uimanager.defaultUI.activeSelf)
            {
                //uimanager.defaultUI.SetActive(true);
                uimanager.evacUI_1.SetActive(false);
                uimanager.evacUI_2.SetActive(false);
            }
            cubeCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.4f , player.transform.position.z);
            isCrouched = false;
            movement.disable_move = true;
            pointer.enabled = true;
            //pointer.OnPointerExit(player);
        }
    }


    //List<string> contacts = new List<string>();
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "table")
        //if(contacts.Contains("tabble") && contacts.Contains("Chair"))
        {
            //default ui를 끈다
            if (uimanager.defaultUI.activeSelf)
            {
                uimanager.defaultUI.SetActive(false);
                uimanager.evacUI_1.SetActive(false);
                uimanager.evacUI_2.SetActive(true);
            }
            //ctrl를 누르면 수그린다
            if (Input.GetButtonUp("Fire1") && !isCrouched && !Inventory.GetComponent<inventory>().inven)
            {
                uimanager.evacUI_2.SetActive(false);
                uimanager.evacUI_1.SetActive(true);

                pointer.enabled = false;

                //플레이어 못 움직이게 한다
                movement.disable_move = false;
                stand = cubeCamera.transform;
                //서있을때의 카메라 위치 저장
                stand.position = cubeCamera.transform.position;
                //플레이어의 카메라를 수그린것처럼 이동
                cubeCamera.transform.position = new Vector3(collision.gameObject.transform.position.x, player.transform.position.y - 0.3f, collision.gameObject.transform.position.z);
                crouchHeight = player.transform.position.y - 0.3f;
                crouchPos = new Vector3(collision.gameObject.transform.position.x, player.transform.position.y - 0.3f, collision.gameObject.transform.position.z);

                //회전하는 코루틴 함수 부른다
                StartCoroutine("Rotate");

            }
        }
    }

    


    //회전하는 코루틴 함수
    IEnumerator Rotate()
    {
        Quaternion startRot = cubeCamera.transform.rotation;
        float t = 0.0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            cubeCamera.transform.rotation = startRot * Quaternion.AngleAxis(t / 1.5f * 180f, transform.up); //or transform.right if you want it to be locally based
            yield return null;
        }
        isCrouched = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "table")
        {
            uimanager.defaultUI.SetActive(true);
            uimanager.evacUI_1.SetActive(false);
            uimanager.evacUI_2.SetActive(false);
        }
    }

    


}
