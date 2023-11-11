//#define EVENTROUTER_THROWEXCEPTIONS
#if EVENTROUTER_THROWEXCEPTIONS
#define EVENTROUTER_REQUIRELISTENER
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public struct RGGameEvent
    {
        public string EventName;
        public bool isFail;
        static RGGameEvent e;
        public static void Trigger(string newName)
        {
            e.EventName = newName;
            RGEventManager.TriggerEvent(e);
        }
    }
    [ExecuteAlways]
    public class RGEventManager : MonoBehaviour
    {
        private static Dictionary<Type, List<IRGEventListenerBase>> _subscribersList;

        static RGEventManager()
        {
            _subscribersList = new Dictionary<Type, List<IRGEventListenerBase>>();
        }


        /// <summary>
        /// Adds a new subscriber to a certain event.
        /// </summary>
        /// <param name="listener">listener.</param>
        /// <typeparam name="RGEvent">The event type.</typeparam>
        public static void AddListener<RGEvent>(IRGEventListener<RGEvent> listener) where RGEvent : struct
        {
            Type eventType = typeof(RGEvent);
            if (!_subscribersList.ContainsKey(eventType))
            {
                _subscribersList[eventType] = new List<IRGEventListenerBase>();
            }
            if (!SubscriptionExists(eventType, listener))
            {
                _subscribersList[eventType].Add(listener);
            }
        }
        public static void RemoveListener<MMEvent>(IRGEventListener<MMEvent> listener) where MMEvent : struct
        {
            Type eventType = typeof(MMEvent);
            if (!_subscribersList.ContainsKey(eventType))
            {
#if EVENTROUTER_THROWEXCEPTIONS
                throw new ArgumentException(string.Format("Removing listener \"{0}\", but the event type \"{1}\" isn't registered.", listener, eventType.ToString()));
#else
                return;
#endif

            }
            List<IRGEventListenerBase> subscriberList = _subscribersList[eventType];
#if EVENTROUTER_THROWEXCEPTIONS
            bool listenerFound = false;
#endif
            for (int i = 0; i < subscriberList.Count; i++)
            {
                if (subscriberList[i] == listener)
                {
                    subscriberList.Remove(subscriberList[i]);
#if EVENTROUTER_THROWEXCEPTIONS
                           listenerFound = true;
#endif
                    if (subscriberList.Count == 0)
                    {
                        _subscribersList.Remove(eventType);
                    }
                    return;
                }
            }
#if EVENTROUTER_THROWEXCEPTIONS
            if (!listenerFound)
            {
                throw new ArgumentException(string.Format("Removing listener, but the supplied receiver isn't subscribed to event type \"{0}\".", eventType.ToString()));
            }
#endif

        }
        public static void TriggerEvent<RGEvent>(RGEvent newEvent) where RGEvent : struct
        {
            List<IRGEventListenerBase> list;
            if (!_subscribersList.TryGetValue(typeof(RGEvent), out list))
            {
#if EVENTROUTER_REQUIRELISTENER
                throw new ArgumentException(string.Format("Attempting to send event of type \"{0}\", but no listener for this type has been found. Make sure this.Subscribe<{0}>(EventRouter) has been called, or that all listeners to this event haven't been unsubscribed.", typeof(RGEvent).ToString()));
#else
			        return;
#endif
            }
            for (int i = 0; i < list.Count; i++)
            {
                (list[i] as IRGEventListener<RGEvent>).OnRGEvent(newEvent);
            }
        }
        private static bool SubscriptionExists(Type type, IRGEventListenerBase receiver)
        {
            List<IRGEventListenerBase> receivers;
            if (!_subscribersList.TryGetValue(type, out receivers))
            {
                return false;
            }
            bool exists = false;
            for (int i = 0; i < receivers.Count; i++)
            {
                if (receivers[i] == receiver)
                {
                    exists = true;
                    break;
                }
            }
            return exists;

        }

    }
    /// <summary>
    /// Event listener basic interface
    /// </summary>
    public interface IRGEventListenerBase { };


    /// <summary>
    /// A public interface you'll need to implement for each type of event you want to listen to.
    /// </summary>
    public interface IRGEventListener<T> : IRGEventListenerBase
    {
        void OnRGEvent(T eventType);
    }
    public static class EventRegister
    {
        public static void RGEventStartListening<EventType>(this IRGEventListener<EventType> caller) where EventType : struct
        {
            Debug.Log("EventRegister-----RGEventStartListening");
            RGEventManager.AddListener(caller);
        }
        public static void RGEventStopListening<EventType>(this IRGEventListener<EventType> caller) where EventType : struct
        {
            Debug.Log("EventRegister-----RGEventStopListening");
            RGEventManager.RemoveListener(caller);
        }
    }
}
