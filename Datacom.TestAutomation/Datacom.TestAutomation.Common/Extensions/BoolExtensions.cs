﻿namespace Datacom.TestAutomation.Common.Extensions
{
    public static class BoolExtensions
    {
        public static string AsString(this bool @bool, string trueCondition, string falseCondition)
        {
            return @bool ? trueCondition : falseCondition;
        }

        public static string IfFalse(this bool @bool, string falseCondition)
        {
            return AsString(@bool, string.Empty, falseCondition);
        }

        public static string IfTrue(this bool @bool, string trueCondition)
        {
            return AsString(@bool, trueCondition, string.Empty);
        }
    }
}
