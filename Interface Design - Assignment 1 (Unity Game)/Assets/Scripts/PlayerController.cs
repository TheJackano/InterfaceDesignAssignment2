﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public Camera camera;
    public NavMeshAgent agent;
    public AudioSource StepsSound;

    public Text StepsTakenTextField;
    public static float StepsTaken;

    private bool hasFirstClicked = false;

    Animator myAnim;
    float dist;
    RaycastHit hit;

    Quaternion newRotation;
    float rotSpeed = 5f;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            hasFirstClicked = true;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                myAnim.SetBool("isRunning", true);
            }
        }
        Vector3 relativePos = hit.point - transform.position;
        newRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);

        if(hasFirstClicked) dist = Vector3.Distance(hit.point, transform.position);

        if (dist < 1.1f)
        {
            myAnim.SetBool("isRunning", false);
            agent.isStopped = true;
            StepsSound.Pause();
        }
        else
        {
            agent.isStopped = false;
            StepsTaken += 4f * Time.deltaTime;
            StepsTakenTextField.text = ((int)StepsTaken).ToString();
            StepsSound.UnPause();
        }
    }
}
