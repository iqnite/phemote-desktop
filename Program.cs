namespace PhemoteDesktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new PhemoteDesktopApplicationContext());
        }
    }

    public class PhemoteDesktopApplicationContext : ApplicationContext
    {
        private NotifyIcon notifyIcon;

        public PhemoteDesktopApplicationContext()
        {
            notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.AppIcon,
                Text = "Phemote Control",
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items =
                    {
                        new ToolStripMenuItem("Exit", null, Exit)
                    },
                },
                Visible = true,
            };
            notifyIcon.Click += OnNotifyIconClick;
        }

        void Exit(object? sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        void OnNotifyIconClick(object? sender, EventArgs e)
        {
            MouseEventArgs? eventArgs = e as MouseEventArgs;
            switch (eventArgs?.Button)
            {
                case MouseButtons.Left:
                    Form desktopForm = new PhemoteDesktopForm();
                    desktopForm.Show();
                    break;
                default:
                    break;
            }
        }
    }
}
