// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.DemoClassTests;

public sealed class DemoClassTestsPropertiesWithTheoryTestData : IDisposable
{
    private readonly DemoClass _sut = new();
    private static readonly TestDataToTheoryTestDataSource DataSource = new(ArgsCode.Properties);

    public static TheoryTestData? IsOlderReturnsTheoryTestData
    => DataSource.IsOlderReturnsToTheoryTestData();

    public static TheoryTestData? IsOlderThrowsTheoryTestData
    => DataSource.IsOlderThrowsToTheoryTestData();

    public void Dispose()
    => DataSource.ResetTheoryTestData();

    [Theory, MemberTestData(nameof(IsOlderReturnsTheoryTestData))]
    public void IsOlder_validArgs_returnsExpected(bool expected, DateTime thisDate, DateTime otherDate)
    {
        // Arrange & Act
        var actual = _sut.IsOlder(thisDate, otherDate);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory, MemberTestData(nameof(IsOlderThrowsTheoryTestData))]
    public void IsOlder_invalidArgs_throwsException(ArgumentOutOfRangeException expected, DateTime thisDate, DateTime otherDate)
    {
        // Arrange & Act
        void attempt() => _ = _sut.IsOlder(thisDate, otherDate);

        // Assert
        var actual = Assert.Throws<ArgumentOutOfRangeException>(attempt);
        Assert.Equal(expected.ParamName, actual.ParamName);
        Assert.Equal(expected.Message, actual.Message);
    }
}

