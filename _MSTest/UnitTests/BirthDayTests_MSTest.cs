// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.SampleCodes.DynamicDataSources;
using System.Reflection;

namespace CsabaDu.DynamicTestData.SampleCodes.MSTest.UnitTests
{
    [TestClass]
    public sealed class BirthDayTests_MSTest
    {
        private static readonly BirthDayDynamicObjectArraySource DataSource =
            new(ArgsCode.Instance);

        private const string DisplayName = nameof(GetDisplayName);

        public static string? GetDisplayName(MethodInfo testMethod, object?[] args)
        => DynamicDataSourceBase.GetDisplayName(testMethod.Name, args);

        private BirthDay? _sut;

        private static IEnumerable<object?[]>? BirthDayConstructorValidArgs =>
            DataSource.GetBirthDayConstructorValidArgs();

        [TestMethod]
        [DynamicData(nameof(BirthDayConstructorValidArgs), DynamicDataDisplayName = DisplayName)]
        public void Ctor_validArgs_createsInstance(TestData<DateOnly> testData)
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
    }
}
