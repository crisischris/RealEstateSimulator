using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject headEyes;
    public AudioSource[] sources;
    public AudioClip openSound;
    public AudioClip closeSound;
    public float doorDistanceToHead;
    public Vector3 doorPos;
    public Vector3 playerPos;
    public Vector3 localPos;
    public int DoorCounter = 1;
    public int audioCounter = 1;
    public int rotateDirection;
    public bool doorOpensOnce;
    public bool regularDoor;

    // Use this for initialization
    void Start()
    {
        DoorCounter = 1;
        doorPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = headEyes.transform.position;
        doorDistanceToHead = Vector3.Distance(doorPos, playerPos);

        if (doorDistanceToHead <= 1.5 && DoorCounter == 1)
        {
            if (regularDoor)
            {
                StartCoroutine(openDoor());
                if (audioCounter == 1)
                {
                    openDoorAudio();
                }
            }
        }

        if (doorDistanceToHead > 1.5 && DoorCounter == 2)
        {
            if (regularDoor)
            {
                if (!doorOpensOnce)
                {
                    StartCoroutine(closeDoor());
                    if (audioCounter == 2)
                    {
                        closeDoorAudio();
                    }
                }
            }
        }
    }

    IEnumerator openDoor()
    {
        gameObject.transform.Rotate(Vector3.up, rotateDirection * Time.deltaTime);
        yield return new WaitForSeconds(1);
        DoorCounter = 2;
    }

    IEnumerator closeDoor()
    {
        gameObject.transform.Rotate(Vector3.up, -rotateDirection * Time.deltaTime);
        yield return new WaitForSeconds(1);
        DoorCounter = 1;
    }

    public void openDoorAudio()
    {
        sources[0].PlayOneShot(openSound);
        audioCounter = 2;
    }

    public void closeDoorAudio()
    {
        sources[1].PlayOneShot(closeSound);
        audioCounter = 1;
    }
}
