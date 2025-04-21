/* 
 * MIT License
 * 
 * Copyright (c) 2025. Csaba Dudas (CsabaDu)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using Microsoft.VisualBasic;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.DynamicTestData.SampleCodes;

public class DemoClass
{
    public const string GreaterThanCurrentDateTimeMessage
        = "The DateTime parameter cannot be greater than the current date and time.";

    public bool IsOlder(DateTime thisDate, DateTime otherDate)
    {
        if (thisDate <= DateTime.Now && otherDate <= DateTime.Now)
        {
            return thisDate > otherDate;
        }

        throw new ArgumentOutOfRangeException(getParamName(), GreaterThanCurrentDateTimeMessage);

        #region Local methods
        string getParamName()
        => thisDate > DateTime.Now ? nameof(thisDate) : nameof(otherDate);
        #endregion
    }

    public sealed record BirthDay : IComparer<BirthDay>
    {
        private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Now);
        public const string ParameterValueCannotBeGreaterThanTheCurrentDateMessage
            = "Parameter value cannot be greater than the current date.";

        public BirthDay(string name, DateOnly dateOfBirth)
        {
            Name = Validated(name, nameof(name));
            DateOfBirth = Validated(dateOfBirth, nameof(dateOfBirth));
        }

        public BirthDay(string name, DateTime dateOfBirth)
        {
            Name = Validated(name, nameof(name));
            DateOfBirth = Validated(dateOfBirth, nameof(dateOfBirth));
        }

        public string Name { get; init; }

        public DateOnly DateOfBirth { get; init; }

        public int Compare(BirthDay? thisBirthDay, BirthDay? otherBirthDay)
        {
            DateOnly leftDate = thisBirthDay?.DateOfBirth ?? DateOnly.MinValue;
            DateOnly rightDate = otherBirthDay?.DateOfBirth ?? DateOnly.MinValue;

            if (leftDate <= Today && rightDate <= Today)
            {
                return leftDate.CompareTo(rightDate);
            }

            throw new ArgumentOutOfRangeException(getParamName(), ParameterValueCannotBeGreaterThanTheCurrentDateMessage);

            #region Local methods
            string getParamName()
            => leftDate > Today ? nameof(thisBirthDay) : nameof(otherBirthDay);
            #endregion
        }

        public bool IsOlderThan(BirthDay other)
        => Compare(this, other) < 0;

        private static DateOnly Validated<TDate>(TDate dateOfBirth, string paramName)
        where TDate : struct
        {
            if (dateOfBirth is DateOnly dateOnly)
            {
                return validated(dateOnly);
            }

            if (dateOfBirth is not DateTime dateTime)
            {
                throw new ArgumentException("Invalid date type.", paramName);
            }

            dateOnly = DateOnly.FromDateTime(dateTime);

            return validated(dateOnly);

            #region Local methods
            DateOnly validated(DateOnly dateOnly)
            => dateOnly <= Today ?
                dateOnly
                : throw new ArgumentOutOfRangeException(paramName, "Parameter value cannot be greater than the current date.");
            #endregion
        }

        private static string Validated(string name, string paramName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, paramName);
            return name;
        }
    }
}
