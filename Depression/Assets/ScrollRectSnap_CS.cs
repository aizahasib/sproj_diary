using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap_CS : MonoBehaviour 
{
	// Public Variables
	public RectTransform panel;	// To hold the ScrollPanel
	public RectTransform[] bttn;
	public RectTransform center;	// Center to compare the distance for each button


	//for emotion panel
	public RectTransform emotion_panel;	// To hold the ScrollPanel
	public RectTransform[] emotion_bttn;

	// Private Variables
	public float[] distance;	// All buttons' distance to the center
	public float[] distReposition;
	private bool dragging = false;	// Will be true, while we drag the panel
	private int bttnDistance;	// Will hold the distance between the buttons
	private int minButtonNum;	// To hold the number of the button, with smallest distance to center
	private int bttnLength;

	void Start()
	{
		bttnLength = bttn.Length;
		distance = new float[bttnLength];
		distReposition = new float[bttnLength];

		// Get distance between buttons
		bttnDistance  = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
	}

	void Update()
	{
		for (int i = 0; i < bttn.Length; i++)
		{
			distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
			distance[i] = Mathf.Abs(distReposition[i]);

			if (distReposition[i] > 1200)
			{
				float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
				float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
				float emotion_curY = emotion_bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
				Vector2 newAnchoredPos = new Vector2 (curX + (bttnLength * bttnDistance), curY);
				Vector2 emotion_newAnchoredPos = new Vector2 (curX + (bttnLength * bttnDistance), emotion_curY);
				bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
				emotion_bttn[i].GetComponent<RectTransform>().anchoredPosition = emotion_newAnchoredPos;
			}

			if (distReposition[i] < -1200)
			{
				float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
				float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
				float emotion_curY = emotion_bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
				Vector2 newAnchoredPos = new Vector2 (curX - (bttnLength * bttnDistance), curY);
				Vector2 emotion_newAnchoredPos = new Vector2 (curX - (bttnLength * bttnDistance), emotion_curY);
				bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
				emotion_bttn[i].GetComponent<RectTransform>().anchoredPosition = emotion_newAnchoredPos;
			}
		}
	
		float minDistance = Mathf.Min(distance);	// Get the min distance

		for (int a = 0; a < bttn.Length; a++)
		{
			if (minDistance == distance[a])
			{
				minButtonNum = a;
			}
		}

		if (!dragging)
		{
			LerpToBttn (-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
		}
	}

	void LerpToBttn(float position)
	{
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);
		Vector2 emotion_newPosition = new Vector2 (newX, emotion_panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;
		emotion_panel.anchoredPosition=emotion_newPosition;
	}

	public void StartDrag()
	{
		dragging = true;
	}

	public void EndDrag()
	{
		dragging = false;
	}

}













