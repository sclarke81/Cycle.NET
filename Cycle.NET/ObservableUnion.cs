using System;
using System.Reactive;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions;
using SdgApps.Common.DotnetSealedUnions.Generic;

namespace Cycle.NET
{
    public static class ObservableUnion
    {
        public static IObservable<IUnion0<
            TFirst>>
            Merge<
            TFirst>(
            IObservable<TFirst> firsts)
        {
            var fac = GenericUnions.NulletFactory<
                TFirst>();

            var unionFirsts = firsts.Select(fac.First);

            return Observable.Merge(
                unionFirsts);
        }

        public static IObservable<IUnion1<
            TFirst>>
            Merge<
            TFirst>(
            IObservable<Unit> nones,
            IObservable<TFirst> firsts)
        {
            var fac = GenericUnions.SingletFactory<
                TFirst>();

            var unionNones = nones.Select(_ => fac.None());
            var unionFirsts = firsts.Select(fac.First);

            return Observable.Merge(
                unionNones,
                unionFirsts);
        }

        public static IObservable<IUnion2<
            TFirst,
            TSecond>>
            Merge<
            TFirst,
            TSecond>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds)
        {
            var fac = GenericUnions.DoubletFactory<
                TFirst,
                TSecond>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);

            return Observable.Merge(
                unionFirsts,
                unionSeconds);
        }

        public static IObservable<IUnion3<
            TFirst,
            TSecond,
            TThird>>
            Merge<
            TFirst,
            TSecond,
            TThird>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds)
        {
            var fac = GenericUnions.TripletFactory<
                TFirst,
                TSecond,
                TThird>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds);
        }

        public static IObservable<IUnion4<
            TFirst,
            TSecond,
            TThird,
            TFourth>>
            Merge<
            TFirst,
            TSecond,
            TThird,
            TFourth>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds,
            IObservable<TFourth> fourths)
        {
            var fac = GenericUnions.QuartetFactory<
                TFirst,
                TSecond,
                TThird,
                TFourth>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);
            var unionFourths = fourths.Select(fac.Fourth);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds,
                unionFourths);
        }

        public static IObservable<IUnion5<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth>>
            Merge<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds,
            IObservable<TFourth> fourths,
            IObservable<TFifth> fifths)
        {
            var fac = GenericUnions.QuintetFactory<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);
            var unionFourths = fourths.Select(fac.Fourth);
            var unionFifths = fifths.Select(fac.Fifth);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds,
                unionFourths,
                unionFifths);
        }

        public static IObservable<IUnion6<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth>>
            Merge<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds,
            IObservable<TFourth> fourths,
            IObservable<TFifth> fifths,
            IObservable<TSixth> sixths)
        {
            var fac = GenericUnions.SextetFactory<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);
            var unionFourths = fourths.Select(fac.Fourth);
            var unionFifths = fifths.Select(fac.Fifth);
            var unionSixths = sixths.Select(fac.Sixth);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds,
                unionFourths,
                unionFifths,
                unionSixths);
        }

        public static IObservable<IUnion7<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh>>
            Merge<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds,
            IObservable<TFourth> fourths,
            IObservable<TFifth> fifths,
            IObservable<TSixth> sixths,
            IObservable<TSeventh> sevenths)
        {
            var fac = GenericUnions.SeptetFactory<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth,
                TSeventh>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);
            var unionFourths = fourths.Select(fac.Fourth);
            var unionFifths = fifths.Select(fac.Fifth);
            var unionSixths = sixths.Select(fac.Sixth);
            var unionSevenths = sevenths.Select(fac.Seventh);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds,
                unionFourths,
                unionFifths,
                unionSixths,
                unionSevenths);
        }

        public static IObservable<IUnion8<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh,
            TEighth>>
            Merge<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh,
            TEighth>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds,
            IObservable<TFourth> fourths,
            IObservable<TFifth> fifths,
            IObservable<TSixth> sixths,
            IObservable<TSeventh> sevenths,
            IObservable<TEighth> eighths)
        {
            var fac = GenericUnions.OctetFactory<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth,
                TSeventh,
                TEighth>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);
            var unionFourths = fourths.Select(fac.Fourth);
            var unionFifths = fifths.Select(fac.Fifth);
            var unionSixths = sixths.Select(fac.Sixth);
            var unionSevenths = sevenths.Select(fac.Seventh);
            var unionEighths = eighths.Select(fac.Eighth);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds,
                unionFourths,
                unionFifths,
                unionSixths,
                unionSevenths,
                unionEighths);
        }

        public static IObservable<IUnion9<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh,
            TEighth,
            TNinth>>
            Merge<
            TFirst,
            TSecond,
            TThird,
            TFourth,
            TFifth,
            TSixth,
            TSeventh,
            TEighth,
            TNinth>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds,
            IObservable<TThird> thirds,
            IObservable<TFourth> fourths,
            IObservable<TFifth> fifths,
            IObservable<TSixth> sixths,
            IObservable<TSeventh> sevenths,
            IObservable<TEighth> eighths,
            IObservable<TNinth> ninths)
        {
            var fac = GenericUnions.NonetFactory<
                TFirst,
                TSecond,
                TThird,
                TFourth,
                TFifth,
                TSixth,
                TSeventh,
                TEighth,
                TNinth>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);
            var unionThirds = thirds.Select(fac.Third);
            var unionFourths = fourths.Select(fac.Fourth);
            var unionFifths = fifths.Select(fac.Fifth);
            var unionSixths = sixths.Select(fac.Sixth);
            var unionSevenths = sevenths.Select(fac.Seventh);
            var unionEighths = eighths.Select(fac.Eighth);
            var unionNinths = ninths.Select(fac.Ninth);

            return Observable.Merge(
                unionFirsts,
                unionSeconds,
                unionThirds,
                unionFourths,
                unionFifths,
                unionSixths,
                unionSevenths,
                unionEighths,
                unionNinths);
        }
    }
}
