using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;
using System.Runtime.Serialization;

namespace Cycle.NET
{
    [Serializable]
    public class Streams : Dictionary<string, IObservable<object>>
    {
        public Streams() : base() { }

        public Streams(Dictionary<string, IObservable<object>> value) : base(value) { }

        protected Streams(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class Drivers : Dictionary<string, Func<IObservable<object>, IObservable<object>>>
    {
        public Drivers() : base() { }

        protected Drivers(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }

    public static class Runner
    {
        public static void Run(Func<Streams, Streams> main, Drivers drivers) =>
            Kernel.Run(
                sources =>
                {
                    var sourceStreams = new Streams(sources
                        .Split(drivers.Keys)
                        .ToDictionary(p => p.Key, p => p.Value));

                    var sinkStreams = main(sourceStreams);

                    return ObservableUnion
                        .Merge(sinkStreams);
                },
                (IObservable<KeyValuePair<string, object>> sinks) => sinks.CallDrivers(drivers));

        public static void Run<
            TSource1,
            TSink1>(
            Func<
                IObservable<TSource1>,
                IObservable<TSink1>> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver) =>
            Kernel.Run(
                sources =>
                {
                    var firstSources = sources.Split();

                    var firstSinks = main(
                        firstSources);

                    return ObservableUnion.Merge(
                        firstSinks);
                },
                (IObservable<IUnion0<
                    TSink1>> sinks) => sinks.CallDrivers(
                    firstDriver));

        public static void Run<
            TSource1,
            TSink1>(
            Func<
                IObservable<Unit>,
                IObservable<TSource1>,
                (
                    IObservable<Unit> NoneSinks,
                    IObservable<TSink1> FirstSinks)> main,
            Func<IObservable<Unit>, IObservable<Unit>> noneDriver,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        noneSources,
                        firstSources) = sources.Split();

                    var (
                        noneSinks,
                        firstSinks) = main(
                            noneSources,
                            firstSources);

                    return ObservableUnion.Merge(
                        noneSinks,
                        firstSinks);
                },
                (IObservable<IUnion1<
                    TSink1>> sinks) => sinks.CallDrivers(
                    noneDriver,
                    firstDriver));

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

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks);
                },
                (IObservable<IUnion3<
                    TSink1,
                    TSink2,
                    TSink3>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver));

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3,
            TSource4,
            TSink4>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                IObservable<TSource4>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks,
                    IObservable<TSink4> FourthSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver,
            Func<IObservable<TSink4>, IObservable<TSource4>> fourthDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources,
                        fourthSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources,
                            fourthSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks);
                },
                (IObservable<IUnion4<
                    TSink1,
                    TSink2,
                    TSink3,
                    TSink4>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver,
                    fourthDriver));

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3,
            TSource4,
            TSink4,
            TSource5,
            TSink5>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                IObservable<TSource4>,
                IObservable<TSource5>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks,
                    IObservable<TSink4> FourthSinks,
                    IObservable<TSink5> FifthSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver,
            Func<IObservable<TSink4>, IObservable<TSource4>> fourthDriver,
            Func<IObservable<TSink5>, IObservable<TSource5>> fifthDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources,
                        fourthSources,
                        fifthSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources,
                            fourthSources,
                            fifthSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks);
                },
                (IObservable<IUnion5<
                    TSink1,
                    TSink2,
                    TSink3,
                    TSink4,
                    TSink5>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver,
                    fourthDriver,
                    fifthDriver));

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3,
            TSource4,
            TSink4,
            TSource5,
            TSink5,
            TSource6,
            TSink6>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                IObservable<TSource4>,
                IObservable<TSource5>,
                IObservable<TSource6>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks,
                    IObservable<TSink4> FourthSinks,
                    IObservable<TSink5> FifthSinks,
                    IObservable<TSink6> SixthSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver,
            Func<IObservable<TSink4>, IObservable<TSource4>> fourthDriver,
            Func<IObservable<TSink5>, IObservable<TSource5>> fifthDriver,
            Func<IObservable<TSink6>, IObservable<TSource6>> sixthDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources,
                        fourthSources,
                        fifthSources,
                        sixthSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources,
                            fourthSources,
                            fifthSources,
                            sixthSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks);
                },
                (IObservable<IUnion6<
                    TSink1,
                    TSink2,
                    TSink3,
                    TSink4,
                    TSink5,
                    TSink6>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver,
                    fourthDriver,
                    fifthDriver,
                    sixthDriver));

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3,
            TSource4,
            TSink4,
            TSource5,
            TSink5,
            TSource6,
            TSink6,
            TSource7,
            TSink7>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                IObservable<TSource4>,
                IObservable<TSource5>,
                IObservable<TSource6>,
                IObservable<TSource7>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks,
                    IObservable<TSink4> FourthSinks,
                    IObservable<TSink5> FifthSinks,
                    IObservable<TSink6> SixthSinks,
                    IObservable<TSink7> SeventhSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver,
            Func<IObservable<TSink4>, IObservable<TSource4>> fourthDriver,
            Func<IObservable<TSink5>, IObservable<TSource5>> fifthDriver,
            Func<IObservable<TSink6>, IObservable<TSource6>> sixthDriver,
            Func<IObservable<TSink7>, IObservable<TSource7>> seventhDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources,
                        fourthSources,
                        fifthSources,
                        sixthSources,
                        seventhSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks,
                        seventhSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources,
                            fourthSources,
                            fifthSources,
                            sixthSources,
                            seventhSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks,
                        seventhSinks);
                },
                (IObservable<IUnion7<
                    TSink1,
                    TSink2,
                    TSink3,
                    TSink4,
                    TSink5,
                    TSink6,
                    TSink7>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver,
                    fourthDriver,
                    fifthDriver,
                    sixthDriver,
                    seventhDriver));

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3,
            TSource4,
            TSink4,
            TSource5,
            TSink5,
            TSource6,
            TSink6,
            TSource7,
            TSink7,
            TSource8,
            TSink8>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                IObservable<TSource4>,
                IObservable<TSource5>,
                IObservable<TSource6>,
                IObservable<TSource7>,
                IObservable<TSource8>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks,
                    IObservable<TSink4> FourthSinks,
                    IObservable<TSink5> FifthSinks,
                    IObservable<TSink6> SixthSinks,
                    IObservable<TSink7> SeventhSinks,
                    IObservable<TSink8> EighthSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver,
            Func<IObservable<TSink4>, IObservable<TSource4>> fourthDriver,
            Func<IObservable<TSink5>, IObservable<TSource5>> fifthDriver,
            Func<IObservable<TSink6>, IObservable<TSource6>> sixthDriver,
            Func<IObservable<TSink7>, IObservable<TSource7>> seventhDriver,
            Func<IObservable<TSink8>, IObservable<TSource8>> eighthDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources,
                        fourthSources,
                        fifthSources,
                        sixthSources,
                        seventhSources,
                        eighthSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks,
                        seventhSinks,
                        eighthSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources,
                            fourthSources,
                            fifthSources,
                            sixthSources,
                            seventhSources,
                            eighthSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks,
                        seventhSinks,
                        eighthSinks);
                },
                (IObservable<IUnion8<
                    TSink1,
                    TSink2,
                    TSink3,
                    TSink4,
                    TSink5,
                    TSink6,
                    TSink7,
                    TSink8>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver,
                    fourthDriver,
                    fifthDriver,
                    sixthDriver,
                    seventhDriver,
                    eighthDriver));

        public static void Run<
            TSource1,
            TSink1,
            TSource2,
            TSink2,
            TSource3,
            TSink3,
            TSource4,
            TSink4,
            TSource5,
            TSink5,
            TSource6,
            TSink6,
            TSource7,
            TSink7,
            TSource8,
            TSink8,
            TSource9,
            TSink9>(
            Func<
                IObservable<TSource1>,
                IObservable<TSource2>,
                IObservable<TSource3>,
                IObservable<TSource4>,
                IObservable<TSource5>,
                IObservable<TSource6>,
                IObservable<TSource7>,
                IObservable<TSource8>,
                IObservable<TSource9>,
                (
                    IObservable<TSink1> FirstSinks,
                    IObservable<TSink2> SecondSinks,
                    IObservable<TSink3> ThirdSinks,
                    IObservable<TSink4> FourthSinks,
                    IObservable<TSink5> FifthSinks,
                    IObservable<TSink6> SixthSinks,
                    IObservable<TSink7> SeventhSinks,
                    IObservable<TSink8> EighthSinks,
                    IObservable<TSink9> NinthSinks)> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> firstDriver,
            Func<IObservable<TSink2>, IObservable<TSource2>> secondDriver,
            Func<IObservable<TSink3>, IObservable<TSource3>> thirdDriver,
            Func<IObservable<TSink4>, IObservable<TSource4>> fourthDriver,
            Func<IObservable<TSink5>, IObservable<TSource5>> fifthDriver,
            Func<IObservable<TSink6>, IObservable<TSource6>> sixthDriver,
            Func<IObservable<TSink7>, IObservable<TSource7>> seventhDriver,
            Func<IObservable<TSink8>, IObservable<TSource8>> eighthDriver,
            Func<IObservable<TSink9>, IObservable<TSource9>> ninthDriver) =>
            Kernel.Run(
                sources =>
                {
                    var (
                        firstSources,
                        secondSources,
                        thirdSources,
                        fourthSources,
                        fifthSources,
                        sixthSources,
                        seventhSources,
                        eighthSources,
                        ninthSources) = sources.Split();

                    var (
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks,
                        seventhSinks,
                        eighthSinks,
                        ninthSinks) = main(
                            firstSources,
                            secondSources,
                            thirdSources,
                            fourthSources,
                            fifthSources,
                            sixthSources,
                            seventhSources,
                            eighthSources,
                            ninthSources);

                    return ObservableUnion.Merge(
                        firstSinks,
                        secondSinks,
                        thirdSinks,
                        fourthSinks,
                        fifthSinks,
                        sixthSinks,
                        seventhSinks,
                        eighthSinks,
                        ninthSinks);
                },
                (IObservable<IUnion9<
                    TSink1,
                    TSink2,
                    TSink3,
                    TSink4,
                    TSink5,
                    TSink6,
                    TSink7,
                    TSink8,
                    TSink9>> sinks) => sinks.CallDrivers(
                    firstDriver,
                    secondDriver,
                    thirdDriver,
                    fourthDriver,
                    fifthDriver,
                    sixthDriver,
                    seventhDriver,
                    eighthDriver,
                    ninthDriver));
    }
}
