using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaDeObfuscator
{
    class JSONHandler
    {
        public static void ParseTheJson(string[] args, ArrayList classList, RenameDatabase renameStore)
        {
            foreach(string s in classList)
            {
                string className = s.Split('\\')[s.Split('\\').Length - 1];
                string directory = s.Substring(0, s.Length - className.Length);
                string jsonName = className.Replace(".class", ".json");
                string fullFilePath = directory + jsonName;
                string json;
                var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    json = streamReader.ReadToEnd();
                }
                ObfuscatedClass clazz = JsonConvert.DeserializeObject<ObfuscatedClass>(json);
                string classNameStripped = className.Replace(".class", "");
                if (classNameStripped != clazz.Name)
                {
                    renameStore.AddRenameClass(className.Replace(".class", "") + " : " + clazz.Superclass, clazz.Name + " : " + clazz.Superclass);
                }
                else
                {
                    renameStore.AddRenameClass(className.Replace(".class", "") + " : " + clazz.Superclass, "___"+clazz.Name + " : " + clazz.Superclass);
                }
                foreach(ObfuscatedMethod method in clazz.Methods)
                {
                    if (method.OldName != method.NewName)
                    {
                        renameStore.AddRenameMethod(method.ParentClass, method.Signature, method.OldName, method.Signature, method.NewName);
                    }
                    else if (!method.OldName.Contains("<") && (!method.OldName.Equals("init")))
                    {
                        renameStore.AddRenameMethod(method.ParentClass, method.Signature, method.OldName, method.Signature, "___"+method.NewName);
                    }
                }
                foreach(ObfuscatedField field in clazz.Fields)
                {
                    if (field.OldName != field.NewName)
                    {
                        renameStore.AddRenameField(field.ParentClass, field.Signature, field.OldName, field.Signature, field.NewName);
                    }
                    else if(!field.OldName.Contains("this$0"))
                    {
                        renameStore.AddRenameField(field.ParentClass, field.Signature, field.OldName, field.Signature, "___"+field.NewName);
                    }
                }
            }
        }
    }
}
