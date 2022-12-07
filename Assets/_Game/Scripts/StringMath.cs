namespace Aezakmi
{
    public static class StringMath
    {
        public static string Multiply(string numA, string numB)
        {
            int memorize = 0;
            int c = 0;
            string zeros = "";
            string mult = "";
            string result = "0";
            for (int a = numA.Length - 1; a >= 0; a--)
            {
                for (int b = numB.Length - 1; b >= 0; b--)
                {
                    c = int.Parse("" + numB[b]) * int.Parse("" + numA[a]) + memorize;
                    if (a > 0 && b > 0) { memorize = c / 10; c = c % 10; }
                    mult = "" + c + mult;
                }
                result = Add(result, mult + zeros);
                memorize = 0;
                zeros += "0";
                mult = "";
            }
            return result;
        }

        public static string Add(string numA, string numB)
        {
            string aN = "";
            string bN = "";
            string result = "";
            int c = 0;
            int memorize = 0;
            if (numA.Length > numB.Length) { aN = numA; bN = numB; }
            else { aN = numB; bN = numA; }
            for (int x = aN.Length - 1; x >= 0; x--)
            {
                if (x - (aN.Length - bN.Length) >= 0)
                    c = int.Parse("" + aN[x]) + int.Parse("" + bN[x - (aN.Length - bN.Length)]) + memorize;
                else c = int.Parse("" + aN[x]) + memorize;
                if (x > 0) { memorize = c / 10; c = c % 10; }
                result = "" + c + result;
            }
            return result;
        }

        private static bool isBigger(string numA, string numB)
        {
            bool output = false;
            if (numA.Length > numB.Length) output = true;
            else if (numA.Length == numB.Length)
            {
                for (int x = 0; x < numA.Length; x++)
                    if (int.Parse("" + numA[x]) > int.Parse("" + numB[x]))
                    { output = true; x = numA.Length; }
                    else if (int.Parse("" + numA[x]) < int.Parse("" + numB[x]))
                    { output = false; x = numA.Length; }
                    else output = false;
            }
            else output = false;
            return output;
        }

        private static bool isBiggerEqual(string numA, string numB)
        {
            if (numA == numB)
                return true;
            bool output = false;
            if (numA.Length > numB.Length) output = true;
            else if (numA.Length == numB.Length)
            {
                for (int x = 0; x < numA.Length; x++)
                    if (int.Parse("" + numA[x]) > int.Parse("" + numB[x]))
                    { output = true; x = numA.Length; }
                    else if (int.Parse("" + numA[x]) < int.Parse("" + numB[x]))
                    { output = false; x = numA.Length; }
                    else output = false;
            }
            else output = false;
            return output;
        }

        private static bool isZero(string numA)
        {
            bool output = false;
            for (int i = 1; i < 10; i++)
                output = numA.Contains("" + i);
            return !output;
        }

        private static string clearZeros(string numA)
        {
            string output = "";
            char removeIt = '0';
            for (int i = 0; i < numA.Length; i++)
                if (numA[i] != removeIt) { output += numA[i]; removeIt = 'x'; }
            if (output.Length > 0)
                return output;
            else return "0";
        }

        public static string Subtract(string numA, string numB)
        {
            string aN = "";
            string bN = "";
            string result = "";
            int zeros = 0;
            bool negative = false;
            int c = 0;
            int memorize = 0;
            if (numA.Length > numB.Length)
            { aN = numA; bN = numB; }
            else if (numA.Length == numB.Length)
            {
                for (int x = 0; x < numA.Length; x++)
                    if (int.Parse("" + numA[x]) == int.Parse("" + numB[x]))
                    {
                        aN = numA; bN = numB;
                        if (x == numA.Length - 1) return "0";
                        if (x < numA.Length - 1) zeros++;
                    }
                    else if (int.Parse("" + numA[x]) > int.Parse("" + numB[x]))
                    {
                        if (x < numA.Length - 1) zeros++; aN = numA; bN = numB; x = numA.Length;
                    }
                    else if (int.Parse("" + numA[x]) < int.Parse("" + numB[x]))
                    {
                        if (x < numA.Length - 1) zeros++;
                        aN = numB;
                        bN = numA;
                        negative = true;
                        x = numA.Length;
                    }
            }
            else { aN = numB; bN = numA; negative = true; }
            for (int x = aN.Length - 1; x >= 0; x--)
            {
                if (x - (aN.Length - bN.Length) >= 0)
                {
                    c = int.Parse("" + aN[x]) - int.Parse("" + bN[x - (aN.Length - bN.Length)]) + memorize;
                    if (c < 0) { c += 10; memorize = -1; }
                    else memorize = 0;
                }
                else
                {
                    c = int.Parse("" + aN[x]) + memorize;
                    if (c < 0) { c += 10; memorize = -1; }
                    else memorize = 0;
                }
                if (result.Length <= aN.Length - zeros) { result = "" + c + result; }
            }
            if (!negative) return result; else return "-" + result;
        }

        public static string Dev(string numA, string numB)
        {
            if (isBigger(numB, numA)) return "0";
            else if (numA == numB) return "1";
            else if (numB == "1") return numA;
            else if (numB == "0") return "0";
            string result = "";
            string devide = numB;
            string smallerA = "";
            for (int x = 0; x < numA.Length; x++)
            {
                smallerA += numA[x];
                smallerA = clearZeros(smallerA);
                if (isBigger(smallerA, devide))
                {
                    for (int i = 0; i < 11; i++)
                    {
                        if (isBiggerEqual(smallerA, devide))
                        {
                            devide = Add(devide, numB);
                        }
                        else
                        {
                            devide = Subtract(devide, numB);
                            smallerA = Subtract(smallerA, devide);
                            result += "" + i % 10;
                            i = 12;
                            devide = numB;
                        }
                    }
                }
                else if (smallerA == devide) { smallerA = "0"; result += "1"; devide = numB; }
                else
                {
                    if (result.Length > 0) result += "0"; devide = numB;
                }
            }
            return result;
        }
    }
}
