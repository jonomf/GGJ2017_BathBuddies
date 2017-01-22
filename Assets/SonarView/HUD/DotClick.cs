using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DotClick : MonoBehaviour, IPointerDownHandler {

	private Vector3 clickMousePos;
	private Vector3 clickPos;
	private bool mouseDown;
	public GameObject itemPrefab;
	public Transform ParentPanel;

	public void OnPointerDown(PointerEventData ped) 
	{
		mouseDown = true;
		clickPos = transform.position;
		clickMousePos = Input.mousePosition;
		HUDController.Instance.OnPanelClick(ped);

	}

}