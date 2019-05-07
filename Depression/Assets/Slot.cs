using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public GameObject item {
		get {
			if(transform.childCount>0){
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if(!item){
			//GameObject Store;
			GameObject boxClone;
			GameObject box = DragHandeler.itemBeingDraged; // sets box to GameObject this script is attached to
            //Store = box.transform.parent.parent.parent.parent.gameObject; // set store equal to parent of box
            boxClone = Instantiate (box) as GameObject; // clone box
            boxClone.transform.SetParent( DragHandeler.itemBeingDraged.transform.parent);
            boxClone.GetComponent<CanvasGroup>().blocksRaycasts = true;
            //boxClone.AddComponent<DragHandeler>();

            //boxClone.transform.SetParent(Store.transform); 
            //UnityEditor.GameObjectUtility.SetParentAndAlign(boxClone, box.transform.parent.parent.parent.parent.gameObject);

			DragHandeler.itemBeingDraged.transform.SetParent (transform);
			boxClone.name=DragHandeler.itemBeingDraged.name;
			ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged ());
		}
	}
	#endregion
}