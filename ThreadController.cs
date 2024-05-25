using System.Text;
using System.Threading;

namespace PassBruteForce
{
    public class ThreadController
    {
        private int threadCount;
        private string encodedPassword;
        private PasswordCracker passwordCracker;

        public ThreadController(int threadCount, string encodedPassword, PasswordCracker passwordCracker)
        {
            this.threadCount = threadCount;
            this.encodedPassword = encodedPassword;
            this.passwordCracker = passwordCracker;
        }

        public void Start()
        {
            Thread[] workerThreads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                workerThreads[i] = new Thread(TryPasswordCrack);
                workerThreads[i].Start();
            }

            foreach (Thread workerThread in workerThreads)
            {
                workerThread.Join();
            }
        }

        private void TryPasswordCrack()
        {
            char[] characterSet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            StringBuilder currentAttempt = new StringBuilder();

            while (!passwordCracker.IsPasswordFound())
            {
                string passwordAttempt = GenerateNextAttempt(currentAttempt, characterSet);
                if (ValidatePassword(passwordAttempt))
                {
                    passwordCracker.NotifyPasswordFound(passwordAttempt);
                    break;
                }
            }
        }

        private string GenerateNextAttempt(StringBuilder currentAttempt, char[] characterSet)
        {
            if (currentAttempt.Length == 0)
            {
                currentAttempt.Append(characterSet[0]);
            }
            else
            {
                int position = currentAttempt.Length - 1;
                while (position >= 0)
                {
                    if (currentAttempt[position] < characterSet[characterSet.Length - 1])
                    {
                        currentAttempt[position]++;
                        break;
                    }
                    else
                    {
                        currentAttempt[position] = characterSet[0];
                        if (position == 0)
                        {
                            currentAttempt.Insert(0, characterSet[0]);
                        }
                        position--;
                    }
                }
            }
            return currentAttempt.ToString(); // Ensure that a value is returned in all code paths
        }

        private bool ValidatePassword(string passwordAttempt)
        {
            PasswordHandler passwordHandler = new PasswordHandler();
            passwordHandler.ComputeHash(passwordAttempt);
            string hashedPasswordAttempt = passwordHandler.EncodedPassword;

            return hashedPasswordAttempt == encodedPassword; // Compare the hashed attempt with the actual hashed password
        }
    }
}
