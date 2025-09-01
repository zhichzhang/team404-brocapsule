using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static readonly Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    private static readonly Dictionary<string, Delegate> eventDictionaryParam = new Dictionary<string, Delegate>();

    public static EventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the EventManager persistent
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate EventManagers
        }
    }

    private void OnDestroy()
    {
        eventDictionary.Clear();
        eventDictionaryParam.Clear();
    }

    // Parameterless Events
    public static void StartListening(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            eventDictionary[eventName] -= listener;
            if (eventDictionary[eventName] == null)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
    public static void TriggerEventWithDelay(string eventName, float delay)
    {

        if (instance != null)
        {
            instance.StartCoroutine(TriggerEventCoroutine(eventName, delay));
        }
        else
        {
            Debug.LogError("EventManager instance is not initialized. Make sure an EventManager exists in the scene.");
        }

    }

    // Parameterized Events
    public static void StartListening<T>(string eventName, Action<T> listener)
    {
        if (eventDictionaryParam.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> typedEvent)
            {
                eventDictionaryParam[eventName] = typedEvent + listener;
            }
            else
            {
                Debug.LogError($"EventManager: Event {eventName} exists but with a different parameter type.");
            }
        }
        else
        {
            eventDictionaryParam[eventName] = listener;
        }
    }

    public static void StopListening<T>(string eventName, Action<T> listener)
    {
        if (eventDictionaryParam.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> typedEvent)
            {
                eventDictionaryParam[eventName] = typedEvent - listener;
                if (eventDictionaryParam[eventName] == null)
                {
                    eventDictionaryParam.Remove(eventName);
                }
            }
        }
    }

    public static void TriggerEvent<T>(string eventName, T param)
    {
        if (eventDictionaryParam.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> typedEvent)
            {
                typedEvent?.Invoke(param);
            }
            else
            {
                Debug.LogError($"EventManager: Event {eventName} exists but with a different parameter type.");
            }
        }
    }
    public static void TriggerEventWithDelay<T>(string eventName, T param, float delay)
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.TriggerEventWithDelayCoroutine(eventName, param, delay));
        }
        else
        {
            Debug.LogError("EventManager instance is not initialized. Make sure an EventManager exists in the scene.");
        }
    }

    // Coroutine to handle delayed event triggering with a parameter
    private IEnumerator TriggerEventWithDelayCoroutine<T>(string eventName, T param, float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerEvent(eventName, param);
    }

    private static IEnumerator TriggerEventCoroutine(string eventName, float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerEvent(eventName);
    }
}
