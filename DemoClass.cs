// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

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
