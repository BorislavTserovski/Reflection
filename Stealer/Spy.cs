using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class Spy
{

    public string StealFieldInfo(string nameOfClass, params string[] namesOfFields)
    {
        Type hackerType = Type.GetType(nameOfClass);
        StringBuilder sb = new StringBuilder();

        FieldInfo[] fields = hackerType.GetFields(
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
        Object classInstance = Activator.CreateInstance(hackerType, new object[] { });
        sb.AppendLine($"Class under investigation: {nameOfClass}");

        foreach (FieldInfo field in fields.Where(f => namesOfFields.Contains(f.Name)))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
        }
        return sb.ToString().Trim();

       
    }
    public string AnalyzeAcessModifiers(string className)
    {
        var sb = new StringBuilder();
        var targetType = Type.GetType(className);

        var wrongFields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Static |
                                               BindingFlags.Public);

        foreach (var field in wrongFields)
        {
            sb.AppendLine($"{field.Name} must be private!");
        }

        var pulbicMethods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        var nonpulbicMethods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (var method in nonpulbicMethods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} have to be public!");
        }

        foreach (var method in pulbicMethods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} have to be private!");
        }

        return sb.ToString().Trim();
    }

    public string RevealPrivateMethods(string className)
    {
        var targetType = Type.GetType(className);
        var sb = new StringBuilder();
        sb.AppendLine($"All Private Methods of Class: {targetType}");

        var baseType = targetType.BaseType;
        sb.AppendLine($"Base Class: {baseType.Name}");

        var privateMethods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var method in privateMethods)
        {
            sb.AppendLine(method.Name);
        }

        return sb.ToString().Trim();
    }

    public string CollectGettersAndSetters(string className)
    {
        var targetType = Type.GetType(className);
        var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var sb = new StringBuilder();

        foreach (var method in methods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} will return {method.ReturnType}");
        }

        foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
        }

        return sb.ToString().Trim();
    }

}

