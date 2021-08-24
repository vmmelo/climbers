using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private GameObject platformDestructionPoint;
     
    // Start is called before the first frame update
    void Start()
    {
       platformDestructionPoint = GameObject.Find("Destruction Limit");
    }

    // Update is called once per frame
    void Update()
    {
        if(platformIsLowerThanLimit())
        {
            Destroy(gameObject);
        }
    }

    private bool platformIsLowerThanLimit()
    {
        return this.transform.position.y < platformDestructionPoint.transform.position.y;
    }

}