using System.Reflection;
using System.Text;

namespace _01HarestingFields
{
    using System;

    class HarvestingFieldsTest
    {
        static void Main()
        {
            string input = Console.ReadLine();

            StringBuilder sb = new StringBuilder();
            Type type = typeof(HarvestingFields);
            while (input!="HARVEST")
            {
                switch (input)
                {
                    case "private":
                        FieldInfo[] privateFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                        foreach (FieldInfo pf in privateFields)
                        {
                            if (pf.IsPrivate)
                            {
                                sb.AppendLine($"private {pf.FieldType.Name} {pf.Name}");
                            }
                            
                        }
                        Console.WriteLine(sb.ToString().Trim());
                        sb.Clear();
                        break;
                    case "public":
                        FieldInfo[] publicFields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                        foreach (FieldInfo pubf in publicFields)
                        {
                            if (pubf.IsPublic)
                            {
                                sb.AppendLine($"public {pubf.FieldType.Name} {pubf.Name}");
                            }
                        }
                        Console.WriteLine(sb.ToString().Trim());
                        sb.Clear();
                        break;
                    case "protected":
                        FieldInfo[] protectedFields = type.GetFields(BindingFlags.Instance | BindingFlags.Static|BindingFlags.NonPublic
                            |BindingFlags.Public);
                        foreach (FieldInfo prf in protectedFields)
                        {
                            if (prf.IsFamily)
                            {
                                sb.AppendLine($"protected {prf.FieldType.Name} {prf.Name}");
                            }
                        }
                        Console.WriteLine(sb.ToString().Trim());
                        sb.Clear();
                        break;
                    case "all":
                        FieldInfo[] allFields = type.GetFields(BindingFlags.Instance | BindingFlags.Static|BindingFlags.NonPublic
                            |BindingFlags.Public);
                        string access = "";
                        foreach (FieldInfo af in allFields)
                        {
                            if (af.IsFamily)
                            {
                                access = "protected";
                            }
                            else if (af.IsPrivate)
                            {
                                access = "private";
                            }
                            else if (af.IsPublic)
                            {
                                access = "public";
                            }
                             sb.AppendLine($"{access} {af.FieldType.Name} {af.Name}");
                        }
                        Console.WriteLine(sb.ToString().Trim());
                        sb.Clear();
                        break;
                }
                input = Console.ReadLine();
            }
        }
    }
}
