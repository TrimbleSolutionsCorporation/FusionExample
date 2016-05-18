namespace FusionExample
{
    using System;
    using System.Windows.Input;
    using Fusion;

    /// <summary>
    /// Example application.
    /// </summary>
    public class ExampleApp : App
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            App.Start(new ExampleApp());
        }

        /// <summary>
        /// Creates the main window.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The main window view model.</returns>
        [PublishedView("App.MainWindow")]
        public ViewModel CreateMainWindow(object parameter)
        {
            return new MainWindowViewModel();
        }

        /// <summary>
        /// Raises the <see cref="E:KeyDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // F9 opens the diagnostic window.
            if (e.Match(Key.F9))
            {
                e.Handled = true;
                this.Host.UI.ShowDiagnosticWindow();
            }
        }
    }
}
