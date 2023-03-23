using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET.Demo
{
    public class Streams<T1, T2, T3>
    {
        public Streams(
            IObservable<T1> first,
            IObservable<T2> second,
            IObservable<T3> third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public IObservable<T1> First { get; }
        public IObservable<T2> Second { get; }
        public IObservable<T3> Third { get; }
    }

    public class Component<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> : IComponent
    {
        public Component(
            FakeSinks<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> fakeSinks,
            IObservable<TSource1> first,
            IObservable<TSource2> second,
            IObservable<TSource3> third)
        {
            FakeSinks = fakeSinks;
            First = first;
            Second = second;
            Third = third;
        }

        public FakeSinks<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> FakeSinks { get; }
        public IObservable<TSource1> First { get; }
        public IObservable<TSource2> Second { get; }
        public IObservable<TSource3> Third { get; }

        public ISinks Main()
        {
            var sources = new Streams<TSource1, TSource2, TSource3>(First, Second, Third);
            var sinks = FakeSinks.Drivers.Main(sources);
            return new Sinks<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>(
                this,
                sinks.First,
                sinks.Second,
                sinks.Third);
        }
    }

    internal class Sinks<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> : ISinks
    {
        public Sinks(Component<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> component, IObservable<TSink1> first, IObservable<TSink2> second, IObservable<TSink3> third)
        {
            Component = component;
            First = first;
            Second = second;
            Third = third;
        }

        public Component<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> Component { get; }
        public IObservable<TSink1> First { get; }
        public IObservable<TSink2> Second { get; }
        public IObservable<TSink3> Third { get; }

        public void Subscribe()
        {
            First.Subscribe(Component.FakeSinks.First.OnNext);
            Second.Subscribe(Component.FakeSinks.Second.OnNext);
            Third.Subscribe(Component.FakeSinks.Third.OnNext);
        }
    }

    public class Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> : IDrivers
    {
        public Drivers(
            Func<Streams<TSource1, TSource2, TSource3>, Streams<TSink1, TSink2, TSink3>> main,
            Func<IObservable<TSink1>, IObservable<TSource1>> onFirst,
            Func<IObservable<TSink2>, IObservable<TSource2>> onSecond,
            Func<IObservable<TSink3>, IObservable<TSource3>> onThird)
        {
            Main = main;
            OnFirst = onFirst;
            OnSecond = onSecond;
            OnThird = onThird;
        }

        public Func<Streams<TSource1, TSource2, TSource3>, Streams<TSink1, TSink2, TSink3>> Main { get; }
        public Func<IObservable<TSink1>, IObservable<TSource1>> OnFirst { get; }
        public Func<IObservable<TSink2>, IObservable<TSource2>> OnSecond { get; }
        public Func<IObservable<TSink3>, IObservable<TSource3>> OnThird { get; }

        public IFakeSinks CreateFakeSinks()
        {
            return new FakeSinks<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>(
                this,
                new ReplaySubject<TSink1>(),
                new ReplaySubject<TSink2>(),
                new ReplaySubject<TSink3>());
        }
    }

    public class FakeSinks<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> : IFakeSinks
    {
        public FakeSinks(
            Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> drivers,
            ISubject<TSink1> first,
            ISubject<TSink2> second,
            ISubject<TSink3> third)
        {
            Drivers = drivers;
            First = first;
            Second = second;
            Third = third;
        }

        public Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> Drivers { get; }
        public ISubject<TSink1> First { get; }
        public ISubject<TSink2> Second { get; }
        public ISubject<TSink3> Third { get; }

        public IComponent Invoke()
        {
            IObservable<TSource1> first = this.Drivers.OnFirst(this.First);
            IObservable<TSource2> second = this.Drivers.OnSecond(this.Second);
            IObservable<TSource3> third = this.Drivers.OnThird(this.Third);
            return new Component<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>(
                this,
                first,
                second,
                third);
        }
    }

    static class Program
    {
        private static IObservable<int> LogDriver(IObservable<int> sinks)
        {
            sinks.Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<int>();
        }

        private static IObservable<int> KeyInputDriver(IObservable<int> sinks)
        {
            return Observable.Range(20, 5);
        }

        private static Streams<int, int, Unit> CycleMain(Streams<int, int, Unit> sources)
        {
            var sinks = new Streams<int, int, Unit>(
                sources.Second,
                Observable.Empty<int>(),
                Observable.Empty<Unit>());

            return sinks;
        }
        static void Main(string[] args)
        {
            var drivers = new Drivers<int, int, int, int, Unit, Unit>(
                CycleMain,
                onFirst: LogDriver,
                onSecond: KeyInputDriver,
                onThird: _ => _);
            Runner<Unit, Unit>.Run(drivers);
            Console.ReadLine();
        }
    }
}
