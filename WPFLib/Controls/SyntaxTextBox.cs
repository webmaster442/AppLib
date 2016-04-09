using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFLib.Controls
{
    /// <summary>
    /// TexBox with Syntax Highlight feature
    /// </summary>
    public class SyntaxTextBox : RichTextBox
    {

        struct SyntaxTag
        {
            public TextPointer StartPosition;
            public TextPointer EndPosition;
            public string Word;

        }

        private List<SyntaxTag> m_tags;

        /// <summary>
        /// Creates a new instace of SyntaxHighlightTextbox
        /// </summary>
        public SyntaxTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SyntaxTextBox), new FrameworkPropertyMetadata(typeof(SyntaxTextBox)));
            TextChanged += SyntaxTextBox_TextChanged;
            SetValue(Paragraph.MarginProperty, new Thickness(0));
            m_tags = new List<SyntaxTag>();
        }

        /// <summary>
        /// Dependency property for KeywordColor
        /// </summary>
        public static DependencyProperty KeyWordColorProperty =
            DependencyProperty.Register("KeyWordColor", typeof(Color), typeof(SyntaxTextBox), new PropertyMetadata(Colors.Blue));

        /// <summary>
        /// Specifies the keywordColor
        /// </summary>
        public Color KeyWordColor
        {
            get { return (Color)GetValue(KeyWordColorProperty); }
            set { SetValue(KeyWordColorProperty, value); }
        }

        /// <summary>
        /// Syntax provider element
        /// </summary>
        public SyntaxProvider Syntax
        {
            get; set;
        }

        private void SyntaxTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Document == null) return;
            var documentRange = new TextRange(Document.ContentStart, Document.ContentEnd);
            documentRange.ClearAllProperties();

            var navigator = Document.ContentStart;

            while (navigator.CompareTo(Document.ContentEnd) < 0)
            {
                var context = navigator.GetPointerContext(LogicalDirection.Backward);
                if (context == TextPointerContext.ElementStart && navigator.Parent is Run)
                {
                    CheckWordsInRun((Run)navigator.Parent);

                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
            Format();
        }

        private void Format()
        {
            if (Syntax == null) return;
            TextChanged -= SyntaxTextBox_TextChanged;

            for (int i = 0; i < m_tags.Count; i++)
            {
                TextRange range = new TextRange(m_tags[i].StartPosition, m_tags[i].EndPosition);
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(KeyWordColor));
                range.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            m_tags.Clear();

            TextChanged += SyntaxTextBox_TextChanged;
        }

        private void CheckWordsInRun(Run run)
        {
            if (Syntax == null) return;

            var text = run.Text;

            int sIndex = 0;
            int eIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsWhiteSpace(text[i]) | Syntax.SpecialChars.Contains(text[i]))
                {
                    if (i > 0 && !(Char.IsWhiteSpace(text[i - 1]) | Syntax.SpecialChars.Contains(text[i - 1])))
                    {
                        eIndex = i - 1;
                        var word = text.Substring(sIndex, eIndex - sIndex + 1);

                        if (Syntax.IsKnownKeyWord(word))
                        {
                            SyntaxTag t = new SyntaxTag();
                            t.StartPosition = run.ContentStart.GetPositionAtOffset(sIndex, LogicalDirection.Forward);
                            t.EndPosition = run.ContentStart.GetPositionAtOffset(eIndex + 1, LogicalDirection.Backward);
                            t.Word = word;
                            m_tags.Add(t);
                        }
                    }
                    sIndex = i + 1;
                }
            }

            string lastWord = text.Substring(sIndex, text.Length - sIndex);
            if (Syntax.IsKnownKeyWord(lastWord))
            {
                SyntaxTag t = new SyntaxTag();
                t.StartPosition = run.ContentStart.GetPositionAtOffset(sIndex, LogicalDirection.Forward);
                t.EndPosition = run.ContentStart.GetPositionAtOffset(eIndex + 1, LogicalDirection.Backward);
                t.Word = lastWord;
                m_tags.Add(t);
            }
        }
    }
}
