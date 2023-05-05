namespace Cycle.NET.ToggleDemo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.MainPage = AppBootstrapper.CreateMainPage();
        }
    }
}
