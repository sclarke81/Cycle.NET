using System;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET.Extensions
{
    public static class ObservableUnionExtensions
    {
        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds)
            Split<
            TFirst,
            TSecond>(
            this IObservable<IUnion2<
                TFirst,
                TSecond>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds);
        }

        public static IObservable<IUnion2<
            TFirstSource,
            TSecondSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource>(
            this IObservable<IUnion2<
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
