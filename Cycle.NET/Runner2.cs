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
        public static void Run(
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
