//// SPDX-License-Identifier: MIT
//// Copyright (c) 2025. Csaba Dudas (CsabaDu)

//namespace CsabaDu.DynamicTestData.SampleCodes.xUnit.v3.DynamicDataSources;

//internal class BirthDayTestsDataSource(ArgsCode argsCode)
//: DynamicTestDataSource(argsCode)
//{
//    private static readonly DateOnly Today =
//        DateOnly.FromDateTime(DateTime.Now);

//    private string? _name = null;
//    private DateOnly _dateOfBirth = Today;

//    #region Constructors tests
//    public void BirthDayCtorThrowsData(ArgsCode? argsCode = null)
//    {
//        string definition = "Name is null";
//        string paramName = "name";
//        ArgumentException expected = new ArgumentNullException(paramName);
//        string? message = null;
//        addOptional();

//        definition = "Name is empty";
//        expected = new ArgumentException(paramName);
//        addOptional();

//        definition = "Name is white space";
//        addOptional();

//        definition = "Date of birth is greater than the current date";
//        _name = paramName;
//        _dateOfBirth = Today.AddDays(1);
//        paramName = "dateOfBirth";
//        message = BirthDay.GreaterThanTheCurrentDateMessage;
//        expected = new ArgumentOutOfRangeException(paramName, message);
//        addOptional();

//        #region Local methods
//        void addOptional()
//        => AddOptional(add, argsCode);

//        void add()
//        => AddThrows(
//            definition,
//            expected,
//            _name,
//            _dateOfBirth,
//            paramName,
//            message,
//            expected.GetType());
//        #endregion
//    }

//    // Creates
//    // - today
//    // - régebbi
//    #endregion

//    #region CompareTo tetst
//    // null
//    // kisebb
//    // egyenlő
//    // nagyobb
//    #endregion
//}
