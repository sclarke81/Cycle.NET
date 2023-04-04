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
    }
}
