// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.SampleCodes.DynamicDataSources;
using CsabaDu.DynamicTestData.xUnit.v3.Attributes;
using Xunit;

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.DemoClassTests;

public class BirthDayTests : IDisposable
{
    private static readonly DateOnly Today =
        DateOnly.FromDateTime(DateTime.Now);
    private static readonly BirthDayDynamicObjectArraySource InstanceDataSource = new(ArgsCode.Instance);
    private static readonly BirthDayDynamicObjectArraySource PropertiesDataSource = new(ArgsCode.Properties);

    public static readonly IEnumerable<object?[]>?
        InstanceDataSourceBirthDayConstructorInvalidArgs = InstanceDataSource.GetBirthDayConstructorInvalidArgs(ArgsCode.Instance);

    public void Dispose()
    {
        InstanceDataSource.ResetDataRowHolder();
        PropertiesDataSource.ResetDataRowHolder();

        GC.SuppressFinalize(this);
    }


    #region Constructors tests
    // name is null => throws ArguemntNullException
    // name is empty => throws ArgumentException
    // name is white space => throws ArgumentException
    // dateOfBirth is less than the current day => throws ArgumentOutOfRangeException
    [Theory, MemberTestData(nameof(InstanceDataSource))]
    public void BirthDay_invalidArgs_throwsArgumentException(TestDataThrows<ArgumentException, string> testData)
    {
        // Arrange
        string? name = testData.Arg1;
        DateOnly dateOfBirth = Today.AddDays(1);

        // Act
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Assert
        var actual = Record.Exception(attempt);
        Assert.IsType<ArgumentException>(actual);
        Assert.Equal(testData.Expected.Message, actual?.Message);
        Assert.Equal(testData.Expected.ParamName, ((ArgumentException)actual!).ParamName);
    }

    // Valid name and dateOfBirth is equal with the current day => creates BirthDay instance
    // Valid name and dateOfBirth is greater than the current day => creates BirthDay instance
    #endregion

    #region CompareTo tetst
    // other is null => returns -1
    // this.DateOfBirth is less than other.DateOfBirth => returns -1
    // this.DateOfBirth is equal with other.DateOfBirth => returns 0
    // this.DateOfBirth is greater than other.DateOfBirth => returns 1
    #endregion
}
