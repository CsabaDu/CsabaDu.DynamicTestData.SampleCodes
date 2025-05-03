// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.DemoClassTests;

public sealed class DemoClassTestsInstanceWithTheoryTestData : IDisposable
{
    private readonly DemoClass _sut = new();
    private static readonly TestDataToTheoryTestDataSource DataSource = new(ArgsCode.Instance);

    public static TheoryTestData? IsOlderReturnsTheoryTestData
    => DataSource.IsOlderReturnsToTheoryTestData();

    public static TheoryTestData? IsOlderThrowsTheoryTestData
    => DataSource.IsOlderThrowsToTheoryTestData();

    public void Dispose()
    => DataSource.ResetTheoryTestData();

    [Theory, MemberTestData(nameof(IsOlderReturnsTheoryTestData))]
    public void IsOlder_validArgs_returnsExpected(TestDataReturns<bool, DateTime, DateTime> testData)
    {
        // Arrange & Act
        var actual = _sut.IsOlder(testData.Arg1, testData.Arg2);

        // Assert
        Assert.Equal(testData.Expected, actual);
    }

    [Theory, MemberTestData(nameof(IsOlderThrowsTheoryTestData))]
    public void IsOlder_invalidArgs_throwsException(TestDataThrows<ArgumentOutOfRangeException, DateTime, DateTime> testData)
    {
        // Arrange & Act
        void attempt() => _ = _sut.IsOlder(testData.Arg1, testData.Arg2);

        // Assert
        var actual = Assert.Throws<ArgumentOutOfRangeException>(attempt);
        Assert.Equal(testData.Expected.ParamName, actual.ParamName);
        Assert.Equal(testData.Expected.Message, actual.Message);
    }
}
