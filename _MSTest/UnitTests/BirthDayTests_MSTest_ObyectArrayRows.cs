// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.Statics;

namespace CsabaDu.DynamicTestData.SampleCodes.MSTest.UnitTests;

[TestClass]
public sealed class BirthDayTests_MSTest_ObyectArrayRows
{
    #region Test preparation
    private static BirthDayDynamicObjectArrayRowSource DataSource
    => new(ArgsCode.Instance, null);

    [ClassCleanup(ClassCleanupBehavior.EndOfClass)]
    public static void Cleanup()
    {
        DataSource.ResetDataRowHolder();
    }

    public static string? GetDisplayName(MethodInfo testMethod, object?[] args)
    => DynamicDataSource.GetDisplayName(testMethod.Name, args);
    #endregion

    #region ArgsCode.Instance sample tests
    #region Constructor tests
    private static IEnumerable<object?[]>? BirthDayConstructorValidArgs
    => DataSource.GetBirthDayConstructorValidArgs();

    [TestMethod,
        DynamicData(nameof(BirthDayConstructorValidArgs),
        DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Ctor_validArgs_createsInstance(
        TestData<DateOnly> testData)
    {
        // Arrange
        string name = "valid name";
        DateOnly dateOfBirth = testData.Arg1;

        // Act
        var actual = new BirthDay(name, dateOfBirth);
        
        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(name, actual.Name);
        Assert.AreEqual(dateOfBirth, actual.DateOfBirth);
    }

    private static IEnumerable<object?[]>? BirthDayConstructorInvalidArgs
    => DataSource.GetBirthDayConstructorInvalidArgs();

    [TestMethod,
        DynamicData(nameof(BirthDayConstructorInvalidArgs),
        DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Ctor_invalidArgs_throwsArgumentException(
        TestDataThrows<ArgumentException, string> testData)
    {
        // Arrange
        string? name = testData.Arg1;
        DateOnly dateOfBirth = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
        var expected = testData.Expected;
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Act & Assert
        try
        {
            attempt();
            Assert.Fail(
                $"Expected {expected.GetType().Name} was not thrown.");
        }
        catch (ArgumentException actual)
        {
            Assert.IsInstanceOfType(actual, expected.GetType());
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ParamName, actual.ParamName);
        }
        catch (Exception ex)
        {
            Assert.Fail(
                $"Unexpected exception type: {ex.GetType().Name}");
        }
    }
    #endregion

    #region CompareTo tests
    private static IEnumerable<object?[]>? CompareToArgs
    => DataSource.GetCompareToArgs();

    [TestMethod,
        DynamicData(nameof(CompareToArgs),
        DynamicDataDisplayName = nameof(GetDisplayName))]
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
        Assert.AreEqual(testData.Expected, actual);
    }
    #endregion
    #endregion ArgsCode.Instance sample tests

    #region ArgsCode.Properties sample tests
    #region Constructor tests
    private static IEnumerable<object?[]>? BirthDayConstructorValidArgs_Props
    => DataSource.GetBirthDayConstructorValidArgs(ArgsCode.Properties);

    [TestMethod,
        DynamicData(nameof(BirthDayConstructorValidArgs_Props),
        DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Ctor_validArgs_createsInstance_Props(
        string ignored,
        DateOnly dateOfBirth)
    {
        // Arrange
        string name = "valid name";

        // Act
        var actual = new BirthDay(name, dateOfBirth);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(name, actual.Name);
        Assert.AreEqual(dateOfBirth, actual.DateOfBirth);
    }

    private static IEnumerable<object?[]>? BirthDayConstructorInvalidArgs_Props
    => DataSource.GetBirthDayConstructorInvalidArgs(ArgsCode.Properties);

    [TestMethod,
        DynamicData(nameof(BirthDayConstructorInvalidArgs_Props),
        DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Ctor_invalidArgs_throwsArgumentException_Props(
        string ignored,
        ArgumentException expected,
        string? name)
    {
        // Arrange
        DateOnly dateOfBirth = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
        void attempt() => _ = new BirthDay(name!, dateOfBirth);

        // Act & Assert
        try
        {
            attempt();
            Assert.Fail(
                $"Expected {expected.GetType().Name} was not thrown.");
        }
        catch (ArgumentException actual)
        {
            Assert.IsInstanceOfType(actual, expected.GetType());
            Assert.AreEqual(expected.ParamName, actual.ParamName);
            Assert.AreEqual(expected.Message, actual.Message);
        }
        catch (Exception ex)
        {
            Assert.Fail(
                $"Unexpected exception type: {ex.GetType().Name}");
        }
    }
    #endregion

    #region CompareTo tests
    private static IEnumerable<object?[]>? CompareToArgs_Props
        => DataSource.GetCompareToArgs(ArgsCode.Properties);

    [TestMethod,
        DynamicData(nameof(CompareToArgs_Props),
        DynamicDataDisplayName = nameof(GetDisplayName))]
    public void CompareTo_validArgs_returnsExpected_Props(
        string ignored,
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
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion ArgsCode.Properties sample tests
}
