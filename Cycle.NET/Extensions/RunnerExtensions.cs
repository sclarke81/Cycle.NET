using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET
{
    public static class RunnerExtensions
    {
        public static IObservable<KeyValuePair<string, object>>
            CallDrivers(
            this IObservable<KeyValuePair<string, object>> sinks,
            IDictionary<string, Func<IObservable<object>, IObservable<object>>> drivers)
        {
            var splitSinks = sinks.Split(drivers.Keys);

            var splitSources = splitSinks
                .ToDictionary(p => p.Key, p => drivers[p.Key](p.Value));

            return ObservableUnion.Merge(
                splitSources);
        }

        public static IObservable<IUnion0<
            TFirstSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource>(
            this IObservable<IUnion0<
                TFirstSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver)
        {
            var firstSink = sinks.Split();

            var firstSource = firstDriver(firstSink);

            return ObservableUnion.Merge(
                firstSource);
        }

        public static IObservable<IUnion1<
            TFirstSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource>(
            this IObservable<IUnion1<
                TFirstSink>> sinks,
            Func<IObservable<Unit>, IObservable<Unit>> noneDriver,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver)
        {
            var (
                noneSink,
                firstSink) = sinks.Split();

            var noneSource = noneDriver(noneSink);
            var firstSource = firstDriver(firstSink);

            return ObservableUnion.Merge(
                noneSource,
                firstSource);
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

        public static IObservable<IUnion3<
            TFirstSource,
            TSecondSource,
            TThirdSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource>(
            this IObservable<IUnion3<
                TFirstSink,
                TSecondSink,
                TThirdSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource);
        }

        public static IObservable<IUnion4<
            TFirstSource,
            TSecondSource,
            TThirdSource,
            TFourthSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource,
            TFourthSink,
            TFourthSource>(
            this IObservable<IUnion4<
                TFirstSink,
                TSecondSink,
                TThirdSink,
                TFourthSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver,
            Func<IObservable<TFourthSink>, IObservable<TFourthSource>> fourthDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink,
                fourthSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);
            var fourthSource = fourthDriver(fourthSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource,
                fourthSource);
        }

        public static IObservable<IUnion5<
            TFirstSource,
            TSecondSource,
            TThirdSource,
            TFourthSource,
            TFifthSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource,
            TFourthSink,
            TFourthSource,
            TFifthSink,
            TFifthSource>(
            this IObservable<IUnion5<
                TFirstSink,
                TSecondSink,
                TThirdSink,
                TFourthSink,
                TFifthSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver,
            Func<IObservable<TFourthSink>, IObservable<TFourthSource>> fourthDriver,
            Func<IObservable<TFifthSink>, IObservable<TFifthSource>> fifthDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink,
                fourthSink,
                fifthSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);
            var fourthSource = fourthDriver(fourthSink);
            var fifthSource = fifthDriver(fifthSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource,
                fourthSource,
                fifthSource);
        }

        public static IObservable<IUnion6<
            TFirstSource,
            TSecondSource,
            TThirdSource,
            TFourthSource,
            TFifthSource,
            TSixthSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource,
            TFourthSink,
            TFourthSource,
            TFifthSink,
            TFifthSource,
            TSixthSink,
            TSixthSource>(
            this IObservable<IUnion6<
                TFirstSink,
                TSecondSink,
                TThirdSink,
                TFourthSink,
                TFifthSink,
                TSixthSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver,
            Func<IObservable<TFourthSink>, IObservable<TFourthSource>> fourthDriver,
            Func<IObservable<TFifthSink>, IObservable<TFifthSource>> fifthDriver,
            Func<IObservable<TSixthSink>, IObservable<TSixthSource>> sixthDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink,
                fourthSink,
                fifthSink,
                sixthSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);
            var fourthSource = fourthDriver(fourthSink);
            var fifthSource = fifthDriver(fifthSink);
            var sixthSource = sixthDriver(sixthSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource,
                fourthSource,
                fifthSource,
                sixthSource);
        }

        public static IObservable<IUnion7<
            TFirstSource,
            TSecondSource,
            TThirdSource,
            TFourthSource,
            TFifthSource,
            TSixthSource,
            TSeventhSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource,
            TFourthSink,
            TFourthSource,
            TFifthSink,
            TFifthSource,
            TSixthSink,
            TSixthSource,
            TSeventhSink,
            TSeventhSource>(
            this IObservable<IUnion7<
                TFirstSink,
                TSecondSink,
                TThirdSink,
                TFourthSink,
                TFifthSink,
                TSixthSink,
                TSeventhSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver,
            Func<IObservable<TFourthSink>, IObservable<TFourthSource>> fourthDriver,
            Func<IObservable<TFifthSink>, IObservable<TFifthSource>> fifthDriver,
            Func<IObservable<TSixthSink>, IObservable<TSixthSource>> sixthDriver,
            Func<IObservable<TSeventhSink>, IObservable<TSeventhSource>> seventhDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink,
                fourthSink,
                fifthSink,
                sixthSink,
                seventhSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);
            var fourthSource = fourthDriver(fourthSink);
            var fifthSource = fifthDriver(fifthSink);
            var sixthSource = sixthDriver(sixthSink);
            var seventhSource = seventhDriver(seventhSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource,
                fourthSource,
                fifthSource,
                sixthSource,
                seventhSource);
        }

        public static IObservable<IUnion8<
            TFirstSource,
            TSecondSource,
            TThirdSource,
            TFourthSource,
            TFifthSource,
            TSixthSource,
            TSeventhSource,
            TEighthSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource,
            TFourthSink,
            TFourthSource,
            TFifthSink,
            TFifthSource,
            TSixthSink,
            TSixthSource,
            TSeventhSink,
            TSeventhSource,
            TEighthSink,
            TEighthSource>(
            this IObservable<IUnion8<
                TFirstSink,
                TSecondSink,
                TThirdSink,
                TFourthSink,
                TFifthSink,
                TSixthSink,
                TSeventhSink,
                TEighthSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver,
            Func<IObservable<TFourthSink>, IObservable<TFourthSource>> fourthDriver,
            Func<IObservable<TFifthSink>, IObservable<TFifthSource>> fifthDriver,
            Func<IObservable<TSixthSink>, IObservable<TSixthSource>> sixthDriver,
            Func<IObservable<TSeventhSink>, IObservable<TSeventhSource>> seventhDriver,
            Func<IObservable<TEighthSink>, IObservable<TEighthSource>> eighthDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink,
                fourthSink,
                fifthSink,
                sixthSink,
                seventhSink,
                eighthSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);
            var fourthSource = fourthDriver(fourthSink);
            var fifthSource = fifthDriver(fifthSink);
            var sixthSource = sixthDriver(sixthSink);
            var seventhSource = seventhDriver(seventhSink);
            var eighthSource = eighthDriver(eighthSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource,
                fourthSource,
                fifthSource,
                sixthSource,
                seventhSource,
                eighthSource);
        }

        public static IObservable<IUnion9<
            TFirstSource,
            TSecondSource,
            TThirdSource,
            TFourthSource,
            TFifthSource,
            TSixthSource,
            TSeventhSource,
            TEighthSource,
            TNinthSource>>
            CallDrivers<
            TFirstSink,
            TFirstSource,
            TSecondSink,
            TSecondSource,
            TThirdSink,
            TThirdSource,
            TFourthSink,
            TFourthSource,
            TFifthSink,
            TFifthSource,
            TSixthSink,
            TSixthSource,
            TSeventhSink,
            TSeventhSource,
            TEighthSink,
            TEighthSource,
            TNinthSink,
            TNinthSource>(
            this IObservable<IUnion9<
                TFirstSink,
                TSecondSink,
                TThirdSink,
                TFourthSink,
                TFifthSink,
                TSixthSink,
                TSeventhSink,
                TEighthSink,
                TNinthSink>> sinks,
            Func<IObservable<TFirstSink>, IObservable<TFirstSource>> firstDriver,
            Func<IObservable<TSecondSink>, IObservable<TSecondSource>> secondDriver,
            Func<IObservable<TThirdSink>, IObservable<TThirdSource>> thirdDriver,
            Func<IObservable<TFourthSink>, IObservable<TFourthSource>> fourthDriver,
            Func<IObservable<TFifthSink>, IObservable<TFifthSource>> fifthDriver,
            Func<IObservable<TSixthSink>, IObservable<TSixthSource>> sixthDriver,
            Func<IObservable<TSeventhSink>, IObservable<TSeventhSource>> seventhDriver,
            Func<IObservable<TEighthSink>, IObservable<TEighthSource>> eighthDriver,
            Func<IObservable<TNinthSink>, IObservable<TNinthSource>> ninthDriver)
        {
            var (
                firstSink,
                secondSink,
                thirdSink,
                fourthSink,
                fifthSink,
                sixthSink,
                seventhSink,
                eighthSink,
                ninthSink) = sinks.Split();

            var firstSource = firstDriver(firstSink);
            var secondSource = secondDriver(secondSink);
            var thirdSource = thirdDriver(thirdSink);
            var fourthSource = fourthDriver(fourthSink);
            var fifthSource = fifthDriver(fifthSink);
            var sixthSource = sixthDriver(sixthSink);
            var seventhSource = seventhDriver(seventhSink);
            var eighthSource = eighthDriver(eighthSink);
            var ninthSource = ninthDriver(ninthSink);

            return ObservableUnion.Merge(
                firstSource,
                secondSource,
                thirdSource,
                fourthSource,
                fifthSource,
                sixthSource,
                seventhSource,
                eighthSource,
                ninthSource);
        }
    }
}
