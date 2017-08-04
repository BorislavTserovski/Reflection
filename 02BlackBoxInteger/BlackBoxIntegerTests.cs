using System.Linq;
using System.Reflection;
using System.Text;


using System;

class BlackBoxIntegerTests
{
    static void Main()
    {
        string input = Console.ReadLine();
        Type type = typeof(BlackBoxInt);
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        var instance = Activator.CreateInstance(type, true);
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        StringBuilder result = new StringBuilder();
        while (input != "END")
        {
            string[] args = input.Split('_');
            string command = args[0];
            int number = int.Parse(args[1]);
            methods.FirstOrDefault(m => m.Name == command)?.Invoke(instance, new object[] { number });

            foreach (var field in fields)
            {
                result.AppendLine(field.GetValue(instance).ToString());
            }

            input = Console.ReadLine();
        }
        Console.WriteLine(result.ToString().Trim());
    }
}

