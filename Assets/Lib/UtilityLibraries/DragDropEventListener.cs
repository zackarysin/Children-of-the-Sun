using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class DragDropEventListener : MonoBehaviour,IDragHandler,IEndDragHandler,IDropHandler {
	public delegate void Vector2Delegate (Vector2 delta);
	public event Vector2Delegate onDrag;
	public event Vector2Delegate onEndDrag;
	public event Vector2Delegate onDrop;
	public System.Action<PointerEventData> onDragEvent;
	public System.Action<PointerEventData> onEndDragEvent;
	
	static public DragDropEventListener Get (GameObject go)
	{
		DragDropEventListener listener = go.GetComponent<DragDropEventListener>();
		if (listener == null) listener = go.AddComponent<DragDropEventListener>();
		return listener;
	}

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		if(onDrag != null) onDrag(eventData.delta);
		if(onDragEvent != null) onDragEvent(eventData);
	}
	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		if(onEndDrag != null) onEndDrag(eventData.delta);
		if(onEndDragEvent != null) onEndDragEvent(eventData);
	}

	#endregion

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if(onDrop != null) onDrop(eventData.delta);
	}

	#endregion
}
