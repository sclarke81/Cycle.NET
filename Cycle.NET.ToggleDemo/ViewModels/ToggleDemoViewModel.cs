using System.Reactive.Concurrency;
using System.Reactive.Linq;
using ReactiveUI;

namespace Cycle.NET.ToggleDemo.ViewModels
{
    /// <summary>
    /// A view model that contains a toggled checkbox.
    /// </summary>
    public class ToggleDemoViewModel : ViewModelBase
    {
        private ObservableAsPropertyHelper<string> _status;
        private bool _toggled;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleDemoViewModel"/> class.
        /// </summary>
        /// <param name="mainThreadScheduler">The scheduler to use for main thread operations.</param>
        /// <param name="taskPoolScheduler">The scheduler to use for task pool operations.</param>
        /// <param name="hostScreen">The screen to use for routing operations.</param>
        public ToggleDemoViewModel(
                IScheduler mainThreadScheduler = null,
                IScheduler taskPoolScheduler = null,
                IScreen hostScreen = null)
            : base("Toggle a Checkbox", mainThreadScheduler, taskPoolScheduler, hostScreen)
        {
            Func<IObservable<string>, IObservable<bool>> domDriver = (IObservable<string> sinks) =>
            {
                sinks.ToProperty(this, x => x.Status, out this._status);

                return this.WhenAnyValue(x => x.Toggled);
            };

            Runner.Run(
                CycleMain,
                domDriver);
        }

        private static IObservable<string> CycleMain(IObservable<bool> domSources)
        {
            var domSink =
                domSources
                .Select(toggled => toggled ? "ON" : "off");

            return domSink;
        }

        /// <summary>
        /// Gets or sets a value indicaying whether the toggle is toggled.
        /// </summary>
        public bool Toggled
        {
            get { return this._toggled; }
            set { this.RaiseAndSetIfChanged(ref this._toggled, value); }
        }

        /// <summary>
        /// Gets status text.
        /// </summary>
        public string Status => this._status.Value;
    }
}
