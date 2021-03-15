using System;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace espansoeditor
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                System.Console.WriteLine("Empty trigger or replacements!!!");
            }
            else
            {
                string Path = @"/home/nandhakumar/Documents/MyGit/espansotriggers/user/nn.yml";

                using (var reader = new StreamReader(Path))
                {
                    var yaml = new YamlStream();
                    yaml.Load(reader);

                    var rootNode = (YamlMappingNode)yaml.Documents[0].RootNode;
                    var matches = (YamlSequenceNode)rootNode["matches"];

                    var triggerProp = new YamlScalarNode();
                    triggerProp.Style = YamlDotNet.Core.ScalarStyle.DoubleQuoted;
                    triggerProp.Value = args[0];

                    var replaceProp = new YamlScalarNode();
                    replaceProp.Style = YamlDotNet.Core.ScalarStyle.DoubleQuoted;
                    replaceProp.Value = args[1];

                    var espansoItem = new YamlMappingNode();
                    espansoItem.Add("trigger", triggerProp);
                    espansoItem.Add("replace", replaceProp);

                    matches.Add(espansoItem);

                    using (TextWriter writer = File.CreateText(Path))
                    {
                        yaml.Save(writer, false);
                    }
                }

            }
        }
    }
}
