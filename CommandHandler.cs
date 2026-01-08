namespace PhemoteDesktop
{
    static class CommandHandler
    {
        public static void HandleCommand(string command)
        {
            switch (command)
            {
                case "1":
                    SendKeys.SendWait("{UP}{LEFT}");
                    break;
                case "2":
                    SendKeys.SendWait("{UP}");
                    break;
                case "3":
                    SendKeys.SendWait("{UP}{RIGHT}");
                    break;
                case "4":
                    SendKeys.SendWait("{LEFT}");
                    break;
                case "5":
                    SendKeys.SendWait(" ");
                    break;
                case "6":
                    SendKeys.SendWait("{RIGHT}");
                    break;
                case "7":
                    SendKeys.SendWait("{DOWN}{LEFT}");
                    break;
                case "8":
                    SendKeys.SendWait("{DOWN}");
                    break;
                case "9":
                    SendKeys.SendWait("{DOWN}{RIGHT}");
                    break;
                case "code:9":
                    // *9# to kill the current window
                    SendKeys.SendWait("{ALT}{F4}");
                    break;
                default:
                    break;
            }
        }
    }
}
