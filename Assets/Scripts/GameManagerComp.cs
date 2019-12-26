using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerComp : MonoBehaviour {
    public static GameManagerComp instance = null;

    public List<GameObject> flyEnemies_ = new List<GameObject>();
    public List<GameObject> slimeEnemies_ = new List<GameObject>();
    public List<GameObject> robotEnemies_ = new List<GameObject>();

    public GameObject fly_;
    public GameObject slime_;
    public GameObject robot_;

	public GameObject Door;

    //SpawnZone
    float xm, xM, yf, yr, ys, zm, zM;
    enemies currentEnemies_;
    bool fFin, sFin, rFin;
    bool levelFinished_;
    //Cooldown
    float timeLeft_ = 0.0f;
    int score_;
    int currentLevel_;

	bool levelInitialized = false;

    public AudioSource audioSource;

    struct enemies
    {
        public int numSlimes_;
        public int numFlyers_;
        public int numRobots_;

        public enemies(int numFlyers, int numSlimes, int numRobots)
        {
            numFlyers_ = numFlyers;
            numSlimes_ = numSlimes;
            numRobots_ = numRobots;
        }
    };

	public void Reset()
	{
        levelFinished_ = false;

        flyEnemies_.Clear();
        slimeEnemies_.Clear();
        robotEnemies_.Clear();

		fFin = false;
		sFin = false;
		rFin = false;
	}

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        score_ = 0;
        currentLevel_ = 0;

        string scoreText = score_.ToString();
        levelFinished_ = false;
        //SpawnZone
        xm = -30.0f;
        xM = 50.0f;
        yf = -11.0f;
        yr = -11.8f;
        ys = -10.5f;
        zm = -40.0f;
        zM = 60.0f;

        fFin = false;
        sFin = false;
        rFin = false;

        Init();
    }

    public void Init()
    {
		if (!levelInitialized)
		{
            Camera.main.GetComponent<GlitchEffect>().enabled = false;

            currentLevel_++;
            Reset();
            levelInitialized = true;
			currentEnemies_ = AmountOfEnemiesInLvl(currentLevel_);

			Door.SetActive(true);
			FindObjectOfType<DoorBehaviour>().setEnemies(currentEnemies_.numFlyers_ + currentEnemies_.numSlimes_ + currentEnemies_.numRobots_);

            //Debug.Log("Random enemies: " + (currentEnemies_.numFlyers_ + currentEnemies_.numSlimes_ + currentEnemies_.numRobots_));
            //Debug.Log("LEVEL" + currentLevel_);

            Camera.main.GetComponent<GlitchEffect>().enabled = false;
        }
	}

    // Update is called once per frame
    void Update () {
        string scoreText = score_.ToString();
        if (!levelFinished_)
        {
            timeLeft_ -= Time.deltaTime;

            if (timeLeft_ < 0)
            {
                int rnd = Random.Range(1, 4);
                if ((rnd == 1 && !fFin))
                    if (flyEnemies_.Count < currentEnemies_.numFlyers_)
                    {
                        flyEnemies_.Add(InstantiateEnemy(fly_, GenerateRandom(fly_)));
                    }
                    else fFin = true;

                if ((rnd == 2 && !sFin))
                    if (slimeEnemies_.Count < currentEnemies_.numSlimes_)
                    {
                        slimeEnemies_.Add(InstantiateEnemy(slime_, GenerateRandom(slime_)));
                    }
                    else sFin = true;

                if ((rnd == 3 && !rFin))
                    if (robotEnemies_.Count < currentEnemies_.numRobots_)
                    {
                        robotEnemies_.Add(InstantiateEnemy(robot_, GenerateRandom(robot_)));
                    }
                    else rFin = true;
                if (fFin && sFin && rFin)
                {
                    levelFinished_ = true;
                }
                
                timeLeft_ = Random.Range(2, 5f);
            }
        }   
    }

    enemies AmountOfEnemiesInLvl(int level)
    {
        int rnd = Random.Range(0, 2);
        int x = 1;
        if (rnd == 1)
            x = -1;



		switch (level)
        {
            case 1:
                return new enemies(1, 1, 0);
            case 2:
                return new enemies(2, 2, 1);
            case 3:
                return new enemies(2, 3, 2);
            default:
                int dif = level + x * level / (level / 2);
                return new enemies(dif + rnd, dif + rnd, dif + rnd);
        }

		

	}

    Vector3 GenerateRandom(GameObject enemy )
    {
        float rndX = Random.Range(xm, xM);
        float rndZ = Random.Range(zm, zM);
        if(enemy == fly_)
            return new Vector3(rndX, yf, rndZ);
        else if(enemy == slime_)
            return new Vector3(rndX, ys, rndZ);
        else
            return new Vector3(rndX, yr, rndZ);
    }

    GameObject InstantiateEnemy (GameObject enemy, Vector3 pos)
    {
        return Instantiate(enemy, pos, Quaternion.identity);
    }

   public void Setlevelinitialized(bool x)
    {
        levelInitialized = x;
    }

    //Score
    public int GetScore()
    {
        return score_;
    }

    public void SetScore(int amount)
    {
        score_ = amount;
    }

    public void ReduceScore(int amount)
    {
        score_ -= amount;
    }

    public void IncreaseScore(int amount)
    {
        audioSource.Play();
        score_ += amount;
    }
}
