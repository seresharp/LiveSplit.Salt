using System.Windows.Forms;

namespace LiveSplit.Salt
{
    public partial class Settings : UserControl
    {
        public bool RandomizeSkins { get; set; } = true;

        public Settings()
        {
            InitializeComponent();

            rndSkinsBox.Click += (garbage, garbage2) => RandomizeSkins = rndSkinsBox.Checked;

            Load += LoadLayout;
        }

        private void LoadLayout(object sender, System.EventArgs e)
        {
            rndSkinsBox.Checked = RandomizeSkins;
        }
    }
}
