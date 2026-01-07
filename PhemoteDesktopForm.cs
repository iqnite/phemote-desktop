using PhemoteDesktop.Properties;

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
            ClientSize = new Size(400, 220);
            Icon = resources.GetObject("$this.Icon") as Icon;
            Name = "PhemoteDesktopForm";
            Text = "Phemote Control";
            StartPosition = FormStartPosition.CenterScreen;
            ForeColor = Color.White;
            BackColor = Color.FromArgb(30, 30, 30);

            Label instructionLabel = new()
            {
                Name = "instructionLabel",
                Text = "Dial the following number in the Phemote Control app to connect:",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                MaximumSize = new Size(350, 0),
                AutoSize = true,
                Location = new Point(50, 50),
                TabIndex = 0
            };
            Controls.Add(instructionLabel);

            Label ipLabel = new()
            {
                Name = "ipLabel",
                Text = $"#{Server.GetLocalIPAddress().Replace(".", "*")}#{Resources.Port}#",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                MaximumSize = new Size(350, 0),
                AutoSize = true,
                Location = new Point(50, 120),
                TabIndex = 0
            };
            Controls.Add(ipLabel);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}