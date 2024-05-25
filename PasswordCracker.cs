using System;
using System.Text;
using System.Threading;

namespace PassBruteForce
{
    public class PasswordCracker
    {
        private volatile bool isPasswordFound;
        public string ResultOutput { get; private set; } = string.Empty;

        public void StartBruteForce(int threadCount, string encodedPassword)
        {
            isPasswordFound = false;
            ResultOutput = string.Empty;
            DateTime startTime = DateTime.Now;

            ThreadController threadController = new ThreadController(threadCount, encodedPassword, this);
            threadController.Start();

            DateTime endTime = DateTime.Now;
            TimeSpan timeElapsed = endTime - startTime;
            ResultOutput += $"Brute force attack completed in {timeElapsed.TotalSeconds} seconds.\n";
        }

        public void NotifyPasswordFound(string crackedPassword)
        {
            isPasswordFound = true;
            ResultOutput += $"Password cracked: {crackedPassword}\n";
        }

        public bool IsPasswordFound()
        {
            return isPasswordFound;
        }
    }
}
