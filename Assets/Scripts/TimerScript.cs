using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float MaxTime;
    public bool IsTick=false;
    private float _carrentTime;
    private Image _image;
 
    void Start()
    {
        _image = GetComponent<Image>();
        _carrentTime = MaxTime;
      
    }

  
    void Update()
    {

        IsTick = false;
        if (_carrentTime<=0)
        {
            IsTick = true;
            _carrentTime = MaxTime;
        }
        _image.fillAmount = _carrentTime / MaxTime;
        _carrentTime -= Time.deltaTime;
    }


}
