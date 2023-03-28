using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Cycle.NET.Demo
{
    class Program
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

        private static IObservable<(int? Log, int? KeyInput)> Driver(IObservable<(int? Log, int? KeyInput)> sinks)
        {
            var logSinks = sinks.Where(s => s.Log.HasValue);
            var logSources = LogDriver(logSinks.Select(s => s.Log.Value));

            var keyInputSinks = sinks.Where(s => s.KeyInput.HasValue);
            var keyInputSources = KeyInputDriver(keyInputSinks.Select(s => s.KeyInput.Value));

            return Observable.Merge(
                logSources.Select(s => (Log: s as int?, KeyInput: null as int?)),
                keyInputSources.Select(s => (Log: null as int?, KeyInput: s as int?)));
        }

        private static IObservable<(int? Log, int? KeyInput)> CycleMain(IObservable<(int? Log, int? KeyInput)> sources)
        {
            var sinks = sources.Select(s => (
                Log: s.KeyInput,
                KeyInput: null as int?));

            return sinks;
        }
        static void Main(string[] args)
        {
            var component = new Component<(int? Log, int? KeyInput), (int? Log, int? KeyInput)>(
                CycleMain);
            var drivers = new Drivers<(int? Log, int? KeyInput), (int? Log, int? KeyInput)>(
                onFirst: Driver);
            Runner<(int? Log, int? KeyInput), (int? Log, int? KeyInput)>.Run(component, drivers);
            Console.ReadLine();
        }
    }
}
