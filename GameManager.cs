using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public int asteroidCount = 0; // amount of hited asteroids
    public Text asteroidHitText; // text for count of hited asteroids
    public Text recordScore;
    int highScore;

    public Text stationHealth; // text for station health count

    // Шаблон корабля, позиция его создания и текущий объект корабля
    public GameObject shipPrefab;
    public Transform shipStartPosition;
    public GameObject currentShip { get; private set; }

    // Шаблон космической станции, позиция ее создания и текущий объект станции
    public GameObject spaceStationPrefab;
    public Transform spaceStationStartPosition;
    public GameObject currentSpaceStation { get; private set; }

    // Сценарий, управляющий главной камерой
    public SmoothFollow cameraFollow;

    // Границы игры
    public Boundary boundary;

    // Контейнеры для разных групп элементов пользовательского интерфейса
    public GameObject inGameUI;
    public GameObject pausedUI;
    public GameObject gameOverUI;
    public GameObject mainMenuUI;

    // Предупреждающая рамка, которая появляется, когда игрок пересекает границу
    public GameObject warningUI;

    // Игра находится в состоянии проигрования?
    public bool gameIsPlaying { get; private set; }

    // Система создания астероидов
    public AsteroidSpawner AsteroidSpawner;

    // Признак приостановки игры.
    public bool paused;

    // Отображает главное меню в момент запуска игры
           
    void Start()
    {
        ShowMainMenu();
    }

    // Отображает заданный контейнер с элементами пользовательского интерфейса и скрывает все остальные.
    void ShowUI (GameObject newUI)
    {
        // Создать список всех контейнеров.
        GameObject[] allUI = { inGameUI, pausedUI, gameOverUI, mainMenuUI };

        // Скрыть их все.
        foreach (GameObject UIToHide in allUI)
        {
            UIToHide.SetActive(false);
        }

        // И затем отобразить указанный.
        newUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().StopMusic();
        ShowUI(mainMenuUI);
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlayIntroMusic();

        // Когда игра запускается, она находится не в состоянии проигрывания
        gameIsPlaying = false;

        // Запретить создавать астероиды
        AsteroidSpawner.spawnAsteroids = false;
    }

    // Вызывается в ответ на касание кнопки New Game
    public void StartGame()
    {
        // Вывести интерфейс игры
        ShowUI(inGameUI);
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().StopMusic();
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlayLevelMusic();

        recordScore.text = "Record: " + PlayerPrefs.GetInt("HighScore",0).ToString();

        // Перейти в режим игры
        gameIsPlaying = true;

        // Если корабль уже есть, удалить его 
        if (currentShip != null)
        {
            Destroy(currentShip);
        }

        // То же для станции
        if (currentSpaceStation != null)
        {
            Destroy(currentSpaceStation);
        }

        // Создать новый корабль и поместить его в начальную позицию
        currentShip = Instantiate(shipPrefab);
        currentShip.transform.position = shipStartPosition.position;
        currentShip.transform.rotation = shipStartPosition.rotation;

        // То же для станции
        currentSpaceStation = Instantiate(spaceStationPrefab);
        currentSpaceStation.transform.position = spaceStationStartPosition.position;
        currentSpaceStation.transform.rotation = spaceStationStartPosition.rotation;

        // Передать сценарию управления камерой ссылку на новый корабль, за которым она должна следовать
        cameraFollow.target = currentShip.transform;

        // начать создавать астероиды
        AsteroidSpawner.spawnAsteroids = true;
        
        // Сообщить системе создания астероидов

        AsteroidSpawner.target = currentSpaceStation.transform;
    }

    // Вызывается объектами, завершающими игру при разрушении
    public void GameOver()
    {
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().StopMusic();
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlayIntroMusic();
        UpdateScore(); // update highscore
        // Показать меню завершения игры
        ShowUI(gameOverUI);

        // Выйти из режима игры
        gameIsPlaying = false;

        // Удалить корабль и станцию
        if (currentShip != null)
            Destroy(currentShip);
        if (currentSpaceStation != null)
            Destroy(currentSpaceStation);

        // Скрыть предупреждающую рамку, если она видима
        warningUI.SetActive(false);

        // Прекратить создавать астероиды
        AsteroidSpawner.spawnAsteroids = false;

        // и удалить все уже созданные астероиды
        AsteroidSpawner.DestroyAllAsteroids();

        asteroidCount = 0; // обнулить счетчик
    }

    // Вызывается в ответ на касании кнопки Pause или Unpause
    public void SetPaused(bool paused)
    {
        //  Переключаться между интерфейсами паузы и игры
        inGameUI.SetActive(!paused);
        pausedUI.SetActive(paused);

        // Если игра приостановлена ...
        if (paused)
        {
            // Остановить время
            Time.timeScale = 0.0f;

        } else
        {
            // Возобновить ход времени
            Time.timeScale = 1.0f;
        }
    }

    // exit the game if pressed Quit Game button
    public void QuitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        // Если корабля нет, выйти
        if (currentShip == null)
            return;

        asteroidHitText.text = "Asteroids: " + asteroidCount; // update text if asteroid was hited by blaster
        stationHealth.text = "Station Health: " + GameObject.Find("Space Station(Clone)").GetComponent<DamageTaking>().hitPoints + " %";
        // Если корабль вышел за границу сферы уничтожения, завершить игру.
        // Если он внутри сферы уничтожения, но за границами сферы предупреждения, показать предупреждающую рамку.
        // Если он внутри обеих сфер, скрыть рамку.

        float distance = (currentShip.transform.position - boundary.transform.position).magnitude;

        if(distance > boundary.destroyRadius)
        {
            // Корабль за пределами сферы уничтожения, завершить игру
            GameOver();
        } else if (distance > boundary.warningRadius)
        {
            // Корабль за пределами сферы предупреждения, показать предупреждающую рамку
            warningUI.SetActive(true);
        } else
        {
            // Корабль внутри сферы предупреждения, скрыть рамку
            warningUI.SetActive(false);
        }
    }

    // updating of record score
    void UpdateScore()
    {
        if(asteroidCount > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore = asteroidCount;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        

    }
}
