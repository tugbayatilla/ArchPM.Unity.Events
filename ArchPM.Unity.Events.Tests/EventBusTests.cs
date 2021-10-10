using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchPM.Unity.Events.Tests
{
	public partial class Tests
	{
		[SetUp]
		public void Setup()
		{
			EventBus.Instance.Init.RegisterErrorLogAction(ex => { TestContext.Out.WriteLine(ex.Message); });
		}

		[Test]
		public void unsubscribe_all()
		{
			var event1 = new Event1();
			var event2 = new Event2();

			EventBus.Instance.Consumer.Consume<Event1>(p => { });
			EventBus.Instance.Consumer.Consume<Event2>(p => { });

			EventBus.Instance.Consumer.ActiveCount.Should().Be(2);
			EventBus.Instance.Consumer.UnsubscribeAll();
			EventBus.Instance.Consumer.ActiveCount.Should().Be(0);
		}

		[Test]
		public void When_common_one_event_published_no_consumer_to_create_memory_leak()
		{
			var badKid = new EventCreatorWithoutUnsubscribe();
			badKid = null;
			//bravo, now you have a memory leak
			Assert.Pass();
		}

		[Test]
		public void When_generic_one_event_published_one_consumer()
		{
			var event1 = new Event1();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			EventBus.Instance.Publisher.Publish(event1);

			expectedEvents.Count.Should().Be(1);

			eventConsumer1.Unsubscribe();
		}

		[Test]
		public void When_generic_one_event_published_multiple_consumers()
		{
			var event1 = new Event1();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			var eventConsumer2 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			var eventConsumer3 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));

			EventBus.Instance.Publisher.Publish(event1);

			expectedEvents.Count.Should().Be(3);

			eventConsumer1.Unsubscribe();
			eventConsumer2.Unsubscribe();
			eventConsumer3.Unsubscribe();
		}

		[Test]
		public void When_generic_multiple_events_published_one_consumer()
		{
			var event1 = new Event1();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));

			EventBus.Instance.Publisher.Publish(event1);
			EventBus.Instance.Publisher.Publish(event1);

			expectedEvents.Count.Should().Be(2);

			eventConsumer1.Unsubscribe();
		}

		[Test]
		public void When_generic_multiple_events_published_multiple_consumers()
		{
			var event1 = new Event1();
			var event2 = new Event2();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			var eventConsumer2 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			var eventConsumer3 = EventBus.Instance.Consumer.Consume<Event2>(p => expectedEvents.Add(p.Event));

			EventBus.Instance.Publisher.Publish(event1);//2
			EventBus.Instance.Publisher.Publish(event1);//2
			EventBus.Instance.Publisher.Publish(event2);//1
			EventBus.Instance.Publisher.Publish(event2);//1

			expectedEvents.Count.Should().Be(6);

			eventConsumer1.Unsubscribe();
			eventConsumer2.Unsubscribe();
			eventConsumer3.Unsubscribe();
		}

		[Test]
		public void When_generic_multiple_events_published_multiple_consumers_one_of_consumer_throws_exception()
		{
			var event1 = new Event1();
			var event2 = new Event2();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			var eventConsumer2 = EventBus.Instance.Consumer.Consume<Event1>(p => expectedEvents.Add(p.Event));
			var eventConsumer3 = EventBus.Instance.Consumer.Consume<Event1>(p => { throw new Exception("failing exception event1"); });
			var eventConsumer4 = EventBus.Instance.Consumer.Consume<Event2>(p => expectedEvents.Add(p.Event));

			EventBus.Instance.Publisher.Publish(event1);//2
			EventBus.Instance.Publisher.Publish(event2);//1

			expectedEvents.Count.Should().Be(3);
			expectedEvents.Count(p => p.GetType() == typeof(Event1)).Should().Be(2);
			expectedEvents.Count(p => p.GetType() == typeof(Event2)).Should().Be(1);

			eventConsumer1.Unsubscribe();
			eventConsumer2.Unsubscribe();
			eventConsumer3.Unsubscribe();
			eventConsumer4.Unsubscribe();
		}

		[Test]
		public void when_generic_consumer_throws_exception_ErrorLog_will_be_called()
		{
			var event1 = new Event1();
			var expectedErrorMessage = "throws my expected exception";
			string actualErrorMessage = "";
			EventBus.Instance.Init.RegisterErrorLogAction(p => { actualErrorMessage = p.Message; });

			var eventConsumerResult = EventBus.Instance.Consumer
				.Consume<Event1>(p => throw new Exception(expectedErrorMessage));

			EventBus.Instance.Publisher.Publish(event1);

			expectedErrorMessage.Should().Be(actualErrorMessage);
			EventBus.Instance.Init.RegisterErrorLogAction(null);

		}

		[Test]
		public void When_nongeneric_one_event_published_one_consumer()
		{
			var event1 = new Event1();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			EventBus.Instance.Publisher.Publish(event1);

			expectedEvents.Count.Should().Be(1);

			eventConsumer1.Unsubscribe();
		}

		[Test]
		public void When_nongeneric_one_event_published_multiple_consumers()
		{
			var event1 = new Event1();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			var eventConsumer2 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			var eventConsumer3 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));

			EventBus.Instance.Publisher.Publish(event1);

			expectedEvents.Count.Should().Be(3);

			eventConsumer1.Unsubscribe();
			eventConsumer2.Unsubscribe();
			eventConsumer3.Unsubscribe();
		}

		[Test]
		public void When_nongeneric_multiple_events_published_one_consumer()
		{
			var event1 = new Event1();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));

			EventBus.Instance.Publisher.Publish(event1);
			EventBus.Instance.Publisher.Publish(event1);

			expectedEvents.Count.Should().Be(2);

			eventConsumer1.Unsubscribe();
		}

		[Test]
		public void When_nongeneric_multiple_events_published_multiple_consumers()
		{
			var event1 = new Event1();
			var event2 = new Event2();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			var eventConsumer2 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			var eventConsumer3 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event2));

			EventBus.Instance.Publisher.Publish(event1);//2
			EventBus.Instance.Publisher.Publish(event1);//2
			EventBus.Instance.Publisher.Publish(event2);//1
			EventBus.Instance.Publisher.Publish(event2);//1

			expectedEvents.Count.Should().Be(6);

			eventConsumer1.Unsubscribe();
			eventConsumer2.Unsubscribe();
			eventConsumer3.Unsubscribe();
		}

		[Test]
		public void When_nongeneric_multiple_events_published_multiple_consumers_one_of_consumer_throws_exception()
		{
			var event1 = new Event1();
			var event2 = new Event2();
			var expectedEvents = new List<IEvent>();

			var eventConsumer1 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			var eventConsumer2 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event1));
			var eventConsumer3 = EventBus.Instance.Consumer.Consume(p => { throw new Exception("failing exception event1"); }, typeof(Event1));
			var eventConsumer4 = EventBus.Instance.Consumer.Consume(p => expectedEvents.Add(p.Event), typeof(Event2));

			EventBus.Instance.Publisher.Publish(event1);//2
			EventBus.Instance.Publisher.Publish(event2);//1

			expectedEvents.Count.Should().Be(3);
			expectedEvents.Count(p => p.GetType() == typeof(Event1)).Should().Be(2);
			expectedEvents.Count(p => p.GetType() == typeof(Event2)).Should().Be(1);

			eventConsumer1.Unsubscribe();
			eventConsumer2.Unsubscribe();
			eventConsumer3.Unsubscribe();
			eventConsumer4.Unsubscribe();
		}
		[Test]
		public void when_nongeneric_consumer_throws_exception_ErrorLog_will_be_called()
		{
			var event1 = new Event1();
			var expectedErrorMessage = "throws my expected exception";
			string actualErrorMessage = "";
			EventBus.Instance.Init.RegisterErrorLogAction(p => { actualErrorMessage = p.Message; });

			var eventConsumerResult = EventBus.Instance.Consumer
				.Consume(p => throw new Exception(expectedErrorMessage), typeof(Event1));

			EventBus.Instance.Publisher.Publish(event1);

			expectedErrorMessage.Should().Be(actualErrorMessage);
			EventBus.Instance.Init.RegisterErrorLogAction(null);
		}


		[Test]
		public void when_nongeneric_consumer_consumes_2_events_and_publishes_2_events()
		{
			var event2 = new Event2();
			var event3 = new Event3();
			var expectedEvents = new List<IEvent>();

			var eventConsumer = EventBus.Instance.Consumer
				.Consume(p => expectedEvents.Add(p.Event), typeof(Event3), (typeof(Event2)));

			EventBus.Instance.Publisher.Publish(event3);
			EventBus.Instance.Publisher.Publish(event2);

			expectedEvents.Count.Should().Be(2);
			expectedEvents.Should().NotContainNulls();
			expectedEvents.Count(p => p.GetType() == typeof(Event3)).Should().Be(1);
			expectedEvents.Count(p => p.GetType() == typeof(Event2)).Should().Be(1);

			eventConsumer.Unsubscribe();
		}


		[Test]
		public void no_publisher()
		{
			var event1 = new Event1();

			var eventConsumer = EventBus.Instance.Consumer
					.Consume<Event1>(p => { });
			eventConsumer.Unsubscribe();

			Assert.Pass();
		}

		[Test]
		public void Check_creationTime()
		{
			var event1 = new Event1();
			DateTime before = DateTime.Now;
			DateTime actual = DateTime.Now;

			var eventConsumer = EventBus.Instance.Consumer
					.Consume<Event1>(p => { actual = p.CreationTime; });
			DateTime after = DateTime.Now;

			eventConsumer.Unsubscribe();

			actual.Should().BeAfter(before);
			actual.Should().BeBefore(after);
		}

		[Test]
		public void Event_convertion()
		{
			DateTime justBefore = DateTime.Now;
			var event1 = new Event1();
			EventMessage<Event1> actual = null;

			var eventConsumer = EventBus.Instance.Consumer
					.Consume<Event1>(p => { actual = p; });

			EventBus.Instance.Publisher.Publish(event1);

			eventConsumer.Unsubscribe();

			actual.Event.Should().Be(event1);
			actual.EventType.Should().Be(typeof(Event1));
			actual.CreationTime.Should().BeBefore(DateTime.Now);
			actual.CreationTime.Should().BeAfter(justBefore);
		}


	}
}