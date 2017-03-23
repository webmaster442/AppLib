using System.Linq;
using System.Text;

namespace AppLib.WPF.Controls.ImageControls
{
    /// <summary>
    /// Code 128
    /// Convert an input string to the equivilant string including start and stop characters.
    /// This object compresses the values to the shortest possible code 128 barcode format 
    /// </summary>
    internal static class BarcodeConverter128
    {
        /// <summary>
        /// Converts an input string to the equivilant string, that need to be produced using the 'Code 128' font.
        /// </summary>
        /// <param name="value">String to be encoded</param>
        /// <returns>Encoded string start/stop and checksum characters included</returns>
        public static string StringToBarcode(string value)
        {
            int charPos, minCharPos;
            int currentChar, checksum;
            bool isTableB = true;

            StringBuilder ret = new StringBuilder();

            if (string.IsNullOrEmpty(value))
                return string.Empty;

            // Check for invalid characters
            var valid = value.Select(c => (c >= 126 || c <= 32)).Any();
            if (!valid) return string.Empty;

            // Barcode is full of ascii characters, we can now process it
            charPos = 0;
            while (charPos < value.Length)
            {
                if (isTableB)
                {
                    // See if interesting to switch to table C
                    // yes for 4 digits at start or end, else if 6 digits
                    if (charPos == 0 || charPos + 4 == value.Length)
                        minCharPos = 4;
                    else
                        minCharPos = 6;


                    minCharPos = IsNumber(value, charPos, minCharPos);

                    if (minCharPos < 0)
                    {
                        // Choice table C
                        if (charPos == 0)
                            ret.Append((char)205);
                        else
                            ret.Append((char)199);
                        isTableB = false;
                    }
                    else if (charPos == 0)
                        ret.Append((char)204);
                }

                if (!isTableB)
                {
                    // We are on table C, try to process 2 digits
                    minCharPos = 2;
                    minCharPos = IsNumber(value, charPos, minCharPos);
                    if (minCharPos < 0) // OK for 2 digits, process it
                    {
                        currentChar = int.Parse(value.Substring(charPos, 2));
                        currentChar = currentChar < 95 ? currentChar + 32 : currentChar + 100;
                        ret.Append((char)currentChar);
                        charPos += 2;
                    }
                    else
                    {
                        // We haven't 2 digits, switch to table B
                        ret.Append((char)200);
                        isTableB = true;
                    }
                }
                if (isTableB)
                {
                    // Process 1 digit with table B
                    ret.Append(value[charPos]);
                    charPos++;
                }
            }

            // Calculation of the checksum
            checksum = 0;
            for (int loop = 0; loop < ret.Length; loop++)
            {
                currentChar = ret[loop];
                currentChar = currentChar < 127 ? currentChar - 32 : currentChar - 100;
                if (loop == 0)
                    checksum = currentChar;
                else
                    checksum = (checksum + (loop * currentChar)) % 103;
            }

            // Calculation of the checksum ASCII code
            checksum = checksum < 95 ? checksum + 32 : checksum + 100;
            // Add the checksum and the STOP
            ret.Append((char)checksum);
            ret.Append((char)206);

            return ret.ToString();
        }

        private static int IsNumber(string InputValue, int CharPos, int MinCharPos)
        {
            // if the MinCharPos characters from CharPos are numeric, then MinCharPos = -1
            MinCharPos--;
            if (CharPos + MinCharPos > InputValue.Length) return MinCharPos;

            while (MinCharPos >= 0)
            {
                var a = char.Parse(InputValue.Substring(CharPos + MinCharPos, 1)) < 48;
                var b = char.Parse(InputValue.Substring(CharPos + MinCharPos, 1)) > 57;
                if (a || b) break;
                MinCharPos--;
            }
            return MinCharPos;
        }
    }
}
