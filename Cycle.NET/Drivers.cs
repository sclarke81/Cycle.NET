using System;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET
{
    public partial class Drivers
    {
        public static IObservable<IUnion2<
            TFirstSource,
            TSecondSource>>
            Call<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource>(
            IObservable<IUnion2<
                TFirstSink,
                TSecondSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver)
        {
            var (
                firstSink,
                secondSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource);
        }
    }
}
