// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.xUnit.v3.TestDataHolders.Interfaces;

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.DemoClassTests;

public sealed class BirthDayTests_TheoryTestData : IDisposable
{
    //private readonly DemoClass _sut = new();
    private static readonly BirthDayTheoryTestDataHolder DataSource = new(ArgsCode.Instance);

    public void Dispose()
    => DataSource.ResetDataRowHolder();

    #region ArgsCode.Instance
    public static IEnumerable<ITheoryTestDataRow>? BirthDayConstructorInvalidArgs
    => DataSource.GetBirthDayConstructorInvalidArgs(nameof(Ctor_invalidArgs_throwsArgumentException));


    [Theory, MemberTestData(nameof(BirthDayConstructorInvalidArgs))]
    public void Ctor_invalidArgs_throwsArgumentException(TestDataThrows<ArgumentException, string> testData)
    {
        // Arrange
        var expected = testData.Expected;
        var name = testData.Arg1;
        var dateOfBirth =
            DateOnly.FromDateTime(DateTime.Now).AddDays(1);

        // Act
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Assert
        var actual = Record.Exception(attempt);
        Assert.IsType(expected.GetType(), actual);
        Assert.Equal(expected.ParamName, (actual as ArgumentException)?.ParamName);
        Assert.Equal(expected.Message, actual.Message);
    }

    public static IEnumerable<ITheoryTestDataRow>? BirthDayConstructorValidArgs
    => DataSource.GetBirthDayConstructorValidArgs(nameof(Ctor_validArgs_createsInstance));


    [Theory, MemberTestData(nameof(BirthDayConstructorValidArgs))]
    public void Ctor_validArgs_createsInstance(TestData<DateOnly> testData)
    {
        // Arrange
        string name = "valid name";
        var dateOfBirth = testData.Arg1;

        // Act
        var actual = new BirthDay(name!, dateOfBirth);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(name, actual.Name);
        Assert.Equal(dateOfBirth, actual.DateOfBirth);
    }

    public static IEnumerable<ITheoryTestDataRow>? CompareToArgs
    => DataSource.GetCompareToArgs(nameof(CompareTo_args_returnsExpected));


    [Theory, MemberTestData(nameof(CompareToArgs))]
    public void CompareTo_args_returnsExpected(TestDataReturns<int, DateOnly, BirthDay> testData)
    {
        // Arrange
        var dateOfBirth = testData.Arg1;
        var other = testData.Arg2;
        var sut = new BirthDay("name", dateOfBirth);

        // Act
        var actual = sut.CompareTo(other);

        // Assert
        Assert.Equal(testData.Expected, actual);
    }
    #endregion

    #region ArgsCode.Properties
    public static IEnumerable<ITheoryTestDataRow>? BirthDayConstructorInvalidArgsProps
    => DataSource.GetBirthDayConstructorInvalidArgs(nameof(Ctor_Props_invalidArgs_throwsArgumentException), ArgsCode.Properties);

    [Theory, MemberTestData(nameof(BirthDayConstructorInvalidArgsProps))]
    public void Ctor_Props_invalidArgs_throwsArgumentException(
        ArgumentException expected,
        string name)
    {
        // Arrange
        DateOnly dateOfBirth = DateOnly.FromDateTime(DateTime.Now).AddDays(1);

        // Act
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Assert
        var actual = Record.Exception(attempt);
        Assert.IsType(expected.GetType(), actual);
        Assert.Equal(expected.ParamName, (actual as ArgumentException)?.ParamName);
        Assert.Equal(expected.Message, actual.Message);
    }

    public static IEnumerable<ITheoryTestDataRow>? BirthDayConstructorValidArgsProps
    => DataSource.GetBirthDayConstructorValidArgs(nameof(Ctor_Props_validArgs_createsInstance), ArgsCode.Properties);


    [Theory, MemberTestData(nameof(BirthDayConstructorValidArgsProps))]
    public void Ctor_Props_validArgs_createsInstance(
        DateOnly dateOfBirth)
    {
        // Arrange
        string name = "valid name";

        // Act
        var actual = new BirthDay(name!, dateOfBirth);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(name, actual.Name);
        Assert.Equal(dateOfBirth, actual.DateOfBirth);
    }

    public static IEnumerable<ITheoryTestDataRow>? CompareToArgsProps
    => DataSource.GetCompareToArgs(nameof(CompareTo_Props_args_returnsExpected), ArgsCode.Properties);


    [Theory, MemberTestData(nameof(CompareToArgsProps))]
    public void CompareTo_Props_args_returnsExpected(
        int expected,
        DateOnly dateOfBirth,
        BirthDay other)
    {
        // Arrange
        var sut = new BirthDay("name", dateOfBirth);

        // Act
        var actual = sut.CompareTo(other);

        // Assert
        Assert.Equal(expected, actual);
    }
    #endregion
}

