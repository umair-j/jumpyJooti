using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class level : MonoBehaviour
{
    private state currentState; 

    private const float playerPositionX = 0f;
    private int obstaclesPassed = 0;
    private static level instance;
    private float gapSize;
    private int obstaclesSpawned = 0;
    private float maxTimeToSpawn = 2f;
    private float timeToSpawn;
    private float destroyLocation = -70f;
    private float spawnLocation = 70f;
    private float cameramax = 60f;
    private const float obstacleWidth = 30f;
    private const float obstacleSpeed = 30f;

    private List<obstacle> obstacleList;
    public enum difficulty
    {
        superEasy,
        easy,
        medium,
        hard,
        veryHard,
        impossible,
    }
    private enum state
    {
        waiting,
        playing,
        dead,
    }
    public static level getInstance()
    {
        return instance;
    }
    public int getObstaclesSpawned()
    {
        return obstaclesSpawned;
    }
    public int getObstaclespassed()
    {
        return obstaclesPassed;
    }
    private void setDifficulty(difficulty d)
    {
        switch (d)
        {
            case difficulty.superEasy:
                gapSize = 24f;
                break;
            case difficulty.easy:
                gapSize = 22f;
                break;
            case difficulty.medium:
                gapSize = 20f;
                break;
            case difficulty.hard:
                gapSize = 18f;
                break;
            case difficulty.veryHard:
                gapSize = 16f;
                break;
            case difficulty.impossible:
                gapSize = 14f;
                break;
        }
    }
    private difficulty getDifficulty()
    {
        if(obstaclesSpawned < 5)
        {
            return difficulty.superEasy;
        }
        else if(obstaclesSpawned < 10)
        {
            return difficulty.easy;
        }

        else if (obstaclesSpawned < 15)
        {
            return difficulty.medium;
        }

        else if (obstaclesSpawned < 20)
        {
            return difficulty.hard;
        }
        else if (obstaclesSpawned < 25)
        {
            return difficulty.veryHard;
        }

        else 
        {
            return difficulty.impossible;
        }
    }
    private void obstacleMovement()
    {
        for(int i = 0; i < obstacleList.Count; i++)
        {
            bool obstacleNotPassed = obstacleList[i].returnposition() > playerPositionX;
            
            obstacleList[i].move();
            if (obstacleNotPassed && obstacleList[i].returnposition()<playerPositionX)
            {
                obstaclesPassed++;
            }
            if (obstacleList[i].returnposition() < destroyLocation)
            {
                obstacleList[i].destroyObstacle();
                obstacleList.Remove(obstacleList[i]);
                i--;
            }
        }
    }

    private void Awake()
    {
        instance = this;
        obstacleList = new List<obstacle>();
        timeToSpawn = maxTimeToSpawn;
        setDifficulty(difficulty.easy);
        currentState = state.waiting;
    }
    private void Start()
    {
        //createGapObstacle(50f, 10f, 10f);
        //createGapObstacle(80f, 20f, 20f);
        //createGapObstacle(50f, 0f, 5f);
        jooti.returnJooti().onDied += jootiOnDied;
        
    }

    private void jootiOnDied(object sender, System.EventArgs e)
    {
        currentState = state.dead;
        
    }

    private void Update()
    {
        if(currentState == state.waiting)
        {
            jooti.returnJooti().onStartPlaying += jootiOnStartPlaying;
        }
        if (currentState == state.playing)
        {
            obstacleMovement();
            obstacleSpawnHandler();
        }
    }

    private void jootiOnStartPlaying(object sender, System.EventArgs e)
    {
        currentState = state.playing;
    }

    public void obstacleSpawnHandler()
    {
        
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            timeToSpawn += maxTimeToSpawn;
            float randomHeight = UnityEngine.Random.Range(30, 70);
            createGapObstacle(randomHeight, spawnLocation, gapSize);
        }
    }
    private void createGapObstacle(float y,float x,float gap)
    {
        Transform a = createObstacle(y - gap+10f, x, true);
        Transform b = createObstacle(cameramax+60f -y - gap, x, false);
        obstacle ob = new obstacle(a, b);
        obstacleList.Add(ob);
        obstaclesSpawned++;
        setDifficulty(getDifficulty());
    }
    private Transform createObstacle(float height, float xPos, bool straight)
    {
        if (!straight)
        {
            Transform obstacle1 = Instantiate(gameAssetsScript.getGameAssetsScript().legObstacle1);
            obstacle1.position = new Vector3(xPos, -cameramax);
            SpriteRenderer obstacleSprite = obstacle1.GetComponent<SpriteRenderer>();
            obstacleSprite.size = new Vector2(obstacleWidth, height);
            BoxCollider2D obstacle1BoxCollider = obstacle1.GetComponent<BoxCollider2D>();
            obstacle1BoxCollider.size = new Vector2(obstacleWidth * 0.4f, height);
            obstacle1BoxCollider.offset = new Vector2(0f, height * -0.4f);
            return obstacle1;
        }
        else
        {
            Transform obstacle2 = Instantiate(gameAssetsScript.getGameAssetsScript().straightLegObstacle1);
            obstacle2.position = new Vector3(xPos, cameramax);
            SpriteRenderer obstacleSprite2 = obstacle2.GetComponent<SpriteRenderer>();
            obstacleSprite2.size = new Vector2(obstacleWidth, height);
            BoxCollider2D obstacle2BoxCollider = obstacle2.GetComponent<BoxCollider2D>();
            obstacle2BoxCollider.size = new Vector2(obstacleWidth * 0.4f, height);
            obstacle2BoxCollider.offset = new Vector2(0f, height * -0.4f);
            return obstacle2;
        }
    }
    private class obstacle
    {
        private Transform up;
        private Transform down;
        public obstacle(Transform u,Transform d)
        {
            this.up = u;
            this.down = d;
        }
        public void move()
        {
            this.up.position += new Vector3(-1, 0, 0) * obstacleSpeed * Time.deltaTime;
            this.down.position += new Vector3(-1, 0, 0) * obstacleSpeed * Time.deltaTime;
        }
        public float returnposition()
        {
            return down.position.x;
        }
        public void destroyObstacle()
        {
            Destroy(up.gameObject);
            Destroy(down.gameObject);
        }
    }

}
