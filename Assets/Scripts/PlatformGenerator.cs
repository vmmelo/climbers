using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    //the platform generator follows the camera, which follows the vertical ascension of our player
    [SerializeField] private Transform camera;

    //the easiest platform to jump to
    [SerializeField] private GameObject easyPlatform;

    //medium platform dificulty
    [SerializeField] private GameObject mediumPlatform;

    //hard platform dificulty
    [SerializeField] private GameObject hardPlatform;

    //the point in new platform generation stops
    [SerializeField] private Transform generationLimit;

    //the minimum distance between each platform in the horizontal position
    [SerializeField] private float distancePlatformsX;

    //the minimum distance between each platform in the vertical position
    [SerializeField] private float minDistancePlatformsY;

    //the maximum distance between each platform in the vertical position
    [SerializeField] private float maxDistancePlatformsY;

    //the level the player is at
    [SerializeField] private int level;

    //"distance" between levels
    [SerializeField] private int levelDistance;

    private float newX;
    private float newY;
    private float distancePlatformsY;
    private int platformCount = 0;

    // Update is called once per frame
    void Update()
    {
        if(generatorBelowGenerationLimit())
        {
            generateNewPlatform();
        }
        setLevelUpConditions();
    }

    private void setLevelUpConditions()
    {
        if(this.platformCount == levelDistance)
        {
            this.platformCount = 0;
            this.level++;
        }
    }

    private GameObject randomGenerator(int easy, int medium, int hard)
    {
        GameObject tempPlatform = easyPlatform;

        int rand = Random.Range(1, 7);
        if (rand <= easy)
        {
            tempPlatform = easyPlatform;
        }
        else if (rand <= medium + easy)
        {
            tempPlatform = mediumPlatform;
        }
        else if (rand <= hard + easy + medium)
        {
            tempPlatform = hardPlatform;
        }

        return tempPlatform;
    }

    private void generateNewPlatform()
    {
        platformCount++;
        GameObject generatedPlatform = easyPlatform;

        if (this.level == 1)
        {
            generatedPlatform = randomGenerator(6, 0, 0);
        }
        else if (this.level == 2)
        {
            generatedPlatform = randomGenerator(4, 2, 0);
        }
        else if (this.level == 3)
        {
            generatedPlatform = randomGenerator(3, 2, 1);
        }
        else if(this.level == 4)
        {
            updateVerticalDistances(2.5f, 3f);
            generatedPlatform = randomGenerator(3, 1, 2);
        }
        else if(this.level == 5)
        {
            generatedPlatform = randomGenerator(2, 2, 2);
        }
        else if(this.level == 6)
        {
            generatedPlatform = randomGenerator(1, 3, 2);
        }
        else if(this.level == 7)
        {
            updateVerticalDistances(3f, 3.5f);
            generatedPlatform = randomGenerator(1, 2, 3);
        }
        else if(this.level == 8)
        {
            generatedPlatform = randomGenerator(0, 3, 3);
        }
        else if(this.level == 9)
        {
            generatedPlatform = randomGenerator(0, 2, 4);
        }
        else if(this.level == 10)
        {
             generatedPlatform = randomGenerator(0, 0 , 6);
        }

        settingHorizontalPositionOfPlatform();

        distancePlatformsY = Random.Range(minDistancePlatformsY, maxDistancePlatformsY);
        newY = transform.position.y + distancePlatformsY;
        this.transform.position = new Vector3(newX, newY, transform.position.z);
        Instantiate(generatedPlatform, transform.position, transform.rotation);
    }

    private void updateVerticalDistances(float minY, float maxY)
    {
        minDistancePlatformsY = minY;
        maxDistancePlatformsY = maxY;
    }

    private bool generatorBelowGenerationLimit()
    {
        return transform.position.y < generationLimit.position.y;
    }

    private void settingHorizontalPositionOfPlatform()
    {
        //randomizing the x position of the new platform
        int randPos = Random.Range(0, 2);
        Debug.Log(randPos);

        switch (randPos)
        {
            case 0:
                newX = this.transform.position.x + distancePlatformsX;
                break;
            case 1:
                newX = this.transform.position.x - distancePlatformsX;
                break;
        }

        if (newX < -4.44f)
        {
            newX = 4.44f;
        }

        if (newX > 4.44f)
        {
            newX = -4.44f;
        }
    }
}