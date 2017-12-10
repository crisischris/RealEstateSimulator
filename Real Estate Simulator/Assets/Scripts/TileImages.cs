using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileImages : MonoBehaviour {

    public ObjectManager objectManager;

    public Material[] tileImages;
    public GameObject[] tiles;

	// Use this for initialization
	void Start () {


        if (objectManager.LargeMode2IsActive == true)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                Renderer mat = tiles[i].GetComponent<Renderer>();
                mat.material = tileImages[i];

            }
        }



    }

    // Update is called once per frame
    void Update () {


      
	}
}
