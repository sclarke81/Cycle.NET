using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET
{
    public class Streams<T1, T2, T3>
    {
        public Streams(
            IObservable<T1> first,
            IObservable<T2> second,
            IObservable<T3> third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public IObservable<T1> First { get; }
        public IObservable<T2> Second { get; }
        public IObservable<T3> Third { get; }
    }

    public class Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>
    {
        public Drivers(
            Func<IObservable<TSink1>, IObservable<TSource1>> onFirst,
            Func<IObservable<TSink2>, IObservable<TSource2>> onSecond,
            Func<IObservable<TSink3>, IObservable<TSource3>> onThird)
        {
            OnFirst = onFirst;
            OnSecond = onSecond;
            OnThird = onThird;
        }

        public Func<IObservable<TSink1>, IObservable<TSource1>> OnFirst { get; }
        public Func<IObservable<TSink2>, IObservable<TSource2>> OnSecond { get; }
        public Func<IObservable<TSink3>, IObservable<TSource3>> OnThird { get; }
    }

    public static class Runner<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>
    {
        public static void Run(
            Func<Streams<TSource1, TSource2, TSource3>, Streams<TSink1, TSink2, TSink3>> main,
            Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var fakeSinks = new FakeSinks<TSink1, TSink2, TSink3>(
                new ReplaySubject<TSink1>(),
                new ReplaySubject<TSink2>(),
                new ReplaySubject<TSink3>());

            // Call the drivers and collate the returned sources.
            var sources = new Streams<TSource1, TSource2, TSource3>(
                drivers.OnFirst(fakeSinks.First),
                drivers.OnSecond(fakeSinks.Second),
                drivers.OnThird(fakeSinks.Third));

            Streams<TSink1, TSink2, TSink3> sinks = main(sources);

            // Update the sinks returned from main with the sinks used by the drivers.
            sinks.First.Subscribe(fakeSinks.First.OnNext);
            sinks.Second.Subscribe(fakeSinks.Second.OnNext);
            sinks.Third.Subscribe(fakeSinks.Third.OnNext);
        }

        private class FakeSinks<T1, T2, T3>
        {
            public FakeSinks(
                ISubject<T1> first,
                ISubject<T2> second,
                ISubject<T3> third)
            {
                First = first;
                Second = second;
                Third = third;
            }

            public ISubject<T1> First { get; }
            public ISubject<T2> Second { get; }
            public ISubject<T3> Third { get; }
        }

    }
}
