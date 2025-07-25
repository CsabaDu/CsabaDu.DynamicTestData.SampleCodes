﻿// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.UnitTests;

public class BirthDayTests_xUnit_v3_TheoryTestData : IDisposable
{
    #region Test preparation
    private static BirthDayTheoryTestDataHolder DataSource
    => new(ArgsCode.Instance);

    public void Dispose()
    {
        DataSource.ResetDataRowHolder();
        GC.SuppressFinalize(this);
    }
    #endregion

    #region Constructors tests
    public static TheoryTestData<TestData<DateOnly>>? BirthDayConstructorValidArgs
    => DataSource.GetBirthDayConstructorValidArgs(nameof(Ctor_validArgs_createsInstance));

    [Theory, MemberTestData(nameof(BirthDayConstructorValidArgs))]
    public void Ctor_validArgs_createsInstance(TestData<DateOnly> testData)
    {
        // Arrange
        string name = "valid name";
        DateOnly dateOfBirth = testData.Arg1;

        // Act
        var actual = new BirthDay(name, dateOfBirth);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(name, actual.Name);
        Assert.Equal(dateOfBirth, actual.DateOfBirth);
    }

    public static TheoryTestData<TestDataThrows<ArgumentException, string>>? BirthDayConstructorInvalidArgs
    => DataSource.GetBirthDayConstructorInvalidArgs(nameof(Ctor_invalidArgs_throwsArgumentException));

    [Theory, MemberTestData(nameof(BirthDayConstructorInvalidArgs))]
    public void Ctor_invalidArgs_throwsArgumentException(
        TestDataThrows<ArgumentException, string> testData)
    {
        // Arrange
        string? name = testData.Arg1;
        DateOnly dateOfBirth =
            DateOnly.FromDateTime(DateTime.Now).AddDays(1);
        var expected = testData.Expected;
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Act & Assert
        var actual = Record.Exception(attempt);
        Assert.IsType(expected.GetType(), actual);
        Assert.Equal(expected.Message, actual?.Message);
        Assert.Equal(expected.ParamName, (actual as ArgumentException)?.ParamName);
    }
    #endregion

    #region CompareTo tests
    public static TheoryTestData<TestDataReturns<int, DateOnly, BirthDay>>? CompareToArgs
    => DataSource.GetCompareToArgs(nameof(CompareTo_validArgs_returnsExpected));

    [Theory, MemberTestData(nameof(CompareToArgs))]
    public void CompareTo_validArgs_returnsExpected(
        TestDataReturns<int, DateOnly, BirthDay> testData)
    {
        // Arrange
        string name = "valid name";
        DateOnly dateOfBirth = testData.Arg1;
        BirthDay? other = testData.Arg2;
        BirthDay sut = new(name, dateOfBirth);

        // Act
        var actual = sut.CompareTo(other);

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion

    #region ArgsCode.Properties sample tests
    #region Constructor tests
    public static TheoryTestData<TestData<DateOnly>>? BirthDayConstructorValidArgs_Props
    => DataSource.GetBirthDayConstructorValidArgs(null, ArgsCode.Properties);

    [Theory, MemberTestData(nameof(BirthDayConstructorValidArgs_Props))]
    public void Ctor_validArgs_createsInstance_Props(
        DateOnly dateOfBirth)
    {
        // Arrange
        string name = "valid name";

        // Act
        var actual = new BirthDay(name, dateOfBirth);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(name, actual.Name);
        Assert.Equal(dateOfBirth, actual.DateOfBirth);
    }

    public static TheoryTestData<TestDataThrows<ArgumentException, string>>? BirthDayConstructorInvalidArgs_Props
    => DataSource.GetBirthDayConstructorInvalidArgs(null, ArgsCode.Properties);

    [Theory, MemberTestData(nameof(BirthDayConstructorInvalidArgs_Props))]
    public void Ctor_invalidArgs_throwsArgumentException_Props(
        ArgumentException expected,
        string? name)
    {
        // Arrange
        DateOnly dateOfBirth = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Act & Assert
        var actual = Record.Exception(attempt);
        Assert.IsType(expected.GetType(), actual);
        Assert.Equal(expected.Message, actual?.Message);
        Assert.Equal(expected.ParamName, (actual as ArgumentException)?.ParamName);
    }
    #endregion

    #region CompareTo tests
    public static TheoryTestData<TestDataReturns<int, DateOnly, BirthDay>>? CompareToArgs_Props
        => DataSource.GetCompareToArgs(null, ArgsCode.Properties);

    [Theory, MemberTestData(nameof(CompareToArgs_Props))]
    public void CompareTo_validArgs_returnsExpected_Props(
        int expected,
        DateOnly dateOfBirth,
        BirthDay? other)
    {
        // Arrange
        string name = "valid name";
        BirthDay sut = new(name, dateOfBirth);

        // Act
        var actual = sut.CompareTo(other);

        // Assert
        Assert.Equal(expected, actual);
    }
    #endregion
    #endregion ArgsCode.Properties sample tests
}

