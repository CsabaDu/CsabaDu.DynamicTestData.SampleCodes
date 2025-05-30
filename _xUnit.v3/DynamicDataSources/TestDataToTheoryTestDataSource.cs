// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.DynamicDataSources;

public class TestDataToTheoryTestDataSource(ArgsCode argsCode) : DynamicTheoryTestDataSource(argsCode)
{
    private readonly DateTime DateTimeNow = DateTime.Now;

    private DateTime _thisDate;
    private DateTime _otherDate;

    public TheoryTestData? IsOlderReturnsToTheoryTestData(ArgsCode? argsCode = null)
    {
        bool expected = true;
        string definition = "thisDate is greater than otherDate";
        _thisDate = DateTimeNow;
        _otherDate = DateTimeNow.AddDays(-1);
        addOptional();

        expected = false;
        definition = "thisDate equals otherDate";
        _otherDate = DateTimeNow;
        addOptional();

        definition = "thisDate is less than otherDate";
        _thisDate = DateTimeNow.AddDays(-1);
        addOptional();

        return TheoryTestData;

        #region Local methods
        void addOptional()
        => AddOptional(add, argsCode);

        void add()
        => AddReturns(definition, expected, _thisDate, _otherDate);
        #endregion
    }

    public TheoryTestData? IsOlderThrowsToTheoryTestData(/*ArgsCode? argsCode = null*/)
    {
        string paramName = "otherDate";
        _thisDate = DateTimeNow;
        _otherDate = DateTimeNow.AddDays(1);
        add();

        paramName = "thisDate";
        _thisDate = DateTimeNow.AddDays(1);
        add();

        return TheoryTestData;

        #region Local methods
        //void addOptional()
        //=> AddOptional(add, argsCode);

        void add()
        => AddThrows(getDefinition(), getExpected(), _thisDate, _otherDate);

        string getDefinition()
        => $"{paramName} is greater than the current date";

        ArgumentOutOfRangeException getExpected()
        => new(paramName, DemoClass.GreaterThanCurrentDateTimeMessage);
        #endregion
    }
}
