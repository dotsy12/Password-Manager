using Password_Manager;
using System;
using System.ComponentModel.Design;
using System.Text;

namespace MyApp
{
    internal class Program
    {
        private static void AddOrChangePassword()
        {
            Console.Write("Please Enter Website/App name:");
            var appName = Console.ReadLine();
            Console.Write("Please Enter Password:");
            var password = Console.ReadLine();
            if (_passwordEntries.ContainsKey(appName))
                _passwordEntries[appName] = password;
            else
            {
                _passwordEntries.Add(appName, password);
                SavePassword();
            }
        }

        private static readonly Dictionary<string, string> _passwordEntries = new();
        private const string _masterName = "master";
        static void Main(string[] args)
        {
            ReadPassword();
            string storedPassword = "";
            if (!File.Exists("password.txt"))
            {
                // الملف غير موجود، اطلب من المستخدم تعيين كلمة مرور جديدة
                Console.Write("No password set. Please create a new password: ");
                storedPassword = Console.ReadLine();

                // احفظ كلمة المرور في الملف
                _passwordEntries.Add(_masterName, storedPassword);
                SavePassword();
                Console.WriteLine("Password set successfully.\n");
                DisplayMenu();
            }
            else
            {
                {
                    storedPassword = _passwordEntries[_masterName];
                    const int maxAttempts = 5; // عدد المحاولات المسموح بها

                    for (int attempt = 1; attempt <= maxAttempts; attempt++)
                    {
                        Console.Write("Please enter your password: ");
                        string enteredPassword = Console.ReadLine();

                        if (enteredPassword == storedPassword)
                        {
                            Console.WriteLine("Access granted.\n");
                            DisplayMenu();
                            return; // الخروج من الدالة بعد نجاح الدخول
                        }
                        else
                        {
                            int remainingAttempts = maxAttempts - attempt;

                            if (remainingAttempts > 0)
                            {
                                Console.WriteLine($"Invalid password. You have {remainingAttempts} attempts remaining.\n");
                            }
                            else
                            {
                                Console.WriteLine("Invalid password. Access denied.");
                                Environment.Exit(0); // إنهاء البرنامج بعد فشل المحاولات
                            }
                        }
                    }
                }
            }

        }


        static void DisplayMenu()
        {
            while (true)
            {
                Console.Clear(); // يمسح الشاشة في كل مرة لإعادة عرض القائمة بشكل نظيف
                Console.WriteLine("====================================");
                Console.WriteLine("          Password Manager          ");
                Console.WriteLine("====================================\n");

                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. List All Passwords");
                Console.WriteLine("2. Add/Change Password");
                Console.WriteLine("3. Get Password");
                Console.WriteLine("4. Delete Password");
                Console.WriteLine("------------------------------------");
                Console.Write("Your choice: ");

                var selectOption = Console.ReadLine();
                Console.WriteLine(); // سطر فارغ لجعل العرض يبدو أنظف

                if (selectOption == "1")
                {
                    ListAllPassword();
                }
                else if (selectOption == "2")
                {
                    AddOrChangePassword();
                }
                else if (selectOption == "3")
                {
                    GetPassword();
                }
                else if (selectOption == "4")
                {
                    DeletePassword();
                }
                else
                {
                    Console.WriteLine("Invalid Option! Please try again.");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey(); // ينتظر ضغط المستخدم على زر للعودة للقائمة
            }
        }
        private static void ListAllPassword()
        {
            foreach (var entry in _passwordEntries)
            {
                Console.WriteLine(entry.Key + "=" + entry.Value);
            }
        }
        private static void GetPassword()
        {
            Console.Write("Please Enter Website/App name:");
            var appName = Console.ReadLine();
            if (_passwordEntries.ContainsKey(appName))
                Console.WriteLine($"Password is :{_passwordEntries[appName]}");
            else
                Console.WriteLine("Password Not Found :(");
        }
        private static void DeletePassword()
        {

            Console.Write("Please Enter Website/App name:");
            var appName = Console.ReadLine();
            if (_passwordEntries.ContainsKey(appName))
            {
                _passwordEntries.Remove(appName);
                SavePassword();
            }

            else
                Console.WriteLine("Password Not Found :(");
        }
        private static void ReadPassword()
        {
            if (File.Exists("password.txt"))
            {
                var passwordLines = File.ReadAllText("password.txt");
                foreach (var line in passwordLines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf('=');
                        var appName = line.Substring(0, equalIndex);
                        var password = line.Substring(equalIndex + 1);
                        _passwordEntries.Add(appName, EncryptionUtilite.Dncrypt(password));
                    }
                }
            }
        }
        private static void SavePassword()
        {
            var sb = new StringBuilder();
            foreach (var entry in _passwordEntries)
            {
                sb.AppendLine($"{entry.Key}={EncryptionUtilite.Encrypt(entry.Value)}");
                File.WriteAllText("password.txt", sb.ToString());
            }
        }
    }
}