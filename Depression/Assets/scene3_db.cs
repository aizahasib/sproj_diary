// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.

public class scene3_db : MonoBehaviour {

[SerializeField] Transform slots;

static public string[] inventoryName;
  ArrayList leaderBoard;
protected Firebase.Auth.FirebaseAuth auth;
  private const int MaxScores = 5;
  private string logText = "";
  private string name = "";
  private int score = 100;
    private bool addScorePressed;
private string inventory = "";
  const int kMaxLogSize = 16382;
  DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

  // When the app starts, check to make sure that we have
  // the required dependencies to use Firebase, and if not,
  // add them if possible.
  void Start() {
        addScorePressed = true;
     inventoryName=new string[12];
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
      dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available) {
        InitializeFirebase();
      } else {
        Debug.LogError(
          "Could not resolve all Firebase dependencies: " + dependencyStatus);
      }
    });
  }

  // Initialize the Firebase database:
  protected virtual void InitializeFirebase() {
    FirebaseApp app = FirebaseApp.DefaultInstance;
    // NOTE: You'll need to replace this url with your Firebase App's database
    // path in order for the database connection to work correctly in editor.
    app.SetEditorDatabaseUrl("https://diary-3d3a4.firebaseio.com/");
    if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    //StartListener();
  }


  // Exit if escape (or back, on mobile) is pressed.
  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }}
        
  // Output text to the debug log text field, as well as the console.
  public void DebugLog(string s) {
    Debug.Log(s);
    logText += s + "\n";

    while (logText.Length > kMaxLogSize) {
      int index = logText.IndexOf("\n");
      logText = logText.Substring(index + 1);
    }
  }



  public void AddScore() {
     int index=0;
     
         foreach (Transform slotTransform in slots) { 
         GameObject item = slotTransform.GetComponent<Slot>().item;
         if (item){
             inventoryName[index]=item.name;
             FirebaseDatabase.DefaultInstance.GetReference("Users").Child(LoginHandler.id).Child("emotions").Child(index.ToString()).SetValueAsync(item.name);

         }else{
            inventoryName[index]=null;
            FirebaseDatabase.DefaultInstance.GetReference("Users").Child(LoginHandler.id).Child("emotions").Child(index.ToString()).SetValueAsync("null");
         }
         index++;
     }
    
/*auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
  //DebugLog(auth.CurrentUser.UserId);
  if(auth.CurrentUser!=null){
      
      DebugLog("it is null");
    }else {}*/
    
    
        addScorePressed = true;
        //SceneManager.LoadSceneAsync("scene_3");
    }


}
