using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class loadimage : MonoBehaviour {

    void Start () {
    	Sprite sprite = Resources.Load <Sprite> ("F:/Spring 2019/Sproj/sproj_diary/Depression/Assets/Items/Gaming.png");

        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewImage.sprite = sprite; //Set the Sprite of the Image Component on the new GameObject
    	        NewObj.SetActive(true); //Activate the GameObject

    }

    void Update(){


    }
}