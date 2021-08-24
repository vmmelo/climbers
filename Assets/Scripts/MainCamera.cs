using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{   
    [SerializeField] private GameObject boneco;
    private bool gameOverIsTriggered;

    void Start()
    {
        gameOverIsTriggered = false;
    }

    void Update()
    {
        if(!gameOverIsTriggered)
        {
            followThePlayer();
        }

        if(this.boneco.transform.position.y < this.transform.position.y - 5f)
        {
           
          Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
           
            
        }



    }

    private void followThePlayer()
    {
        this.transform.position = new Vector3(this.transform.position.x, boneco.transform.position.y, this.transform.position.z);
    }

    public void triggerGameOver()
    {
        gameOverIsTriggered = true;
         
    }
}