using Tekla.Structures.Model;

namespace FusionExample
{
    using System.Threading.Tasks;
    using Fusion;
    using TeklaStructures = Tekla.Structures.TeklaStructures;

    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : WindowViewModel
    {
        /// <summary>
        /// Indicates whether the application is connected to Tekla Structures.
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// The model name.
        /// </summary>
        private string modelName;

        /// <summary>
        /// Gets a boolean value indicating whether the application is connected.
        /// </summary>
        /// <value>True if the application is connected, otherwise false.</value>
        public bool IsConnected
        {
            get { return this.isConnected; }
            private set { this.SetValue(ref this.isConnected, value); }
        }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        /// <value>The model name.</value>
        public string ModelName
        {
            get { return this.modelName; }
            private set { this.SetValue(ref this.modelName, value); }
        }

        /// <summary>
        /// Shows a message.
        /// </summary>
        [CommandHandler]
        public void ShowMessage()
        {
            this.Host.UI.ShowMessageDialog("A message from the command handler.[nl/]The message can include [i]simple[/i] [b]formatting[/b] and newlines.", "Message", icon: "Geometry.Cloud");

            this.IsConnected = TeklaStructures.Connect();

            if (this.IsConnected)
            {
                Model myModel = new Model();
                ModelObjectEnumerator moe =
                    myModel.GetModelObjectSelector().GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                int beamNumber = moe.GetSize();
                string message = "Model contains " + beamNumber.ToString() + " beam(s).";
                this.Host.UI.ShowMessageDialog(message, "Beam count", icon: "Geometry.Cloud");
            }
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        protected override async void Initialize()
        {
            base.Initialize();
            
            this.IsConnected = await Task.Run(() => TeklaStructures.Connect());

            if (this.IsConnected)
            {
                Model myModel = new Model();
                this.ModelName = myModel.GetInfo().ModelName;
            }
        }
    }
}
