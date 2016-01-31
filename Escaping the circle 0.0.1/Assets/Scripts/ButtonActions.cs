﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//This script is a collection different actions typical for a button
// Mattias Tronslien 2016

public class ButtonActions : MonoBehaviour
{
    private GazePointDataComponent gaze;
    PointerEventData _pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> _raycastResult = new List<RaycastResult>();
    Button _button;
    float _lerpColor;
    Image _rend;
    float _activateTimer;
    bool _isLookedAt;
    private EyeXHost _eyexHost;


    void Start()
    {
        _eyexHost = EyeXHost.GetInstance();
        gaze = GetComponent<GazePointDataComponent>();
        _button = GetComponent<Button>();
        _rend = GetComponent<Image>();
    }

	public int _PreviousScene;


    public void ChangeLevel(int IndexOfScene)
    {
        SceneManager.LoadScene(IndexOfScene);
    } 

	public void Exit () {
		Application.Quit();
	}

	void Update ()
    {
        var eyeTrackerDeviceStatus = _eyexHost.EyeTrackingDeviceStatus;
        if (eyeTrackerDeviceStatus != EyeXDeviceStatus.Tracking)
        { return; }
        _isLookedAt = false;
        _pointer.position = gaze.LastGazePoint.Screen;
        EventSystem.current.RaycastAll(_pointer,_raycastResult);
        if (_raycastResult.Count > 0)
        {
            if (_raycastResult[0].gameObject.transform.parent.tag == "Button")
            {
                if (_raycastResult[0].gameObject.transform.parent.name == name)
                {
                    _isLookedAt = true;
                    
                }
            }
        }

		if(Input.GetButtonDown("Cancel"))
		{
			SceneManager.LoadScene(_PreviousScene);
		}


        if (_isLookedAt)
        {
            ExecuteEvents.Execute(gameObject, _pointer, ExecuteEvents.pointerEnterHandler);
            /*if (_lerpColor < 1.05f)
            { _lerpColor += 0.1f; }*/
            _activateTimer += Time.deltaTime;
        }
        else
        {
            ExecuteEvents.Execute(gameObject, _pointer, ExecuteEvents.pointerExitHandler);
            /*if (_lerpColor > 0f)
            { _lerpColor -= 0.05f; }*/
            if (_activateTimer > 0)
            {
                _activateTimer -= Time.deltaTime;
            }
        }
        //_rend.color = Color.Lerp(Color.white, new Vector4(80f/255f,139f/255f,236f/255f,255f/255f), _lerpColor);

        _rend.color = Color.Lerp(Color.white, Color.red, _activateTimer/3);

        if (_activateTimer > 3f)
        {
            ExecuteEvents.Execute(gameObject, _pointer, ExecuteEvents.submitHandler);
        }

    }
}
