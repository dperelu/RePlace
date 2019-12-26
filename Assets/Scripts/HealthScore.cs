using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScore : MonoBehaviour {

    public bool dash_, shield_, lifeSteal_;
    public float timeLeft_;
    int health_;
    int currentPower_;
    public bool fountainPower_;
    public Slider healthBar;
    public Gun gun_;
	public ParticleSystem ShieldObject;
    public Image ability;

    // Use this for initialization
    void Start()
    {
        health_ = 100;

        dash_ = false;
        shield_ = false;
        lifeSteal_ = false;
        fountainPower_ = false;


        healthBar.value = health_;
        
        int rnd = Random.Range(1,4);
        currentPower_ = rnd;
        SetPowerTrue(currentPower_, false);

        //Cooldown
        timeLeft_ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //Update health & Score on Hud
        healthBar.value = health_;


        //Powers

        timeLeft_ -= Time.deltaTime;
        if (fountainPower_)
        {
            if (Input.GetKeyDown("f"))
            {
                fountainPower_ = false;
                timeLeft_ = 0.0f;
            }

            if (timeLeft_ < 0)
            {
                fountainPower_ = false;
                timeLeft_ = 10.0f;
            }
        }
        else
        {
            if (timeLeft_ < 0)
            {
                currentPower_ = (currentPower_ + 1) % 4;
                if (currentPower_ == 0)
                    currentPower_ = 1;
                SetPowerTrue(currentPower_, false);
                timeLeft_ = 10.0f;
            }
        }

        if (dash_)
        {
            GetComponent<Dash>().setAvailable(true);
			ShieldObject.GetComponent<Shield>().set(false);

            ability.sprite = Resources.Load<Sprite>("Dash");
		}
        else if (shield_)
        {           
            GetComponent<Dash>().setAvailable(false);
			ShieldObject.GetComponent<Shield>().set(true);

            ability.sprite = Resources.Load<Sprite>("Escudo");

            //Tú
        }
        else if (lifeSteal_)
        {
            GetComponent<Dash>().setAvailable(false);
			ShieldObject.GetComponent<Shield>().set(false);

            ability.sprite = Resources.Load<Sprite>("AbsorbeVida");
        }
    }

    //Health
    public int GetHealth()
    {
        return health_;
    }

    public void SetHealth(int amount)
    {
        health_ = amount;
    }

    public void ReduceHealth(int amount)
    {
        health_ -= amount;
        if (health_ <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            int score = GameManagerComp.instance.GetScore();
            if (score != 85) SceneManager.LoadScene("EndScene");
            else SceneManager.LoadScene("EasterEgg");
        }
        if (health_ > 100) health_ = 100;
    }

    public void IncreaseHealth(int amount)
    {
        health_ += amount;
    }
  

    //Powers
    public bool GetPower(int power)
    {
        if(power == 1)
            return dash_;
        else if(power == 2)
            return shield_;
        else
            return lifeSteal_;
    }

    public void SetPowerTrue(int power, bool fountain)
    {
        currentPower_ = power;
        dash_ = false;
        shield_ = false;
        lifeSteal_ = false;
        fountainPower_ = fountain;
        timeLeft_ = 20.0f;

        gun_.setPower(power);

        if (power == 1) 
            dash_ = true;

        else if (power == 2)
            shield_ = true;
        else
            lifeSteal_ = true;
    }

}
