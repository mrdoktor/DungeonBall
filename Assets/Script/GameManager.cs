using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum GameState
{
    menu,
    running,
    completed}
;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject menuView, gameView, levelDoneView;
    public BallScript ballPrefab;
    public Transform world;
    public LayerMask mask;
    private int scenes;
    private List<BallScript> touchList = new List<BallScript>();
    public GameState _state;

    public GameState state
    {
        get
        {
            return _state;
        }
        set
        {
            if (menuView != null)
                menuView.SetActive(value == GameState.menu);
            if (gameView != null)
                gameView.SetActive(value == GameState.running);
            if (levelDoneView != null)
                levelDoneView.SetActive(value == GameState.completed);
            _state = value;
        }
    }

    private int sceneIndex
    {
        get
        {
            return PlayerPrefs.GetInt("levelIndex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("levelIndex", value);
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        state = GameState.menu;
    }
	
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.running:
                List<BallScript> removeList = new List<BallScript>();
                #if UNITY_EDITOR
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, mask))
                    {
                        BallScript ball = Instantiate(ballPrefab);
                        ball.Init(hit.point, hit.collider.tag=="floor1"?Color.red:Color.green);
                        touchList.Add(ball);
                        ball.fingerID = 0;
                        ball.TouchBegan(Input.mousePosition);
                    }
                }
                else if (Input.GetMouseButton(0))
                {
                    for (int i = 0; i < touchList.Count; i++)
                    {
                        if (touchList[i].fingerID == 0)
                        {
                            touchList[i].TouchDrag(Input.mousePosition);
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    for (int i = 0; i < touchList.Count; i++)
                    {
                        if (touchList[i].fingerID == 0)
                        {
                            touchList[i].TouchEnded();
                            removeList.Add(touchList[i]);
                        }
                    }
                }
                    
                #endif

                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                                RaycastHit hit;
                                if (Physics.Raycast(ray, out hit, 100, mask))
                                {   
                                    BallScript ball = Instantiate(ballPrefab);
                                    ball.Init(hit.point, hit.collider.tag=="floor1"?Color.red:Color.green);
                                    touchList.Add(ball);
                                    ball.fingerID = touch.fingerId;
                                    ball.TouchBegan(touch.position);
                                 
                                }
                                break;
                            case TouchPhase.Stationary:
                            case TouchPhase.Moved:
                                for (int i = 0; i < touchList.Count; i++)
                                {
                                    if (touchList[i].fingerID == touch.fingerId)
                                    {
                                        touchList[i].TouchDrag(touch.position);
                                    }
                                }
                                break;

                            case TouchPhase.Ended:
                            case TouchPhase.Canceled:
                                for (int i = 0; i < touchList.Count; i++)
                                {
                                    if (touchList[i].fingerID == touch.fingerId)
                                    {
                                        touchList[i].TouchEnded();
                                        removeList.Add(touchList[i]);
                                    }
                                }
                                break;
                        }
                    }
                }

                foreach (BallScript handle in removeList)
                {
                    touchList.Remove(handle);
                }

                CheckGoals();
                break;
        }
    }

    void CheckGoals()
    {

    }


    public void LevelDone()
    {
        state = GameState.completed;
        SplatterManager.instance.Reset();
    }

    public void PlayButtonTapped()
    {
        state = GameState.running;
    }


    public void ContinueButtonTapped()
    {
        state = GameState.running;
    }

    public void MenuButtonTapped()
    {
        state = GameState.menu;
    }

  
}
