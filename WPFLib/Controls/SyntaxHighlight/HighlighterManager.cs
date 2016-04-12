using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace WPFLib.Controls
{

    /// <summary>
    /// An IHighlighter built from an Xml syntax file
    /// </summary>
    public class XmlHighlighter : IHighlighter
    {
        private List<HighlightWordsRule> wordsRules;
        private List<HighlightLineRule> lineRules;
        private List<AdvancedHighlightRule> regexRules;

        /// <summary>
        /// Creates a new instance of xml highlighter
        /// </summary>
        /// <param name="XML">xml stream to load</param>
        public XmlHighlighter(Stream XML)
        {
            var schemafile = Application.GetResourceStream(new Uri("pack://application:,,,/WPFLib;component/Resources/syntax.xsd"));
            var schemaStream = schemafile.Stream;

            XmlSchema schema = XmlSchema.Read(schemaStream, (s, e) =>
            {
                Debug.WriteLine("Xml schema validation error : " + e.Message);
            });
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.Schemas.Add(schema);
            readerSettings.ValidationType = ValidationType.Schema;

            try
            {
                var stream = XML;
                var reader = XmlReader.Create(stream, readerSettings);
                var xmldoc = XDocument.Load(reader);
                var root = xmldoc.Root;

                wordsRules = new List<HighlightWordsRule>();
                lineRules = new List<HighlightLineRule>();
                regexRules = new List<AdvancedHighlightRule>();

                foreach (XElement elem in root.Elements())
                {
                    switch (elem.Name.ToString())
                    {
                        case "HighlightWordsRule": wordsRules.Add(new HighlightWordsRule(elem)); break;
                        case "HighlightLineRule": lineRules.Add(new HighlightLineRule(elem)); break;
                        case "AdvancedHighlightRule": regexRules.Add(new AdvancedHighlightRule(elem)); break;
                    }
                }
            }
            catch (XmlSchemaValidationException ex)
            {

                Debug.WriteLine("Xml validation error at line " + ex.LineNumber);
                Debug.WriteLine("Warning : if you cannot find the issue in the xml file, verify the xsd file.");
                Debug.WriteLine(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }

        }

        /// <summary>
        /// Highlights the text of the current block.
        /// </summary>
        /// <param name="text">The text from the current block to highlight</param>
        /// <param name="previousBlockCode">The code assigned to the previous block, or -1 if
        /// there is no previous block</param>
        /// <returns>The current block code</returns>
        public int Highlight(FormattedText text, int previousBlockCode)
        {
            //
            // WORDS RULES
            //
            Regex wordsRgx = new Regex("[a-zA-Z_][a-zA-Z0-9_]*");
            foreach (Match m in wordsRgx.Matches(text.Text))
            {
                foreach (HighlightWordsRule rule in wordsRules)
                {
                    foreach (string word in rule.Words)
                    {
                        if (rule.Options.IgnoreCase)
                        {
                            if (m.Value.Equals(word, StringComparison.InvariantCultureIgnoreCase))
                            {
                                text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                                text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                                text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                            }
                        }
                        else {
                            if (m.Value == word)
                            {
                                text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                                text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                                text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                            }
                        }
                    }
                }
            }

            //
            // REGEX RULES
            //
            foreach (AdvancedHighlightRule rule in regexRules)
            {
                Regex regexRgx = new Regex(rule.Expression);
                foreach (Match m in regexRgx.Matches(text.Text))
                {
                    text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                    text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                    text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                }
            }

            //
            // LINES RULES
            //
            foreach (HighlightLineRule rule in lineRules)
            {
                Regex lineRgx = new Regex(Regex.Escape(rule.LineStart) + ".*");
                foreach (Match m in lineRgx.Matches(text.Text))
                {
                    text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                    text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                    text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                }
            }

            return -1;
        }
    }

    /// <summary>
    /// A set of words and their RuleOptions.
    /// </summary>
    internal class HighlightWordsRule
    {
        public List<string> Words { get; private set; }
        public RuleOptions Options { get; private set; }

        public HighlightWordsRule(XElement rule)
        {
            Words = new List<string>();
            Options = new RuleOptions(rule);

            string wordsStr = rule.Element("Words").Value;
            string[] words = Regex.Split(wordsStr, "\\s+");

            foreach (string word in words)
                if (!string.IsNullOrWhiteSpace(word))
                    Words.Add(word.Trim());
        }
    }

    /// <summary>
    /// A line start definition and its RuleOptions.
    /// </summary>
    internal class HighlightLineRule
    {
        public string LineStart { get; private set; }
        public RuleOptions Options { get; private set; }

        public HighlightLineRule(XElement rule)
        {
            LineStart = rule.Element("LineStart").Value.Trim();
            Options = new RuleOptions(rule);
        }
    }

    /// <summary>
    /// A regex and its RuleOptions.
    /// </summary>
    internal class AdvancedHighlightRule
    {
        public string Expression { get; private set; }
        public RuleOptions Options { get; private set; }

        public AdvancedHighlightRule(XElement rule)
        {
            Expression = rule.Element("Expression").Value.Trim();
            Options = new RuleOptions(rule);
        }
    }

    /// <summary>
    /// A set of options liked to each rule.
    /// </summary>
    internal class RuleOptions
    {
        public bool IgnoreCase { get; private set; }
        public Brush Foreground { get; private set; }
        public FontWeight FontWeight { get; private set; }
        public FontStyle FontStyle { get; private set; }

        public RuleOptions(XElement rule)
        {
            string ignoreCaseStr = rule.Element("IgnoreCase").Value.Trim();
            string foregroundStr = rule.Element("Foreground").Value.Trim();
            string fontWeightStr = rule.Element("FontWeight").Value.Trim();
            string fontStyleStr = rule.Element("FontStyle").Value.Trim();

            IgnoreCase = bool.Parse(ignoreCaseStr);
            Foreground = (Brush)new BrushConverter().ConvertFrom(foregroundStr);
            FontWeight = (FontWeight)new FontWeightConverter().ConvertFrom(fontWeightStr);
            FontStyle = (FontStyle)new FontStyleConverter().ConvertFrom(fontStyleStr);
        }
    }
}
