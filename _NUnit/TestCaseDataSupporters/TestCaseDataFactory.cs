// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

using CsabaDu.DynamicTestData.TestDataTypes.Interfaces;
using static CsabaDu.DynamicTestData.TestDataTypes.TestDataFactory;

namespace CsabaDu.DynamicTestData.SampleCodes.NUnit.TestCaseDataSupporters
{
    public static class TestCaseDataFactory
    {
        public static TestCaseData TestDataToTestCaseData(
            ITestData testData,
            ArgsCode argsCode,
            string? testMethodName = null)
        {
            ArgumentNullException.ThrowIfNull(testData, nameof(testData));

            ITestDataReturns? testDataReturns = testData as ITestDataReturns;
            bool isReturns = testDataReturns is not null;

            var parameters = TestDataToParams(
                testData,
                argsCode,
                PropsCode.Throws,
                out string testCaseName);

            var testCaseData = new TestCaseData(parameters)
                .SetDescription(testCaseName)
                .SetName(GetDisplayName(
                    testMethodName,
                    testCaseName));

            Type testDataType = testData.GetType();

            if (argsCode == ArgsCode.Properties)
            {
                Type[] genericTypes =
                    testDataType.GetGenericArguments();

                testCaseData.TypeArgs = isReturns ?
                    genericTypes[1..]
                    : genericTypes;
            }
            else
            {
                testCaseData.TypeArgs = [testDataType];
            }

            return isReturns ?
                testCaseData.Returns(
                    testDataReturns!.GetExpected())
                : testCaseData;
        }
    }
}
