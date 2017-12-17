using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public ObjectManager objectManager;
    private GameObject eyes;

    public float doorDistanceToHead;

    public Vector3 doorPos;
    public Vector3 playerPos;
    public Vector3 localPos;

    public int DoorCounter = 1;


    // Use this for initialization
    void Start()
    {

        DoorCounter = 1;
        eyes = objectManager.eyes;
        doorPos = transform.position;




    }

    // Update is called once per frame
    void Update()
    {

        playerPos = eyes.transform.position;
        doorDistanceToHead = Vector3.Distance(doorPos, playerPos);


        if (doorDistanceToHead <= 3 && DoorCounter == 1)
        {
            StartCoroutine(openDoor());
            StartCoroutine(openDoorAudio());
        }

        if (doorDistanceToHead > 3 && DoorCounter == 2)
        {
            StartCoroutine(closeDoor());
            StartCoroutine(closeDoorAudio());
        }
    }

    IEnumerator openDoor()
    {
        objectManager.sources[1].Play();
        gameObject.transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        yield return new WaitForSeconds(1);
        DoorCounter = 2;
    }

    IEnumerator closeDoor()
    {
        objectManager.sources[2].Play();
        gameObject.transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        yield return new WaitForSeconds(1);
        DoorCounter = 1;
    }


    IEnumerator closeDoorAudio()
    {
        AudioSource thisClip = objectManager.sources[1];
        objectManager.sources[1].Play();
        yield return new WaitForSeconds(thisClip.clip.length);
    }

    IEnumerator openDoorAudio()
    {
        AudioSource thisClip = objectManager.sources[2];
        objectManager.sources[2].Play();
        yield return new WaitForSeconds(thisClip.clip.length);
    }

}
