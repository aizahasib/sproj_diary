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
    
            boxClone = Instantiate (box) as GameObject; // clone box
            boxClone.transform.SetParent( DragHandeler.itemBeingDraged.transform.parent);
            boxClone.GetComponent<CanvasGroup>().blocksRaycasts = true;
      

			DragHandeler.itemBeingDraged.transform.SetParent (transform);
			boxClone.name=DragHandeler.itemBeingDraged.name;
			boxClone.transform.localScale += new Vector3(0.5F, 0.5F, 0.5F);
			ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged ());
		}
	}
	#endregion
}