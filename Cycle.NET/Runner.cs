using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET
{
    public class Streams : Dictionary<string, IObservable<object>>
    {
        public Streams() : base() { }
        public Streams(Dictionary<string, IObservable<object>> value) : base(value) { }
    }

    public class Drivers : Dictionary<string, Func<IObservable<object>, IObservable<object>>>
    {
        public Drivers() : base() { }
    }

    public static class Runner
    {
        private static IObservable<KeyValuePair<string, object>> CycleMain(
            IObservable<KeyValuePair<string, object>> sources,
            IEnumerable<string> keys,
            Func<Streams, Streams> main)
        {
            var sourceStreams = new Streams(sources
                .Split(keys)
                .ToDictionary(p => p.Key, p => p.Value));

            var sinkStreams = main(sourceStreams);

            return ObservableUnion
                .Merge(sinkStreams);
        }

        public static void Run(Func<Streams, Streams> main, Drivers drivers)
        {
            Kernel.Run(
                sources => CycleMain(
                    sources,
                    drivers.Keys,
                    main),
                (IObservable<KeyValuePair<string, object>> sinks) => sinks.CallDrivers(drivers));
        }

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks) = main(
                            firstSources,
                            secondSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks);
                },
                (IObservable<IUnion2<
                    TSink1,
                    TSink2>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver));
    }
}
