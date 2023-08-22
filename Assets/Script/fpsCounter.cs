using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fpsCounter : MonoBehaviour
{
    public TMP_Text fps_Text;

    [SerializeField]
    private float _refreshTime = 0.5f;
    private int _frameCounter;
    private float _timeCounter;
    private float _fps;


    void Update()
    {
        if (_timeCounter < _refreshTime)
        {
            _timeCounter += Time.deltaTime;
            _frameCounter++;
        }
        else
        {
            _fps = _frameCounter / _timeCounter;
            _frameCounter = 0;
            _timeCounter = 0;
        }
        fps_Text.text = "FPS: " + _fps.ToString();

    }

}
