using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 作用
/// 方便给UI控件添加相应事件，不然如果想实现点击功能，得手动给其添加EventTrigger组件，并绑定相应事件和对应回调
/// 如果是自定义的UI控件，得继承IPointerClickHandler 接口
/// 注意，要使用此功能需要给当前摄像机上添加Physics Raycaster 控件
/// 
/// 用法
/// var el = EventTriggerListener.GetListener(obj);
/// el.onClick += myCLick;
/// 
/// void myCLick(GameObject obj){ }
/// </summary>



namespace BaseFramework.Core
{

    public class EventTriggerListener : EventTrigger
    {

        #region

        public delegate void UIDelegate(GameObject go, BaseEventData baseEventData);

        public event UIDelegate onEnter;
        public event UIDelegate onExit;
        public event UIDelegate onDown;
        public event UIDelegate onUp;
        public event UIDelegate onClick;
        public event UIDelegate onInitializePotentialDrag;
        public event UIDelegate onBeginDrag;
        public event UIDelegate onDrag;
        public event UIDelegate onEndDrag;
        public event UIDelegate onDrop;
        public event UIDelegate onScroll;
        public event UIDelegate onUpdateSelected;
        public event UIDelegate onSelect;
        public event UIDelegate onDeselect;
        public event UIDelegate onMove;
        public event UIDelegate onSubmit;
        public event UIDelegate onCancel;

        #endregion

        public static EventTriggerListener GetListener(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null)
                listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        #region 方法

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null) onEnter(gameObject, eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null) onExit(gameObject, eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null) onDown(gameObject, eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null) onUp(gameObject, eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject, eventData);
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (onInitializePotentialDrag != null) onInitializePotentialDrag(gameObject, eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (onBeginDrag != null) onBeginDrag(gameObject, eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null) onDrag(gameObject, eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null) onEndDrag(gameObject, eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (onDrop != null) onDrop(gameObject, eventData);
        }

        public override void OnScroll(PointerEventData eventData)
        {
            if (onScroll != null) onScroll(gameObject, eventData);
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (onUpdateSelected != null) onUpdateSelected(gameObject, eventData);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            if (onSelect != null) onSelect(gameObject, eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            if (onDeselect != null) onDeselect(gameObject, eventData);
        }

        public override void OnMove(AxisEventData eventData)
        {
            if (onMove != null) onMove(gameObject, eventData);
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            if (onSubmit != null) onSubmit(gameObject, eventData);
        }

        public override void OnCancel(BaseEventData eventData)
        {
            if (onCancel != null) onCancel(gameObject, eventData);
        }

        #endregion
    }
}