using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RemoteControlScriptableObject", order = 1)]
public class ServerData : ScriptableObject
{
    private GameObject UiData;
    private GameObject PlayerData;
    [SerializeField] private EventTrigger[] eventTriggers;

    public GameObject GetUI()
    {
        UiData = GameObject.FindGameObjectWithTag("UI");
        return UiData;
    }

    public void SetCamera()
    {
        Camera.main.GetComponent<CameraFollow>().target = PlayerData.transform;
    }

    public GameObject GetPlayer()
    {
        PlayerData = GameObject.FindGameObjectWithTag("Player");
        return PlayerData;
    }

    public void SetRemote()
    {
        CarController carRef = GetPlayer().GetComponent<CarController>();
        eventTriggers = UiData.GetComponentsInChildren<EventTrigger>();
        SetLeft(carRef, eventTriggers[0]);
        SetRight(carRef, eventTriggers[1]);
        SetBrakes(carRef, eventTriggers[2]);
        SetAccelerator(carRef, eventTriggers[3]);
        SetReverse(carRef, eventTriggers[4]);

    }

    private void SetLeft(CarController car, EventTrigger eventTrigger)
    {
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry2.eventID = EventTriggerType.PointerUp;
        entry3.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventData) => { car.SteerInput(-1); });
        entry2.callback.AddListener((eventData) => { car.SteerInput(0); });
        entry3.callback.AddListener((eventData) => { car.SteerInput(0); });
        eventTrigger.triggers.Add(entry1);
        eventTrigger.triggers.Add(entry2);
    }

    private void SetRight(CarController car, EventTrigger eventTrigger)
    {
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry2.eventID = EventTriggerType.PointerUp;
        entry3.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventData) => { car.SteerInput(1); });
        entry2.callback.AddListener((eventData) => { car.SteerInput(0); });
        entry3.callback.AddListener((eventData) => { car.SteerInput(0); });
        eventTrigger.triggers.Add(entry1);
        eventTrigger.triggers.Add(entry2);
    }
    private void SetBrakes(CarController car, EventTrigger eventTrigger)
    {
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry2.eventID = EventTriggerType.PointerUp;
        entry3.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventData) => { car.BrakeInput(true); });
        entry2.callback.AddListener((eventData) => { car.BrakeInput(false); });
        entry3.callback.AddListener((eventData) => { car.BrakeInput(false); });
        eventTrigger.triggers.Add(entry1);
        eventTrigger.triggers.Add(entry2);
    }
    private void SetAccelerator(CarController car, EventTrigger eventTrigger)
    {
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry2.eventID = EventTriggerType.PointerUp;
        entry3.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventData) => { car.AccelInput(1); });
        entry2.callback.AddListener((eventData) => { car.AccelInput(0); });
        entry3.callback.AddListener((eventData) => { car.AccelInput(0); });
        eventTrigger.triggers.Add(entry1);
        eventTrigger.triggers.Add(entry2);
    }
    private void SetReverse(CarController car, EventTrigger eventTrigger)
    {
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry2.eventID = EventTriggerType.PointerUp;
        entry3.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventData) => { car.AccelInput(-1); });
        entry2.callback.AddListener((eventData) => { car.AccelInput(0); });
        entry3.callback.AddListener((eventData) => { car.AccelInput(0); });
        eventTrigger.triggers.Add(entry1);
        eventTrigger.triggers.Add(entry2);
    }
}




