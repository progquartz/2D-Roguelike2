using System.Collections;
using System.Collections.Generic; // using lists
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null; // 이 뜻은 이제 gameManager 내의 모든 변수와 함수가 게임 상에서의 모든 스크립트에서 사용 가능해진다는 의미이다.
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true; // hideininstpector 는 해당 public 변수가 값에 보이지 않도록 하는것이다.

    private Text levelText;
    private GameObject levelImage;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    private void Awake()
    {
        if (instance == null)
            instance = this; // 만약 instance가 비어있다면, 이 인스턴스를 게임 매니저로 채워넣는다. 
        else if( instance != this)
        {
            Destroy(gameObject); // 만약 instance값이 달라져있다면 (이미 실행된 상태라면) 이걸 뽀사버린다.
        }


        DontDestroyOnLoad(gameObject); // 만약 새로운 장면을 로드할때 일반적으로 모든 게임 오브젝트는 사라질것이다. 게임 매니저가 점수를 확인할수 있어야만 하는데 사라지면 안되니까 이를 넣는다.
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        level++;

        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
        enemies.Clear(); // 게임 매니저가 시작할때 리셋하지 않을것이기 때문에 해야함.
        boardScript.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver() {
        levelText.text = "After " + level + "days, you starved";
        enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(playersTurn || enemiesMoving || doingSetup)
        {
            return;
        }
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList (Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;  //DO NOT FORGET THESE
        enemiesMoving = false;  //DO NOT FORGET THESE
    }
}
