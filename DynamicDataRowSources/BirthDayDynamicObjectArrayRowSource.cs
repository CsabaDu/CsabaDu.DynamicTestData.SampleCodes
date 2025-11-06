// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.DynamicDataRowSources;

public class BirthDayDynamicObjectArrayRowSource(ArgsCode argsCode, PropsCode propsCode)
: DynamicObjectArrayRowSource(argsCode, propsCode)
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
        PropsCode? propsCode = null)
    {
        string expected = "creates BirthDay instance";
        string paramName = "dateOfBirth";

        // Valid name and dateOfBirth is equal with the current day => creates BirthDay instance
        string definition = $"Valid name and {paramName} is equal with the current day";
        DateOnly dateOfBirth = Today;
        add();

        // Valid name and dateOfBirth is less than the current day => creates BirthDay instance
        definition = $"Valid name and {paramName} is less than the current day";
        dateOfBirth = Today.AddDays(-1);
        add();

        return GetRows(argsCode, propsCode);

        #region Local Methods
        void add()
        => Add(
            definition,
            expected,
            dateOfBirth);
        #endregion
    }

    // 'TestDataReturns<int, DateOnly, BirthDay>' type usage.
    // Valid 'string name' parameter should be declared and initialized
    // within the test method.
    public IEnumerable<object?[]>? GetCompareToArgs(
        ArgsCode? argsCode = null,
        PropsCode? propsCode = null)
    {
        string name = "valid name";
        DateOnly dateOfBirth = Today.AddDays(-1);

        // other is null => returns 1
        string definition = "other is null";
        int expected = -1;
        BirthDay? other = null;
        add();

        // this.DateOfBirth is greater than other.DateOfBirth => returns -1
        definition = "this.DateOfBirth is greater than other.DateOfBirth";
        other = new(name, dateOfBirth.AddDays(1));
        add();

        // this.DateOfBirth is equal with other.DateOfBirth => return 0
        definition = "this.DateOfBirth is equal with other.DateOfBirth";
        expected = 0;
        other = new(name, dateOfBirth);
        add();

        // this.DateOfBirth is less than other.DateOfBirth => returns 1
        definition = "this.DateOfBirth is less than other.DateOfBirth";
        expected = 1;
        other = new(name, dateOfBirth.AddDays(-1));
        add();

        return GetRows(argsCode, propsCode);

        #region Local Methods
        void add()
        => AddReturns(
            definition,
            expected,
            dateOfBirth,
            other);
        #endregion
    }

    // 'TestDataThrows<ArgumentException, string>' type usage.
    // Invalid 'DateOnly dateOfBirth' parameter should be declared and initialized
    // within the test method.
    public IEnumerable<object?[]>? GetBirthDayConstructorInvalidArgs(
        ArgsCode? argsCode = null,
        PropsCode? propsCode = null)
    {
        string paramName = "name";

        // name is null => throws ArguemntNullException
        string definition = $"{paramName} is null";
        string name = null!;
        ArgumentException expected = new ArgumentNullException(paramName);
        add();

        // name is empty => throws ArgumentException
        definition = $"{paramName} is empty";
        name = string.Empty;
        string message = "The value cannot be an empty string " +
            "or composed entirely of whitespace.";
        expected = new ArgumentException(message, paramName);
        add();

        // name is white space => throws ArgumentException
        definition = $"{paramName} is white space";
        name = " ";
        add();

        paramName = "dateOfBirth";

        // dateOfBirth is greater than the current day => throws ArgumentOutOfRangeException
        definition = $"{paramName} is greater than the current day";
        name = "valid name";
        message = BirthDay.GreaterThanTheCurrentDateMessage;
        expected = new ArgumentOutOfRangeException(paramName, message);
        add();

        return GetRows(argsCode, propsCode);

        #region Local Methods
        void add()
        => AddThrows(
            definition,
            expected,
            name);
        #endregion
    }
    #endregion
}
