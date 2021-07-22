using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;

public class boneco : MonoBehaviour

{
    private Rigidbody2D body;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();

    }

    void OnMessage(int fromDeviceID, JToken data)
    {
        Debug.Log("message from" + fromDeviceID + ", data: " + data);
        if (data["force"] != null && data["x"] != null && data["y"] != null)
        {
            float force = float.Parse(data["force"].ToString());
            float x = float.Parse(data["x"].ToString());
            float y = float.Parse(data["y"].ToString());
            Pular(x, y, force*0.1F);
            AirConsole.instance.Broadcast("MUDAR");
            Debug.Log("MUDAR");
        }
    }

    

    void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }

    private void Pular(float x, float y, float force)
    {
        Vector2 direcao = new Vector2(x * force*-1, y*force);
        body.AddForce(direcao, ForceMode2D.Impulse);
    }
}
