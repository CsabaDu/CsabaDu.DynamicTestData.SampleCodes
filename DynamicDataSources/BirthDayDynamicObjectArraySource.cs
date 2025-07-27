// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.DynamicDataSources;

public class BirthDayDynamicObjectArraySource(ArgsCode argsCode, PropertyCode propertyCode)
: DynamicObjectArraySource(argsCode, propertyCode)
{
    #region Static Fields
    private static readonly DateOnly Today =
        DateOnly.FromDateTime(DateTime.Now);
    #endregion

    #region Methods
    // 'TestData<DateOnly>' type usage.
    // Valid 'string name' parameter should be declared and initialized
    // within the test method.
    public IEnumerable<object?[]>? GetBirthDayConstructorValidArgs(
        ArgsCode? argsCode = null,
        PropertyCode? propertyCode = null)
    {
        string expected = "creates BirthDay instance";
        string paramName = "dateOfBirth";

        // Valid name and dateOfBirth is equal with the current day => creates BirthDay instance
        string description = $"Valid name and {paramName} is equal with the current day";
        DateOnly dateOfBirth = Today;
        yield return testDataToParams();

        // Valid name and dateOfBirth is less than the current day => creates BirthDay instance
        description = $"Valid name and {paramName} is less than the current day";
        dateOfBirth = Today.AddDays(-1);
        yield return testDataToParams();

        #region Local Methods
        object?[] testDataToParams()
        => WithOptionalDataStrategy(
            () => TestDataToParams(
                description,
                expected,
                dateOfBirth),
            nameof(TestDataToParams),
            argsCode,
            propertyCode)!;
        #endregion
    }

    // 'TestDataReturns<int, DateOnly, BirthDay>' type usage.
    // Valid 'string name' parameter should be declared and initialized
    // within the test method.
    public IEnumerable<object?[]>? GetCompareToArgs(
        ArgsCode? argsCode = null,
        PropertyCode? propertyCode = null)
    {
        string name = "valid name";
        DateOnly dateOfBirth = Today.AddDays(-1);

        // other is null => returns 1
        string description = "other is null";
        int expected = -1;
        BirthDay? other = null;
        yield return testDataToParams();

        // this.DateOfBirth is greater than other.DateOfBirth => returns -1
        description = "this.DateOfBirth is greater than other.DateOfBirth";
        other = new(name, dateOfBirth.AddDays(1));
        yield return testDataToParams();

        // this.DateOfBirth is equal with other.DateOfBirth => return 0
        description = "this.DateOfBirth is equal with other.DateOfBirth";
        expected = 0;
        other = new(name, dateOfBirth);
        yield return testDataToParams();

        // this.DateOfBirth is less than other.DateOfBirth => returns 1
        description = "this.DateOfBirth is less than other.DateOfBirth";
        expected = 1;
        other = new(name, dateOfBirth.AddDays(-1));
        yield return testDataToParams();

        #region Local Methods
        object?[] testDataToParams()
        => WithOptionalDataStrategy(
            () => TestDataReturnsToParams(
                description,
                expected,
                dateOfBirth,
                other),
            nameof(TestDataReturnsToParams),
            argsCode,
            propertyCode)!;
        #endregion
    }

    // 'TestDataThrows<ArgumentException, string>' type usage.
    // Invalid 'DateOnly dateOfBirth' parameter should be declared and initialized
    // within the test method.
    public IEnumerable<object?[]>? GetBirthDayConstructorInvalidArgs(
        ArgsCode? argsCode = null,
        PropertyCode? propertyCode = null)
    {
        string paramName = "name";

        // name is null => throws ArguemntNullException
        string description = $"{paramName} is null";
        string name = null!;
        ArgumentException expected = new ArgumentNullException(paramName);
        yield return testDataToParams();

        // name is empty => throws ArgumentException
        description = $"{paramName} is empty";
        name = string.Empty;
        string message = "The value cannot be an empty string " +
            "or composed entirely of whitespace.";
        expected = new ArgumentException(message, paramName);
        yield return testDataToParams();

        // name is white space => throws ArgumentException
        description = $"{paramName} is white space";
        name = " ";
        yield return testDataToParams();

        paramName = "dateOfBirth";

        // dateOfBirth is greater than the current day => throws ArgumentOutOfRangeException
        description = $"{paramName} is greater than the current day";
        name = "valid name";
        message = BirthDay.GreaterThanTheCurrentDateMessage;
        expected = new ArgumentOutOfRangeException(paramName, message);
        yield return testDataToParams();

        #region Local Methods

        object?[] testDataToParams()
        => WithOptionalDataStrategy(
            () => TestDataThrowsToParams(
                description,
                expected,
                name),
            nameof(TestDataThrowsToParams),
            argsCode,
            propertyCode)!;
        #endregion
    }
    #endregion
}
