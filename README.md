# ArchPM.Unity.Events

## The drivers to write such a framework item.

- I am learning Unity for fun and I am watching youtube videos.
- I saw that everybody is kinda struggling while given or creating examples.
- It easly can be shown the bifferences between Singleton and Observer patterns.
- I said to myself, don't struggle, use this :)

---

## Next thing to do

- I will create an Unity example with walls and doors example soon. 
- The plan is in October 2021.

---

## Benefit of Using this approach

- You don't have to create 1000 methods to handle events.
- Loosly coupled.
- Single publishing point, single consuming point.
- Easy to focus on process instead of code structure.
- it's open source, you can improve if you like.

--- 
## How to use

### Step 1/3: Creating Event Class
```c#
using ArchPM.Unity.Events;

// create a event class that transfers the data you need.
// the class should implement IEvent interface
public class Event1 : IEvent
{
}

// you can create Event classes as much as you want and use them.
```
### Step 2/3: Publishing 

```c#
using ArchPM.Unity.Events;

// create an instace of event class
var event1 = new Event1();

// just call static publish method
EventBus.Instance.Publisher.Publish(event1);

// now your event is published. if there are any consumers, they will get this information.

```

### Step 3/3: Consuming

```c#
using ArchPM.Unity.Events;

// create a method to handle published event
private void Event1Handler(EventMessage<Event1> eventMessage){
    // you will get the eventMessage which is a wrapper of Event1 event class.
    // every time when the Event1 published, you will get this information.
} 

// Call Consume method and set the Event1Handler
// you can put this in Start method
IConsumerResult event1ConsumerResult 
= EventBus.Instance.Consumer.Consume<Event1>(Event1Handler);

// to Unsubscribe, in OnDestroy method
event1ConsumerResult.Unsubscribe();

```

### Step 3/3: Consuming / Another approach

```c#
using ArchPM.Unity.Events;

// Call Consume method and set the Event1Handler
// you can put this in Start method
IConsumerResult event1ConsumerResult 
= EventBus.Instance.Consumer.Consume<Event1>(eventMessage => {
    // you will get the eventMessage which is a wrapper of Event1 event class.
    // every time when the Event1 published, you will get this information.
});

// to Unsubscribe, in OnDestroy method
event1ConsumerResult.Unsubscribe();

```

---

## Other Functionalities 
### Unsubscribe All 
```c#
using ArchPM.Unity.Events;

EventBus.Instance.Consumer.UnsubscribeAll();

```

### Getting Error Logs
```c#
using ArchPM.Unity.Events;

// when the consumer gets an exception, than you can get the error message in Unity Console or wherever you want to add the exception.
EventBus.Instance.Init
.RegisterErrorLogAction(ex=>{ Debug.LogException(ex); });

```