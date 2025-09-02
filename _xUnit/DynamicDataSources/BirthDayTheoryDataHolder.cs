// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.DynamicDataSources;

public class BirthDayTheoryDataHolder
: DynamicTheoryDataHolder
{
    private static readonly DateOnly Today =
        DateOnly.FromDateTime(DateTime.Now);

    // 'TestData<DateOnly>' type usage.
    // Valid 'string name' parameter should be declared and initialized
    // within the test method.
    public TheoryData<TestData<DateOnly>>? GetBirthDayConstructorValidArgs()
    {
        string expected = "creates BirthDay instance";
        string paramName = "dateOfBirth";

        // Valid name and dateOfBirth is equal with the current day => creates BirthDay instance
        string description = $"Valid name and {paramName} is equal with the current day";
        DateOnly dateOfBirth = Today;
        add();

        // Valid name and dateOfBirth is less than the current day => creates BirthDay instance
        description = $"Valid name and {paramName} is less than the current day";
        dateOfBirth = Today.AddDays(-1);
        add();

        return DataHolder as TheoryData<TestData<DateOnly>>;

        #region Local Methods
        void add()
        => Add(CreateTestData(
            description,
            expected,
            dateOfBirth));
        #endregion
    }

    // 'TestDataThrows<ArgumentException, string>' type usage.
    // Invalid 'DateOnly dateOfBirth' parameter should be declared and initialized
    // within the test method.
    public TheoryData<TestDataThrows<ArgumentException, string>>? GetBirthDayConstructorInvalidArgs()
    {
        string paramName = "name";

        // name is null => throws ArguemntNullException
        string description = $"{paramName} is null";
        ArgumentException expected = new ArgumentNullException(paramName);
        string name = null!;
        add();

        // name is empty => throws ArgumentException
        description = $"{paramName} is empty";
        expected = new ArgumentException(
            $"The value cannot be an empty string " +
            $"or composed entirely of whitespace.",
            paramName);
        name = string.Empty;
        add();

        // name is white space => throws ArgumentException
        description = $"{paramName} is white space";
        name = " ";
        add();

        paramName = "dateOfBirth";

        // dateOfBirth is greater than the current day => throws ArgumentOutOfRangeException
        description = $"{paramName} is greater than the current day";
        expected = new ArgumentOutOfRangeException(paramName, BirthDay.GreaterThanTheCurrentDateMessage);
        name = "validName";
        add();

        return DataHolder as TheoryData<TestDataThrows<ArgumentException, string>>;

        #region Local Methods
        void add()
        => Add(CreateTestDataThrows(
            description,
            expected,
            name));
        #endregion
    }

    // 'TestDataReturns<int, DateOnly, BirthDay>' type usage.
    // Valid 'string name' parameter should be declared and initialized
    // within the test method.
    public TheoryData<TestDataReturns<int, DateOnly, BirthDay>>? GetCompareToArgs()
    {
        string name = "validName";
        DateOnly dateOfBirth = Today.AddDays(-1);

        // other is null => returns 1
        string description = "other is null";
        int expected = -1;
        BirthDay? other = null;
        add();

        // this.DateOfBirth is greater than other.DateOfBirth => returns -1
        description = "this.DateOfBirth is greater than other.DateOfBirth";
        other = new(name, dateOfBirth.AddDays(1));
        add();

        // this.DateOfBirth is equal with other.DateOfBirth => return 0
        description = "this.DateOfBirth is equal with other.DateOfBirth";
        expected = 0;
        other = new(name, dateOfBirth);
        add();

        // this.DateOfBirth is less than other.DateOfBirth => returns 1
        description = "this.DateOfBirth is less than other.DateOfBirth";
        expected = 1;
        other = new(name, dateOfBirth.AddDays(-1));
        add();

        return DataHolder as TheoryData<TestDataReturns<int, DateOnly, BirthDay>>;

        #region Local Methods
        void add()
        => Add(CreateTestDataReturns(
            description,
            expected,
            dateOfBirth,
            other));
        #endregion
    }
}
