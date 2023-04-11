namespace Datacom.TestAutomation.Common
{
    public static class StringArrayExtensions
    {
        public static int[] FindIndexOf(this string[,] stringArr, string keyString, bool ignoreCase = true, bool exactMatch = false)
        {
            int[] result = { -1, -1 };
            for (int i = 0; i <= stringArr.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= stringArr.GetUpperBound(1); j++)
                {
                    if (exactMatch)
                    {
                        if (ignoreCase)
                        {
                            if (stringArr[i, j].Equals(keyString, StringComparison.InvariantCultureIgnoreCase))
                            {
                                result[0] = i;
                                result[1] = j;
                                return result;
                            }
                        }
                        else
                        {
                            if (stringArr[i, j].Equals(keyString))
                            {
                                result[0] = i;
                                result[1] = j;
                                return result;
                            }
                        }
                    }
                    else
                    {
                        if (ignoreCase)
                        {
                            if (stringArr[i, j].Contains(keyString, StringComparison.InvariantCultureIgnoreCase))
                            {
                                result[0] = i;
                                result[1] = j;
                                return result;
                            }
                        }
                        else
                        {
                            if (stringArr[i, j].Contains(keyString))
                            {
                                result[0] = i;
                                result[1] = j;
                                return result;
                            }
                        }
                    }
                }
            }

            // if keyString is not found then -1 is returned
            return result;
        }
    }
}
