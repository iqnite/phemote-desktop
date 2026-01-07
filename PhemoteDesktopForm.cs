namespace PhemoteDesktop
{
    public class PhemoteDesktopForm : Form
    {
        public Server PhemoteServer;
        public PhemoteDesktopForm(Server server)
        {
            Text = "Phemote Control";
            PhemoteServer = server;
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
            StartPosition = FormStartPosition.CenterScreen;

            Label ipLabel = new()
            {
                Name = "ipLabel",
                Text = "IP unavailable",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 100),
                TabIndex = 0
            };

            Controls.Add(ipLabel);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}