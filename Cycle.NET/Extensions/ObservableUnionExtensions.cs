using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET.Extensions
{
    public static class ObservableUnionExtensions
    {
        public static
            IDictionary<string, IObservable<object>>
            Split(
            this IObservable<KeyValuePair<string, object>> source,
            IEnumerable<string> keys) =>
            keys
            .Select(k => new KeyValuePair<string, IObservable<object>>(k, source
            .Where(p => p.Key == k)
            .Select(p => p.Value)))
            .ToDictionary(p => p.Key, p => p.Value);

        public static
            IObservable<TFirst>
            Split<
            TFirst>(
            this IObservable<IUnion0<
                TFirst>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return));

            return firsts;
        }

        public static (
            IObservable<Unit> Nones,
            IObservable<TFirst> Firsts)
            Split<
            TFirst>(
            this IObservable<IUnion1<
                TFirst>> source)
        {
            var nones = source.SelectMany(s => s.Join(
                mapNone: () => Observable.Return(Unit.Default),
                mapFirst: _ => Observable.Empty<Unit>()));

            var firsts = source.SelectMany(s => s.Join(
                mapNone: Observable.Empty<TFirst>,
                mapFirst: Observable.Return));

            return (
                Nones: nones,
                Firsts: firsts);
        }

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

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds)
            Split<
            TFirst,
            TSecond,
            TThird>(
            this IObservable<IUnion3<
                TFirst,
                TSecond,
                TThird>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds);
        }

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds,
            IObservable<TFourth> Fourths)
            Split<
            TFirst,
            TSecond,
            TThird,
            TFourth>(
            this IObservable<IUnion4<
                TFirst,
                TSecond,
                TThird,
                TFourth>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>(),
                mapFourth: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>(),
                mapFourth: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return,
                mapFourth: _ => Observable.Empty<TThird>()));

            var fourths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFourth>(),
                mapSecond: _ => Observable.Empty<TFourth>(),
                mapThird: _ => Observable.Empty<TFourth>(),
                mapFourth: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds,
                Fourths: fourths);
        }

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds,
            IObservable<TFourth> Fourths,
            IObservable<TFifth> Fifths)
            Split<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth>(
            this IObservable<IUnion5<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>(),
                mapFourth: _ => Observable.Empty<TFirst>(),
                mapFifth: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>(),
                mapFourth: _ => Observable.Empty<TSecond>(),
                mapFifth: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return,
                mapFourth: _ => Observable.Empty<TThird>(),
                mapFifth: _ => Observable.Empty<TThird>()));

            var fourths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFourth>(),
                mapSecond: _ => Observable.Empty<TFourth>(),
                mapThird: _ => Observable.Empty<TFourth>(),
                mapFourth: Observable.Return,
                mapFifth: _ => Observable.Empty<TFourth>()));

            var fifths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFifth>(),
                mapSecond: _ => Observable.Empty<TFifth>(),
                mapThird: _ => Observable.Empty<TFifth>(),
                mapFourth: _ => Observable.Empty<TFifth>(),
                mapFifth: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds,
                Fourths: fourths,
                Fifths: fifths);
        }

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds,
            IObservable<TFourth> Fourths,
            IObservable<TFifth> Fifths,
            IObservable<TSixth> Sixths)
            Split<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth>(
            this IObservable<IUnion6<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>(),
                mapFourth: _ => Observable.Empty<TFirst>(),
                mapFifth: _ => Observable.Empty<TFirst>(),
                mapSixth: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>(),
                mapFourth: _ => Observable.Empty<TSecond>(),
                mapFifth: _ => Observable.Empty<TSecond>(),
                mapSixth: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return,
                mapFourth: _ => Observable.Empty<TThird>(),
                mapFifth: _ => Observable.Empty<TThird>(),
                mapSixth: _ => Observable.Empty<TThird>()));

            var fourths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFourth>(),
                mapSecond: _ => Observable.Empty<TFourth>(),
                mapThird: _ => Observable.Empty<TFourth>(),
                mapFourth: Observable.Return,
                mapFifth: _ => Observable.Empty<TFourth>(),
                mapSixth: _ => Observable.Empty<TFourth>()));

            var fifths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFifth>(),
                mapSecond: _ => Observable.Empty<TFifth>(),
                mapThird: _ => Observable.Empty<TFifth>(),
                mapFourth: _ => Observable.Empty<TFifth>(),
                mapFifth: Observable.Return,
                mapSixth: _ => Observable.Empty<TFifth>()));

            var sixths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSixth>(),
                mapSecond: _ => Observable.Empty<TSixth>(),
                mapThird: _ => Observable.Empty<TSixth>(),
                mapFourth: _ => Observable.Empty<TSixth>(),
                mapFifth: _ => Observable.Empty<TSixth>(),
                mapSixth: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds,
                Fourths: fourths,
                Fifths: fifths,
                Sixths: sixths);
        }

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds,
            IObservable<TFourth> Fourths,
            IObservable<TFifth> Fifths,
            IObservable<TSixth> Sixths,
            IObservable<TSeventh> Sevenths)
            Split<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh>(
            this IObservable<IUnion7<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth,
                TSeventh>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>(),
                mapFourth: _ => Observable.Empty<TFirst>(),
                mapFifth: _ => Observable.Empty<TFirst>(),
                mapSixth: _ => Observable.Empty<TFirst>(),
                mapSeventh: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>(),
                mapFourth: _ => Observable.Empty<TSecond>(),
                mapFifth: _ => Observable.Empty<TSecond>(),
                mapSixth: _ => Observable.Empty<TSecond>(),
                mapSeventh: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return,
                mapFourth: _ => Observable.Empty<TThird>(),
                mapFifth: _ => Observable.Empty<TThird>(),
                mapSixth: _ => Observable.Empty<TThird>(),
                mapSeventh: _ => Observable.Empty<TThird>()));

            var fourths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFourth>(),
                mapSecond: _ => Observable.Empty<TFourth>(),
                mapThird: _ => Observable.Empty<TFourth>(),
                mapFourth: Observable.Return,
                mapFifth: _ => Observable.Empty<TFourth>(),
                mapSixth: _ => Observable.Empty<TFourth>(),
                mapSeventh: _ => Observable.Empty<TFourth>()));

            var fifths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFifth>(),
                mapSecond: _ => Observable.Empty<TFifth>(),
                mapThird: _ => Observable.Empty<TFifth>(),
                mapFourth: _ => Observable.Empty<TFifth>(),
                mapFifth: Observable.Return,
                mapSixth: _ => Observable.Empty<TFifth>(),
                mapSeventh: _ => Observable.Empty<TFifth>()));

            var sixths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSixth>(),
                mapSecond: _ => Observable.Empty<TSixth>(),
                mapThird: _ => Observable.Empty<TSixth>(),
                mapFourth: _ => Observable.Empty<TSixth>(),
                mapFifth: _ => Observable.Empty<TSixth>(),
                mapSixth: Observable.Return,
                mapSeventh: _ => Observable.Empty<TSixth>()));

            var sevenths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSeventh>(),
                mapSecond: _ => Observable.Empty<TSeventh>(),
                mapThird: _ => Observable.Empty<TSeventh>(),
                mapFourth: _ => Observable.Empty<TSeventh>(),
                mapFifth: _ => Observable.Empty<TSeventh>(),
                mapSixth: _ => Observable.Empty<TSeventh>(),
                mapSeventh: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds,
                Fourths: fourths,
                Fifths: fifths,
                Sixths: sixths,
                Sevenths: sevenths);
        }

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds,
            IObservable<TFourth> Fourths,
            IObservable<TFifth> Fifths,
            IObservable<TSixth> Sixths,
            IObservable<TSeventh> Sevenths,
            IObservable<TEighth> Eighths)
            Split<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh,
            TEighth>(
            this IObservable<IUnion8<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth,
                TSeventh,
                TEighth>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>(),
                mapFourth: _ => Observable.Empty<TFirst>(),
                mapFifth: _ => Observable.Empty<TFirst>(),
                mapSixth: _ => Observable.Empty<TFirst>(),
                mapSeventh: _ => Observable.Empty<TFirst>(),
                mapEighth: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>(),
                mapFourth: _ => Observable.Empty<TSecond>(),
                mapFifth: _ => Observable.Empty<TSecond>(),
                mapSixth: _ => Observable.Empty<TSecond>(),
                mapSeventh: _ => Observable.Empty<TSecond>(),
                mapEighth: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return,
                mapFourth: _ => Observable.Empty<TThird>(),
                mapFifth: _ => Observable.Empty<TThird>(),
                mapSixth: _ => Observable.Empty<TThird>(),
                mapSeventh: _ => Observable.Empty<TThird>(),
                mapEighth: _ => Observable.Empty<TThird>()));

            var fourths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFourth>(),
                mapSecond: _ => Observable.Empty<TFourth>(),
                mapThird: _ => Observable.Empty<TFourth>(),
                mapFourth: Observable.Return,
                mapFifth: _ => Observable.Empty<TFourth>(),
                mapSixth: _ => Observable.Empty<TFourth>(),
                mapSeventh: _ => Observable.Empty<TFourth>(),
                mapEighth: _ => Observable.Empty<TFourth>()));

            var fifths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFifth>(),
                mapSecond: _ => Observable.Empty<TFifth>(),
                mapThird: _ => Observable.Empty<TFifth>(),
                mapFourth: _ => Observable.Empty<TFifth>(),
                mapFifth: Observable.Return,
                mapSixth: _ => Observable.Empty<TFifth>(),
                mapSeventh: _ => Observable.Empty<TFifth>(),
                mapEighth: _ => Observable.Empty<TFifth>()));

            var sixths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSixth>(),
                mapSecond: _ => Observable.Empty<TSixth>(),
                mapThird: _ => Observable.Empty<TSixth>(),
                mapFourth: _ => Observable.Empty<TSixth>(),
                mapFifth: _ => Observable.Empty<TSixth>(),
                mapSixth: Observable.Return,
                mapSeventh: _ => Observable.Empty<TSixth>(),
                mapEighth: _ => Observable.Empty<TSixth>()));

            var sevenths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSeventh>(),
                mapSecond: _ => Observable.Empty<TSeventh>(),
                mapThird: _ => Observable.Empty<TSeventh>(),
                mapFourth: _ => Observable.Empty<TSeventh>(),
                mapFifth: _ => Observable.Empty<TSeventh>(),
                mapSixth: _ => Observable.Empty<TSeventh>(),
                mapSeventh: Observable.Return,
                mapEighth: _ => Observable.Empty<TSeventh>()));

            var eighths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TEighth>(),
                mapSecond: _ => Observable.Empty<TEighth>(),
                mapThird: _ => Observable.Empty<TEighth>(),
                mapFourth: _ => Observable.Empty<TEighth>(),
                mapFifth: _ => Observable.Empty<TEighth>(),
                mapSixth: _ => Observable.Empty<TEighth>(),
                mapSeventh: _ => Observable.Empty<TEighth>(),
                mapEighth: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds,
                Fourths: fourths,
                Fifths: fifths,
                Sixths: sixths,
                Sevenths: sevenths,
                Eighths: eighths);
        }

        public static (
            IObservable<TFirst> Firsts,
            IObservable<TSecond> Seconds,
            IObservable<TThird> Thirds,
            IObservable<TFourth> Fourths,
            IObservable<TFifth> Fifths,
            IObservable<TSixth> Sixths,
            IObservable<TSeventh> Sevenths,
            IObservable<TEighth> Eighths,
            IObservable<TNinth> Ninths)
            Split<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh,
            TEighth,
            TNinth>(
            this IObservable<IUnion9<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth,
                TSeventh,
                TEighth,
                TNinth>> source)
        {
            var firsts = source.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<TFirst>(),
                mapThird: _ => Observable.Empty<TFirst>(),
                mapFourth: _ => Observable.Empty<TFirst>(),
                mapFifth: _ => Observable.Empty<TFirst>(),
                mapSixth: _ => Observable.Empty<TFirst>(),
                mapSeventh: _ => Observable.Empty<TFirst>(),
                mapEighth: _ => Observable.Empty<TFirst>(),
                mapNinth: _ => Observable.Empty<TFirst>()));

            var seconds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSecond>(),
                mapSecond: Observable.Return,
                mapThird: _ => Observable.Empty<TSecond>(),
                mapFourth: _ => Observable.Empty<TSecond>(),
                mapFifth: _ => Observable.Empty<TSecond>(),
                mapSixth: _ => Observable.Empty<TSecond>(),
                mapSeventh: _ => Observable.Empty<TSecond>(),
                mapEighth: _ => Observable.Empty<TSecond>(),
                mapNinth: _ => Observable.Empty<TSecond>()));

            var thirds = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TThird>(),
                mapSecond: _ => Observable.Empty<TThird>(),
                mapThird: Observable.Return,
                mapFourth: _ => Observable.Empty<TThird>(),
                mapFifth: _ => Observable.Empty<TThird>(),
                mapSixth: _ => Observable.Empty<TThird>(),
                mapSeventh: _ => Observable.Empty<TThird>(),
                mapEighth: _ => Observable.Empty<TThird>(),
                mapNinth: _ => Observable.Empty<TThird>()));

            var fourths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFourth>(),
                mapSecond: _ => Observable.Empty<TFourth>(),
                mapThird: _ => Observable.Empty<TFourth>(),
                mapFourth: Observable.Return,
                mapFifth: _ => Observable.Empty<TFourth>(),
                mapSixth: _ => Observable.Empty<TFourth>(),
                mapSeventh: _ => Observable.Empty<TFourth>(),
                mapEighth: _ => Observable.Empty<TFourth>(),
                mapNinth: _ => Observable.Empty<TFourth>()));

            var fifths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TFifth>(),
                mapSecond: _ => Observable.Empty<TFifth>(),
                mapThird: _ => Observable.Empty<TFifth>(),
                mapFourth: _ => Observable.Empty<TFifth>(),
                mapFifth: Observable.Return,
                mapSixth: _ => Observable.Empty<TFifth>(),
                mapSeventh: _ => Observable.Empty<TFifth>(),
                mapEighth: _ => Observable.Empty<TFifth>(),
                mapNinth: _ => Observable.Empty<TFifth>()));

            var sixths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSixth>(),
                mapSecond: _ => Observable.Empty<TSixth>(),
                mapThird: _ => Observable.Empty<TSixth>(),
                mapFourth: _ => Observable.Empty<TSixth>(),
                mapFifth: _ => Observable.Empty<TSixth>(),
                mapSixth: Observable.Return,
                mapSeventh: _ => Observable.Empty<TSixth>(),
                mapEighth: _ => Observable.Empty<TSixth>(),
                mapNinth: _ => Observable.Empty<TSixth>()));

            var sevenths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TSeventh>(),
                mapSecond: _ => Observable.Empty<TSeventh>(),
                mapThird: _ => Observable.Empty<TSeventh>(),
                mapFourth: _ => Observable.Empty<TSeventh>(),
                mapFifth: _ => Observable.Empty<TSeventh>(),
                mapSixth: _ => Observable.Empty<TSeventh>(),
                mapSeventh: Observable.Return,
                mapEighth: _ => Observable.Empty<TSeventh>(),
                mapNinth: _ => Observable.Empty<TSeventh>()));

            var eighths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TEighth>(),
                mapSecond: _ => Observable.Empty<TEighth>(),
                mapThird: _ => Observable.Empty<TEighth>(),
                mapFourth: _ => Observable.Empty<TEighth>(),
                mapFifth: _ => Observable.Empty<TEighth>(),
                mapSixth: _ => Observable.Empty<TEighth>(),
                mapSeventh: _ => Observable.Empty<TEighth>(),
                mapEighth: Observable.Return,
                mapNinth: _ => Observable.Empty<TEighth>()));

            var ninths = source.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<TNinth>(),
                mapSecond: _ => Observable.Empty<TNinth>(),
                mapThird: _ => Observable.Empty<TNinth>(),
                mapFourth: _ => Observable.Empty<TNinth>(),
                mapFifth: _ => Observable.Empty<TNinth>(),
                mapSixth: _ => Observable.Empty<TNinth>(),
                mapSeventh: _ => Observable.Empty<TNinth>(),
                mapEighth: _ => Observable.Empty<TNinth>(),
                mapNinth: Observable.Return));

            return (
                Firsts: firsts,
                Seconds: seconds,
                Thirds: thirds,
                Fourths: fourths,
                Fifths: fifths,
                Sixths: sixths,
                Sevenths: sevenths,
                Eighths: eighths,
                Ninths: ninths);
        }
    }
}
