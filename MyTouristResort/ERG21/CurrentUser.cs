using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERG21
{
    internal class CurrentUser
    {
        public static string Role { get; set; } = "Visitor";
        public static int Id { get; set; }
        public static string Username { get; set; } = "";
        public static string Email { get; set; } = "";
        public static string phone { get; set; } = "";

        // Ιστορικό
        public static List<string> History { get; set; } = new List<string>();

        public static void AddToHistory(string formName)
        {
            if (!History.Contains(formName))
            {
                History.Add(formName);
            }
        }

        public static void ClearHistory()
        {
            History.Clear();
        }

        public static void Reset()
        {
            Role = "Visitor";
            Id = 0;
            Username = "";
            Email = "";
            phone = "";
            History.Clear();
        }
    }
}
