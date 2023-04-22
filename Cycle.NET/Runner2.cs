using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET
{
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
                IObservable<TSource1>,
                IObservable<TSource2>,
                (
                    IObservable<TSink1>,
                    IObservable<TSink2>)> main)
        {
            var sourceStreams = sources.Split();

            var sinkStreams = main(
                sourceStreams.Firsts,
                sourceStreams.Seconds);

            return ObservableUnion.Merge(
                sinkStreams.Item1,
                sinkStreams.Item2);
        }

        public static void Run(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                (
                    IObservable<TSink1>,
                    IObservable<TSink2>)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> onFirst,
            Func<IObservable<TSink2>, IObservable<TSource2>> onSecond)
        {
            Kernel.Run(
                sources => CycleMain(sources, main),
                (IObservable<IUnion2<TSink1, TSink2>> sinks) => sinks.CallDrivers(
                    onFirst,
                    onSecond));
        }
    }
}
