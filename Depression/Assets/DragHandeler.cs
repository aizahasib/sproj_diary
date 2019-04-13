using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
 public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
 public static GameObject itemBeingDraged;
 Vector3 startPosition;
 Transform startParent;
 
 #region IBeginDragHandler implementation
 public void OnBeginDrag (PointerEventData eventData)
 {
     itemBeingDraged = gameObject;
     startPosition = transform.position;
     startParent = transform.parent;
     GetComponent<CanvasGroup>().blocksRaycasts = false;
 }
 #endregion
 
 
 #region IDragHandler implementation
 public void OnDrag (PointerEventData eventData)
 {
     transform.position = Input.mousePosition;
 }
 #endregion
 
 #region IEndDragHandler implementation
 
 public void OnEndDrag (PointerEventData eventData)
 {
     itemBeingDraged = null;
     GetComponent<CanvasGroup>().blocksRaycasts = true;
     if (transform.parent != startParent) {
         transform.position = startPosition;
     }
 }
 
 #endregion
 }