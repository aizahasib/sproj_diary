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
using System.Threading;
using System.Globalization;

// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.
public class RetrieveSelections : MonoBehaviour {
 [SerializeField] Transform imageSlots;
 [SerializeField] Transform selectedSlots;
public string[] inventoryN=new string[12];
  ArrayList leaderBoard;
protected Firebase.Auth.FirebaseAuth auth;
  private const int MaxScores = 5;
  private string logText = "";
  private string name = "";
  private int score = 100;
private string inventory = "";
  const int kMaxLogSize = 16382;
  public bool done=false;
  DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

  // When the app starts, check to make sure that we have
  // the required dependencies to use Firebase, and if not,
  // add them if possible.
  void Start() {
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
    done=true;
  }

 public void Update() {
        if(done){
          AddImages();
        }
  }

  // Output text to the debug log text field, as well as the console.
  public void DebugLog(string s) {
    Debug.Log(s);
    logText += s + "\n";

    while (logText.Length > kMaxLogSize) {
      int index = logText.IndexOf("\n");
      logText = logText.Substring(index + 1);
    }
  }

  // A realtime database transaction receives MutableData which can be modified
  // and returns a TransactionResult which is either TransactionResult.Success(data) with
  // modified data or TransactionResult.Abort() which stops the transaction with no changes.

  public void AddImages() {
     int index=0;

     for (int a = 0; a < 12; a++)
     {
       inventoryN[a]=DatabaseHandler.inventoryName[a];  
     }

     for (int i=0; i<12; i++){

        foreach (Transform slotTransform in imageSlots){
          GameObject item = slotTransform.GetComponent<Slot>().item;
          GameObject boxClone;
          GameObject box = item;
  


          if(inventoryN[i]!= null && item!=null && item.name == inventoryN[i] && selectedSlots.GetChild (i).transform.childCount==0){ 
              boxClone = Instantiate (box) as GameObject; // clone box
              boxClone.transform.SetParent( item.transform.parent);
              boxClone.GetComponent<CanvasGroup>().blocksRaycasts = true;
              boxClone.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
              slotTransform.GetComponent<Slot>().item.transform.SetParent (selectedSlots.GetChild (i));
              boxClone.name=item.name;
              break;
          }
     }

     }
    
    }
}
