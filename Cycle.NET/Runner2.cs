using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET
{
    public class Streams<T1, T2>
    {
        public Streams(
            IObservable<T1> first,
            IObservable<T2> second)
        {
            First = first;
            Second = second;
        }

        public IObservable<T1> First { get; }
        public IObservable<T2> Second { get; }
    }

    public class Component<TSource1, TSink1, TSource2, TSink2>
    {
        public Component(
            Func<Streams<TSource1, TSource2>, Streams<TSink1, TSink2>> main)
        {
            Main = main;
        }

        public Func<Streams<TSource1, TSource2>, Streams<TSink1, TSink2>> Main { get; }
    }

    public class Drivers<TSource1, TSink1, TSource2, TSink2>
    {
        public Drivers(
            Func<IObservable<TSink1>, IObservable<TSource1>> onFirst,
            Func<IObservable<TSink2>, IObservable<TSource2>> onSecond)
        {
            OnFirst = onFirst;
            OnSecond = onSecond;
        }

        public Func<IObservable<TSink1>, IObservable<TSource1>> OnFirst { get; }
        public Func<IObservable<TSink2>, IObservable<TSource2>> OnSecond { get; }
    }

    public static class Runner<TSource1, TSink1, TSource2, TSink2>
    {
        public static void Run(
            Component<TSource1, TSink1, TSource2, TSink2> component,
            Drivers<TSource1, TSink1, TSource2, TSink2> drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var fakeSinks = new FakeSinks<TSink1, TSink2>(
                new ReplaySubject<TSink1>(),
                new ReplaySubject<TSink2>());

            // Call the drivers and collate the returned sources.
            var sources = new Streams<TSource1, TSource2>(
                drivers.OnFirst(fakeSinks.First),
                drivers.OnSecond(fakeSinks.Second));

            Streams<TSink1, TSink2> sinks = component.Main(sources);

            // Update the sinks returned from main with the sinks used by the drivers.
            sinks.First.Subscribe(fakeSinks.First.OnNext);
            sinks.Second.Subscribe(fakeSinks.Second.OnNext);
        }

        private class FakeSinks<T1, T2>
        {
            public FakeSinks(
                ISubject<T1> first,
                ISubject<T2> second)
            {
                First = first;
                Second = second;
            }

            public ISubject<T1> First { get; }
            public ISubject<T2> Second { get; }
        }

    }
}
