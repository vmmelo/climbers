using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPoint : MonoBehaviour
{
    [SerializeField] private Transform destructionPoint;

    void Start(){
        this.transform.position = this.destructionPoint.position;
    }

    void Update()
    {
        if(destructionPointMovesUp())
        {
            moveGameOverPointUp();
        }
    }

    private bool destructionPointMovesUp(){
        return this.destructionPoint.position.y > this.transform.position.y;
    }

    private void moveGameOverPointUp(){
        this.transform.position = new Vector2(0f, this.destructionPoint.position.y);
    }
}