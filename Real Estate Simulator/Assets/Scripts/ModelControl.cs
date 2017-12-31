using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ModelControl : MonoBehaviour
{
    public ObjectManager objectManager;
    public HandController handController1;
    public HandController handController2;
    public GameObject eyes;
    public int rotateSpeed = 10;
    public float vectorToLock = 0;
    public float speed = 1;
    float step;
    public float distanceFromStartPos;
    public float distanceToStartPosY;
    public Vector3 startPos;
    public Vector3 startPosHigh;
    public Vector3 currentPos;
    public Vector3 currentPosInParent;
    public string scene;
    public LineRenderer lr;
    public Rigidbody rb;
    public bool beingMoved = false;
    public float modelDistanceToHead;

    // Use this for initialization
    void Start()
    {
        startPos = gameObject.transform.position;
        startPosHigh = new Vector3(startPos.x, startPos.y + 5, startPos.z);
        step = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = gameObject.transform.position;
        distanceToStartPosY = 1 + startPos.y;
        startPosHigh = new Vector3(startPos.x, distanceFromStartPos, startPos.y);
        step = speed * Time.deltaTime;

        if (objectManager.readyToWarp == true)
        {
            warp();
        }

        if (handController1.isGrabbingModel == true || handController2.isGrabbingModel == true)
        {
            return;
        }
        else
        {
            if (currentPos != startPos)
            {
                beingMoved = true;

                if (currentPos.y < startPos.y)
                {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, startPosHigh, step);
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, startPos, step);
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (currentPos.y == startPos.y)
            {
                beingMoved = false;
            }
        }

        distanceFromStartPos = Vector3.Distance(currentPos, startPos);

        //rotate model
        gameObject.transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed));
    }

    public void warp()
    {
        SteamVR_LoadLevel.Begin(scene, false, 2);
        gameObject.SetActive(false);
        handController1.hapticTrigger();
        handController2.hapticTrigger();
    }
}
