using System;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET.Extensions
{
    public static class StreamsExtensions
    {
        public static Streams<TFirst, TSecond> ToStreams<TFirst, TSecond>(
            this IObservable<IUnion2<TFirst, TSecond>> source)
        {
            var first = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>()));

            var second = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return));

            return new Streams<TFirst, TSecond>(
                first,
                second);
        }
    }
}
