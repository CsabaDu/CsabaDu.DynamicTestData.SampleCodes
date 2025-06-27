// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.NUnit.UnitTests;

[TestFixture]
public class BirthdayTests_NUnit_TestCaseTestData
{
    #region Test preparation
    private static BirthDayDynamicTestCaseDataRowSource DataSource
    => new(ArgsCode.Instance);

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        DataSource.ResetDataRowHolder();
    }
    #endregion

    #region Constructors tests
    public static IEnumerable<TestCaseTestData>? BirthDayConstructorValidArgs
    => DataSource.GetBirthDayConstructorValidArgs(nameof(Ctor_validArgs_createsInstance));

    [TestCaseSource(nameof(BirthDayConstructorValidArgs))]
    public void Ctor_validArgs_createsInstance(TestData<DateOnly> testData)
    {
        // Arrange
        string name = "valid name";
        DateOnly dateOfBirth = testData.Arg1;

        // Act
        var actual = new BirthDay(name, dateOfBirth);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo(name));
            Assert.That(actual.DateOfBirth, Is.EqualTo(dateOfBirth));
        }
    }

    public static IEnumerable<TestCaseTestData>? BirthDayConstructorInvalidArgs
    => DataSource.GetBirthDayConstructorInvalidArgs(nameof(Ctor_invalidArgs_throwsArgumentException));

    [TestCaseSource(nameof(BirthDayConstructorInvalidArgs))]
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
        try
        {
            attempt();
            Assert.Fail(
                $"Expected {expected.GetType().Name} " +
                "was not thrown.");
        }
        catch (ArgumentException actual)
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(actual, Is.TypeOf(expected.GetType()));
                Assert.That(actual?.Message, Is.EqualTo(expected.Message));
                Assert.That(actual?.ParamName, Is.EqualTo(expected.ParamName));
            }
        }
        catch (Exception ex)
        {
            Assert.Fail(
                "Unexpected exception type: " +
                $"{ex.GetType().Name}");
        }
    }
    #endregion

    #region CompareTo tests
    public static IEnumerable<TestCaseTestData>? CompareToArgs
    => DataSource.GetCompareToArgs(nameof(CompareTo_validArgs_returnsExpected));

    [TestCaseSource(nameof(CompareToArgs))]
    public int CompareTo_validArgs_returnsExpected(
        TestDataReturns<int, DateOnly, BirthDay> testData)
    {
        // Arrange
        string name = "valid name";
        DateOnly dateOfBirth = testData.Arg1;
        BirthDay? other = testData.Arg2;
        BirthDay sut = new(name, dateOfBirth);

        // Act
        return sut.CompareTo(other);
    }
    #endregion

}
