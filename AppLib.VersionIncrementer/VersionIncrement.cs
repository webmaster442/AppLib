using System;
using System.Text;

namespace AppLib.VersionIncrementer
{
    [Serializable]
    public class VersionIncrement
    {
        public string Main { get; set; }

        public string Minor { get; set; }

        public string Revision { get; set; }

        public string Build { get; set; }
        public int BuildCounter { get; set; }

        public VersionIncrement()
        {
            Main = "1";
            Minor = "0";
            Revision = "0";
            Build = "$BUILD$";
            BuildCounter = 1;
        }
    }

    public static class CommandWords
    {
        public const string DayInput = "$DAY$";
        public const string YearInput = "$YEAR$";
        public const string MonthInput = "$MONTH$";
        public const string TimeStampInput = "$TIMESTAMP$";
        public const string BuildIncrement = "$BUILD$";


        public static void AppendCommentInfo(StringBuilder file)
        {
            file.AppendLine("\n<!--");
            file.AppendLine("Possible tags:");
            file.AppendLine("$DAY$ - Inserts current day");
            file.AppendLine("$YEAR$ - Inserts current year");
            file.AppendLine("$MONTH$ - Inserts current month");
            file.AppendLine("$TIMESTAMP$ - Creates a timestamp. If time is 11:45:22 then the timestamp will be: 114522");
            file.AppendLine("$BUILD$ - Inserts build counter value");
            file.AppendLine("-->");
        }

    }
}
