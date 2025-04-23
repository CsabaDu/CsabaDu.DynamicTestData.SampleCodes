﻿/* 
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
namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3;

public class TestDataToTheoryTestDataSource(ArgsCode argsCode) : DynamicTheoryTestDataSource(argsCode)
{
    private readonly DateTime DateTimeNow = DateTime.Now;

    private DateTime _thisDate;
    private DateTime _otherDate;

    public TheoryTestData? IsOlderReturnsToTheoryTestData()
    {
        bool expected = true;
        string definition = "thisDate is greater than otherDate";
        _thisDate = DateTimeNow;
        _otherDate = DateTimeNow.AddDays(-1);
        addOptionalToTheoryTestData();

        expected = false;
        definition = "thisDate equals otherDate";
        _otherDate = DateTimeNow;
        addOptionalToTheoryTestData();

        definition = "thisDate is less than otherDate";
        _thisDate = DateTimeNow.AddDays(-1);
        addOptionalToTheoryTestData();

        return TheoryTestData;

        #region Local methods
        void addOptionalToTheoryTestData()
        => AddOptionalToTheoryTestData(addTestDataToTheoryTestData, argsCode);

        void addTestDataToTheoryTestData()
        => AddTestDataReturnsToTheoryTestData(definition, expected, _thisDate, _otherDate);
        #endregion
    }

    public TheoryTestData? IsOlderThrowsToTheoryTestData(string testMethodName, ArgsCode? argsCode = null)
    {
        TheoryTestData.InitTestMethodName(testMethodName);

        string paramName = "otherDate";
        _thisDate = DateTimeNow;
        _otherDate = DateTimeNow.AddDays(1);
        addTestDataToTheoryTestData();

        paramName = "thisDate";
        _thisDate = DateTimeNow.AddDays(1);
        addTestDataToTheoryTestData();

        return TheoryTestData;

        #region Local methods
        void addTestDataToTheoryTestData()
        => AddTestDataThrowsToTheoryTestData(getDefinition(), getExpected(), _thisDate, _otherDate);

        string getDefinition()
        => $"{paramName} is greater than the current date";

        ArgumentOutOfRangeException getExpected()
        => new(paramName, DemoClass.GreaterThanCurrentDateTimeMessage);
        #endregion
    }
}
