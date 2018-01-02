using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AppLib.VersionIncrementer
{
    /// <summary>
    /// Incrementation logic
    /// </summary>
    internal static class IcrementerLogic
    {
        /// <summary>
        /// Serializes a VersionIncrement template to file
        /// </summary>
        /// <param name="target">Target file</param>
        /// <param name="template">Template to save</param>
        private static void Serialize(string target, VersionIncrement template)
        {
            XmlSerializer xs = new XmlSerializer(typeof(VersionIncrement));
            using (var fs = File.Create(target))
            {
                xs.Serialize(fs, template);
            }
            var file = ReadFile(target);
            CommandWords.AppendCommentInfo(file);
            WriteToFile(target, file);
        }

        /// <summary>
        /// Deserializes a VersionIncrement template from file
        /// </summary>
        /// <param name="source">File to deserialize from</param>
        /// <returns>A VersionIncrement template</returns>
        private static VersionIncrement DeSerialize(string source)
        {
            XmlSerializer xs = new XmlSerializer(typeof(VersionIncrement));
            using (var fs = File.OpenRead(source))
            {
                return (VersionIncrement)xs.Deserialize(fs);
            }
        }

        /// <summary>
        /// Reads an assembly info template (.cs file) into memory
        /// </summary>
        /// <param name="source">File to read</param>
        /// <returns>File contents as a StringBuilder</returns>
        private static StringBuilder ReadFile(string source)
        {
            return new StringBuilder(File.ReadAllText(source));
        }

        /// <summary>
        /// Writes a StringBuilder to file
        /// </summary>
        /// <param name="target">Target file</param>
        /// <param name="sb">Stringbuilder to write</param>
        private static void WriteToFile(string target, StringBuilder sb)
        {
            using (var file = File.CreateText(target))
            {
                file.Write(sb.ToString());
            }
        }

        /// <summary>
        /// Process a command word
        /// </summary>
        /// <param name="commandword">Command word to process</param>
        /// <param name="inc">VersionIncrement data</param>
        /// <param name="modified">true, if the build counter needs to be modified</param>
        /// <returns>The processed command word</returns>
        private static string Process(string commandword, VersionIncrement inc, bool increment, out bool modified)
        {
            modified = false;

            switch (commandword)
            {
                case CommandWords.DayInput:
                    return DateTime.Now.Day.ToString();
                case CommandWords.MonthInput:
                    return DateTime.Now.Month.ToString();
                case CommandWords.YearInput:
                    return DateTime.Now.Year.ToString();
                case CommandWords.BuildIncrement:
                    if (increment)
                    {
                        inc.BuildCounter += 1;
                        modified = true;
                    }
                    return inc.BuildCounter.ToString();
                case CommandWords.TimeStampInput:
                    return string.Format("{0:00}{1:00}{2:00}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                default:
                    //if no matching input return original command word
                    return commandword;
            }
        }

        /// <summary>
        /// Writes a VersionIncrement template file
        /// </summary>
        /// <param name="target">target file</param>
        public static void CreateTemplate(string target)
        {
            try
            {
                Serialize(target, new VersionIncrement());
            }
            catch (IOException ex)
            {
                Program.Error(ex);
            }
        }
        /// <summary>
        /// Increment job
        /// </summary>
        /// <param name="incrementertemplate">VersionIncrement XML template</param>
        /// <param name="assemblyinfotemplate">AssemblyInfo.cs template</param>
        /// <param name="outfile">Output file</param>
        /// <param name="increment">Increment version number flag</param>
        public static void Increment(string incrementertemplate, string assemblyinfotemplate, string outfile, bool increment)
        {
            try
            {
                VersionIncrement template = DeSerialize(incrementertemplate);
                bool modified;
                string Main = Process(template.Main, template, increment, out modified);
                string Minor = Process(template.Minor, template, increment, out modified);
                string Revision = Process(template.Revision, template, increment, out modified);
                string Build = Process(template.Build, template, increment, out modified);

                var output = new StringBuilder("using System.Reflection;\n\n");
                if (assemblyinfotemplate != "null")
                {
                    output = ReadFile(assemblyinfotemplate);
                }
                output.AppendFormat("[assembly: AssemblyVersion(\"{0}.{1}.{2}.{3}\")]\r\n", Main, Minor, Revision, Build);
                output.AppendFormat("[assembly: AssemblyFileVersion(\"{0}.{1}.{2}.{3}\")]\r\n", Main, Minor, Revision, Build);
                WriteToFile(outfile, output);

                if (modified)
                {
                    Serialize(incrementertemplate, template);
                }

            }
            catch (Exception ex)
            {
                Program.Error(ex);
            }
        }
    }
}
