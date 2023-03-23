using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET
{
    public class Streams<T1>
    {
        public Streams(
            IObservable<T1> first)
        {
            First = first;
        }

        public IObservable<T1> First { get; }
    }

    public class Component<TSource1, TSink1>
    {
        public Component(
            Func<Streams<TSource1>, Streams<TSink1>> main)
        {
            Main = main;
        }

        public Func<Streams<TSource1>, Streams<TSink1>> Main { get; }
    }

    public class Drivers<TSource1, TSink1>
    {
        public Drivers(
            Func<IObservable<TSink1>, IObservable<TSource1>> onFirst)
        {
            OnFirst = onFirst;
        }

        public Func<IObservable<TSink1>, IObservable<TSource1>> OnFirst { get; }
    }

    public static class Runner<TSource1, TSink1>
    {
        public static void Run(
            Component<TSource1, TSink1> component,
            Drivers<TSource1, TSink1> drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var fakeSinks = new FakeSinks<TSink1>(
                new ReplaySubject<TSink1>());

            // Call the drivers and collate the returned sources.
            var sources = new Streams<TSource1>(
                drivers.OnFirst(fakeSinks.First));

            Streams<TSink1> sinks = component.Main(sources);

            // Update the sinks returned from main with the sinks used by the drivers.
            sinks.First.Subscribe(fakeSinks.First.OnNext);
        }

        private class FakeSinks<T1>
        {
            public FakeSinks(
                ISubject<T1> first)
            {
                First = first;
            }

            public ISubject<T1> First { get; }
        }

    }
}
