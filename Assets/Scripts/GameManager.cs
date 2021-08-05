using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool paused;

    [SerializeField] private int _wheatCount;
    [SerializeField] private Text _wheatCountText;
    [SerializeField] private int _warriorCount;
    [SerializeField] private Text _warriorCountText;
    [SerializeField] private int _peasantCount;
    [SerializeField] private Text _peasantCountText;

    [SerializeField] private int _enemyCount;
    [SerializeField] private int _enemyCountIncreasive;
    [SerializeField] private Text _enemyCountText;

    [SerializeField] private float TimeCreateWarrior;
    [SerializeField] private float TimeCreatePeasant;
    [SerializeField] private int PriceWarrior;
    [SerializeField] private int PricePeasant;
    [SerializeField] private int _peasantCreateWheatCount; // кол-во производимых единиц пшеницы за раз один рабочим
    [SerializeField] private int _warriorEatWheatCount; //кол-во съеденых единиц одним войном за раз 
    [SerializeField] private int _peasantEatWheatCount; //кол-во съеденых единиц одним рабочим за раз

    [SerializeField] private Image _timerRaid;
    [SerializeField] private float _timeTimerRaid;
    [SerializeField] private Image _timerEating;
    [SerializeField] private float _timeTimerEating;
    [SerializeField] private Image _timerHarwest;
    [SerializeField] private float _timeTimerHarwest;
    private float _timerCreatePeasant = -2;
    private float _timerCreateWarrior = -2;

    [SerializeField] private Button ButtonCreatePeasant;
    [SerializeField] private Button ButtonCreateWarrior;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private Sprite _finishWin;
    [SerializeField] private Sprite _finishLose;
    [SerializeField] private string _finishWordsWin;
    [SerializeField] private string _finishWordsLose;
    [SerializeField] private int _conditionWin;
    private SoundScript audioResours;


    void Start()
    {

        Time.timeScale = 0;
        _timerRaid.GetComponent<TimerScript>().MaxTime = _timeTimerRaid;
        _timerEating.GetComponent<TimerScript>().MaxTime = _timeTimerEating;
        _timerHarwest.GetComponent<TimerScript>().MaxTime = _timeTimerHarwest;
        ButtonCreatePeasant.transform.GetChild(2).GetComponent<Text>().text = PricePeasant.ToString();
        ButtonCreateWarrior.transform.GetChild(2).GetComponent<Text>().text = PriceWarrior.ToString();
        audioResours = GetComponent<SoundScript>();


    }

    void Update()
    {

        //увеличиваем количество пшеницы если прошел таймер сбора урожая
        if (_timerHarwest.GetComponent<TimerScript>().IsTick == true)
        {
            audioResours.HarwestSound();
            CreateWheat(_peasantCount * _peasantCreateWheatCount);
            if (_wheatCount >= _conditionWin)
            {
                Pause();
                audioResours.WinFinalSound();
                _finishPanel.SetActive(true);
                _finishPanel.transform.GetChild(0).GetComponent<Image>().sprite = _finishWin;
                _finishPanel.transform.GetChild(1).GetComponent<Text>().text = _finishWordsWin;
            }
        }
        //уменьшаем пшеницу если прошел таймер кормления
        if (_timerEating.GetComponent<TimerScript>().IsTick == true)
        {
            audioResours.EatSound();
            EatWheat(_warriorCount * _warriorEatWheatCount + _peasantCount * _peasantEatWheatCount);
            if (_wheatCount < 0)
            {
                _warriorCount--;
                _wheatCount = 0;
            }
        }
        //реализация нападения
        
        if (_timerRaid.GetComponent<TimerScript>().IsTick == true)
        {
            audioResours.FightSound();
            _warriorCount -= _enemyCount;
            _enemyCount += _enemyCountIncreasive;
            if (_warriorCount < 0)
            {
                
                Pause();
               audioResours.LoseFinalSound();
                _finishPanel.SetActive(true);
                _finishPanel.transform.GetChild(0).GetComponent<Image>().sprite = _finishLose;
                _finishPanel.transform.GetChild(1).GetComponent<Text>().text = _finishWordsLose;
            }
        }
        //создаем воина
        if (_timerCreateWarrior > 0)
        {
            _timerCreateWarrior -= Time.deltaTime;
            ButtonCreateWarrior.transform.GetChild(3).GetComponent<Image>().fillAmount = _timerCreateWarrior / TimeCreateWarrior;

        }
        else if (_timerCreateWarrior > -1)
        {
            ButtonCreateWarrior.transform.GetChild(3).GetComponent<Image>().fillAmount = 1;

            _warriorCount++;
            audioResours.CreateWarriorSound();
            _timerCreateWarrior = -2;
            ButtonCreateWarrior.transform.GetChild(3).GetComponent<Image>().fillAmount = 1;
            ButtonCreateWarrior.interactable = true;
        }
        //создаем рабочего
        if (_timerCreatePeasant > 0)
        {
            _timerCreatePeasant -= Time.deltaTime;
            ButtonCreatePeasant.transform.GetChild(3).GetComponent<Image>().fillAmount = _timerCreatePeasant / TimeCreatePeasant;
        }
        else if (_timerCreatePeasant > -1)
        {
            ButtonCreatePeasant.transform.GetChild(3).GetComponent<Image>().fillAmount = 1;
            _peasantCount++;
            audioResours.CreatePearsantSound();
            _timerCreatePeasant = -2;

            ButtonCreatePeasant.interactable = true;
        }


        //присваиваем все значения в панель с информацией ресурсов 
        _wheatCountText.text = _wheatCount.ToString();
        _warriorCountText.text = _warriorCount.ToString();
        _peasantCountText.text = _peasantCount.ToString();
        _enemyCountText.text = _enemyCount.ToString();
    }

    private void CreateWheat(int createCount)
    {
        _wheatCount += createCount;
        CheckWhetButtonInterctable();

    }

    private void EatWheat(int eatCount)
    {
        _wheatCount -= eatCount;
        CheckWhetButtonInterctable();
    }
    public void ButtonPeasant()
    {

        EatWheat(PricePeasant);
        _timerCreatePeasant = TimeCreatePeasant;
        ButtonCreatePeasant.interactable = false;

    }
    public void ButtonWarrior()
    {

        EatWheat(PriceWarrior);
        _timerCreateWarrior = TimeCreateWarrior;
        ButtonCreateWarrior.interactable = false;


    }
    private void CheckWhetButtonInterctable()
    {
        if (_wheatCount < PricePeasant) ButtonCreatePeasant.interactable = false;
        else if (_wheatCount >= PricePeasant && (_timerCreatePeasant / TimeCreatePeasant) <= 0) ButtonCreatePeasant.interactable = true;


        if (_wheatCount < PriceWarrior) ButtonCreateWarrior.interactable = false;
        else if (_wheatCount >= PriceWarrior && (_timerCreateWarrior / TimeCreateWarrior) <= 0) ButtonCreateWarrior.interactable = true;



    }

    public void Pause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            
        }
        else
        {
            Time.timeScale = 0;
        }
        paused = !paused;
    }
}
