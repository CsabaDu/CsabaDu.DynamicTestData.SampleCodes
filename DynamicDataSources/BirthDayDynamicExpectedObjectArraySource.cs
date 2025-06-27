﻿// SPDX-License-Identifier: MIT
// Copyright (c) 2025. Csaba Dudas (CsabaDu)

namespace CsabaDu.DynamicTestData.SampleCodes.DynamicDataSources;

public class BirthDayDynamicExpectedObjectArraySource(ArgsCode argsCode)
: BirthDayDynamicObjectArraySource(argsCode, typeof(IExpected));