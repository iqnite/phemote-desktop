namespace PhemoteDesktop
{
    public class PhemoteDesktopForm : Form
    {
        public PhemoteDesktopForm()
        {
            Text = "Phemote Control";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new(typeof(PhemoteDesktopForm));
            SuspendLayout();
            // 
            // PhemoteDesktopForm
            // 
            ClientSize = new Size(282, 253);
            Icon = resources.GetObject("$this.Icon") as Icon;
            Name = "PhemoteDesktopForm";
            Text = "Phemote Control";
            ResumeLayout(false);
            Label ipLabel = new()
            {
                Text = "",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 100)
            };
        }
    }
}