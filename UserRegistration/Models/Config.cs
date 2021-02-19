using System;
using System.Reflection;
using UserRegistration.Components;

namespace UserRegistration.Models
{
    class Config
    {
        public string Connectioncode { get; set; }
        public void Write(ConnectionModel connectionModel)
        {
            Connectioncode = Yaml<Config>.YamlToModel(@"\InputData\Config.yaml").Connectioncode;
            foreach (var item in Connectioncode.ToLower().Split(','))
            {
                switch (item)
                {
                    case "youtrack":
                        connectionModel.Youtrack = true;
                        LoadLibrary(nameof(connectionModel.Youtrack));
                        break;
                    case "gitlab":
                        connectionModel.Gitlab = true;
                        //LoadLibrary(nameof(connectionModel.Gitlab));
                        break;
                    case "azuread":
                        connectionModel.Azuread = true;
                        //LoadLibrary(nameof(connectionModel.AzureAD));
                        break;
                    case "teams":
                        connectionModel.Teams = true;
                        //LoadLibrary(nameof(connectionModel.Teams));
                        break;
                    default:
                        break;
                }
            }
            Yaml<ConnectionModel>.ModelToYaml(connectionModel, "AppConfig.yaml");
        }
        private static void LoadLibrary(string libraryToLoad)
        {
            Assembly asm = Assembly.LoadFrom($"{libraryToLoad}.dll");
            Type[] type = asm.GetTypes();
            Object obj = asm.CreateInstance(type[0].ToString());
            object[] obj1 = new object[0];
            MethodInfo method = type[0].GetMethod("Method");//TODO: method = var libraryToLoad
            method.Invoke(obj, obj1);
        }
    }
}
