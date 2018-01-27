using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class PointerEventListener : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler{
	public delegate void VoidDelegate ();
	public VoidDelegate onClick;
	public VoidDelegate onDown;
	public VoidDelegate onEnter;
	public VoidDelegate onExit;
	public VoidDelegate onUp;
	public System.Action<PointerEventData> onDownEvent;
	public System.Action<PointerEventData> onUpEvent;
	public System.Action<PointerEventData> onClickEvent;

    #region States

    protected bool isHover = false;
    public bool IsHover
    {
        get
        {
            return isHover;
        }
    }

    #endregion

    static public PointerEventListener Get (GameObject go)
	{
		PointerEventListener listener = go.GetComponent<PointerEventListener>();
		if (listener == null) listener = go.AddComponent<PointerEventListener>();
		return listener;
	}

	#region IPointerEnterHandler implementation
	public void OnPointerEnter (PointerEventData eventData){
        isHover = true;
        if (onEnter != null) onEnter();
	}
	#endregion

	#region IPointerDownHandler implementation
	public void OnPointerDown (PointerEventData eventData){
		if(onDown != null) onDown();
		if(onDownEvent != null) 	onDownEvent(eventData);
	}
	#endregion

	#region IPointerExitHandler implementation
	public void OnPointerExit (PointerEventData eventData){
        isHover = false;
        if (onExit != null) onExit();
	}
	#endregion

	#region IPointerUpHandler implementation
	public void OnPointerUp (PointerEventData eventData){
		if(onUp != null) onUp();
		if(onUpEvent != null) 	onUpEvent(eventData);
	}
	#endregion

	#region IPointerClickHandler implementation
	public void OnPointerClick(PointerEventData eventData)
	{
		if(onClick != null) 	onClick();
		if(onClickEvent != null) 	onClickEvent(eventData);
	}
	#endregion
}