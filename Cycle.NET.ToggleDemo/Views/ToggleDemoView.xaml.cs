using System.Reactive.Disposables;
using System.Reactive.Linq;
using Cycle.NET.ToggleDemo.ViewModels;
using ReactiveUI;

namespace Cycle.NET.ToggleDemo.Views
{
    /// <summary>
    /// Contains a toggled checkbox.
    /// </summary>
    public partial class ToggleDemoView : ContentPageBase<ToggleDemoViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleDemoView"/> class.
        /// </summary>
        public ToggleDemoView()
        {
            this.InitializeComponent();

            this.WhenActivated(disposables =>
            {
                Observable.FromEventPattern<CheckedChangedEventArgs>(
                    handler => this.Toggled.CheckedChanged += handler,
                    handler => this.Toggled.CheckedChanged -= handler)
                    .Select(p => p.EventArgs)
                    .Select(e => e.Value)
                    .BindTo(this, x => x.ViewModel.Toggled)
                    .DisposeWith(disposables);

                this.OneWayBind(this.ViewModel, vm => vm.Status, v => v.Status.Text).DisposeWith(disposables);
            });
        }
    }
}
