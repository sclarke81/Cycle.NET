using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;

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

    public static class Runner<
        TSource1,
        TSink1,
        TSource2,
        TSink2>
    {
        private static IObservable<IUnion2<
            TSink1,
            TSink2>>
            CycleMain(
            IObservable<IUnion2<
                TSource1,
                TSource2>> sources,
            Func<
                Streams<
                    TSource1,
                    TSource2>,
                Streams<
                    TSink1,
                    TSink2>> main)
        {
            var sourceStreams = sources.Split();

            var sinkStreams = main(new Streams<TSource1, TSource2>(
                sourceStreams.Firsts,
                sourceStreams.Seconds));

            return ObservableUnion.Merge(
                sinkStreams.First,
                sinkStreams.Second);
        }

        public static void Run(
            Component<TSource1, TSink1, TSource2, TSink2> component,
            Drivers<TSource1, TSink1, TSource2, TSink2> drivers)
        {
            Kernel.Run(
                sources => CycleMain(sources, component.Main),
                (IObservable<IUnion2<TSink1, TSink2>> sinks) => sinks.CallDrivers(
                    drivers.OnFirst,
                    drivers.OnSecond));
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
