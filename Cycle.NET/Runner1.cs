using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET
{
    public class Component<TSource1, TSink1>
    {
        public Component(
            Func<IObservable<TSource1>, IObservable<TSink1>> main)
        {
            Main = main;
        }

        public Func<IObservable<TSource1>, IObservable<TSink1>> Main { get; }
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
            var fakeSinks = new ReplaySubject<TSink1>();

            // Call the drivers and collate the returned sources.
            var sources = drivers.OnFirst(fakeSinks);

            IObservable<TSink1> sinks = component.Main(sources);

            // Update the sinks returned from main with the sinks used by the drivers.
            sinks.Subscribe(fakeSinks.OnNext);
        }
    }
}
