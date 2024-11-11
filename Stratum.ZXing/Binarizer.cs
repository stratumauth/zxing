// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

namespace Stratum.ZXing
{
    public enum Binarizer : byte
    {
        LocalAverage = 0,
        GlobalHistogram = 1,
        FixedThreshold = 2,
        BoolCast = 3
    }
}