﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//This script is a collection different actions typical for a button
// Mattias Tronslien 2016

public class ButtonActions : MonoBehaviour {

	public int _PreviousScene;

    public void ChangeLevel(int IndexOfScene) {
        SceneManager.LoadScene(IndexOfScene);
    } 

	public void Exit () {
		Application.Quit();
	}
	void Update () {
		if(Input.GetButtonDown("Cancel"))
		{
			SceneManager.LoadScene(_PreviousScene);
		}

	}

}
