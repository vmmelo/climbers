﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+0.01f);
    }
}
