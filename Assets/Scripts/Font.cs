using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Font : MonoBehaviour {

    private int currentPower_;
    private float timeLeft_;
    private bool wait_;
    public int entitiesMargin_ = 0;
    GameManagerComp gameManager_;
    HealthScore playerLogic_;
    public GameObject lightRay_;
    public Material slimeM_;
    public Material flyM_;
    public Material robotM_;

    bool playerInsideMe = false;

    bool alreadyInteracted = false;

    //Colors


    void FirstCheck()
    {
        currentPower_ = CheckMostCommon();
        if (currentPower_ == 1)
            addColor(slimeM_);
        else if (currentPower_ == 2)
            addColor(robotM_);
        else if (currentPower_ == 3)
            addColor(flyM_);
    }

    void Start()
    {
        gameManager_ = GameObject.FindObjectOfType<GameManagerComp>();
        playerLogic_ = GameObject.FindObjectOfType<HealthScore>();

        if (entitiesMargin_ == 0)
            entitiesMargin_ = 2;

        Invoke("FirstCheck", 5);

        //Cooldown 
        timeLeft_ = 0.0f;
        wait_ = false;
    }

    void Update()
    {
        if (!wait_)
        {
            timeLeft_ -= Time.deltaTime;
            if (timeLeft_ < 0)
            {
                lightRay_.SetActive(true);
                wait_ = true;
                timeLeft_ = 20.0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && wait_ && playerInsideMe)
        {
            //Gives the power to the player
            NextPower();
            wait_ = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ActivateTextPush>().hideText();

            playerInsideMe = false;

        }
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<GeneratedObject>())
		{
			Destroy(other.gameObject);
		}

        //Habilidades
        if (other.gameObject.CompareTag("Player"))
        {
            if (!alreadyInteracted)
            {
                FindObjectOfType<ActivateTextPush>().showText('F');

                playerInsideMe = true;
            }
        }
	}

    //void OnTriggerStay (Collider other)
    //{
    //    //Habilidades
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (wait_)
    //        {
    //            //Gives the power to the player
    //            NextPower();
    //            wait_ = false;
    //        }
    //    }
    //}
    int CheckMostCommon()
    {
        int mostCommon = currentPower_;
        int fly = 0, slime = 0, robot = 0;

        fly = gameManager_.flyEnemies_.Count;
        slime = gameManager_.slimeEnemies_.Count;
        robot = gameManager_.robotEnemies_.Count;

        int x = max(fly, slime);
        if (max(x, robot) == x)
            if ((x - robot) >= entitiesMargin_)
                if (x == fly) mostCommon = 3;
                else mostCommon = 1;
            else;

        else if ((robot - x) >= entitiesMargin_)
            mostCommon = 2;

        return mostCommon;
    }

    public void NextPower()
    {
        lightRay_.SetActive(false);
        currentPower_ = CheckMostCommon();

        playerLogic_.SetPowerTrue(currentPower_, true);
        wait_ = false;

        if (currentPower_ == 1)
            addColor(slimeM_);
        else if (currentPower_ == 2)
            addColor(robotM_);
        else if (currentPower_ == 3)
            addColor(flyM_);
    }
    

    public void addColor(Material mat)
    {
        lightRay_.GetComponent<Renderer>().material = mat;
    }
    //Auxiliar
    int max(int first, int second)
    {
        if (first > second)
            return first;
        else
            return second;
    }//Returns the max between 2 integers,if both are the same, it returns the second one

}