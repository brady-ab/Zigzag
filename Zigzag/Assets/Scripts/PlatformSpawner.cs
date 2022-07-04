using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject Diamond;

    Vector3 lastPos;
    float size;
    public bool GameOver;



    // Start is called before the first frame update
    void Start()
    {
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;

        for (int i = 0; i < 20; i++)
        {
            SpawnPlatforms();
        }

        InvokeRepeating("SpawnPlatforms", 2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            CancelInvoke("SpawnPlatforms");
        }
    

    
    }

    //Uses a random number to spawn a platform in either the X or Z direction.
    void SpawnPlatforms()
    {

        int rand = Random.Range(0, 6);
        if (rand < 3)
        {
            SpawnX();
        }
        else if(rand >= 3)
        {
            SpawnZ();
        }
    }

    //Spawns a platform one platform's length ahead of the last platform in the X direction.
    void SpawnX()
    {
        Vector3 pos = lastPos;
        pos.x += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);

        int rand = Random.Range(0, 4);
        if (rand < 1) {
            Instantiate(Diamond, new Vector3(pos.x, pos.y + 1, pos.z), Diamond.transform.rotation);
        }
        else
        {
            //
        }
    }

    
    //Same but Z
    void SpawnZ()
    {
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);

        int rand = Random.Range(0, 10);
        if (rand < 1)
        {
            Instantiate(Diamond, new Vector3(pos.x, pos.y + 1, pos.z), Diamond.transform.rotation);
        }
        else
        {
            //
        }
    }
}
