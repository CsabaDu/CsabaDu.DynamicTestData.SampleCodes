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
}

//public sealed class BirthDay(string name, DateOnly dateOfBirth) : IComparable<BirthDay>
//{
//    private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Now);
//    public const string ParameterValueCannotBeGreaterThanTheCurrentDateMessage
//        = "Parameter value cannot be greater than the current date.";

//    public string Name { get; init; } = Validated(name, nameof(name));

//    public DateOnly DateOfBirth { get; init; } = Validated(dateOfBirth, nameof(dateOfBirth));

//    public int CompareTo(BirthDay? other)
//    => DateOfBirth.CompareTo(other?.DateOfBirth ?? DateOnly.MinValue);

//    public bool IsOlderThan(BirthDay other)
//    => CompareTo(other) < 0;

//    private static DateOnly Validated(DateOnly dateOfBirth, string paramName)
//    => dateOfBirth <= Today ?
//        dateOfBirth
//        : throw new ArgumentOutOfRangeException(paramName, ParameterValueCannotBeGreaterThanTheCurrentDateMessage);

//    private static string Validated(string name, string paramName)
//    {
//        ArgumentException.ThrowIfNullOrWhiteSpace(name, paramName);
//        return name;
//    }
//}
