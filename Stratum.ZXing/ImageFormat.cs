// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

namespace Stratum.ZXing
{
    public enum ImageFormat
    {
        None = 0,
        Lum = 0x01000000,
        LumA = 0x02000000,
        RGB = 0x03000102,
        BGR = 0x03020100,
        RGBA = 0x04000102,
        ARGB = 0x04010203,
        BGRA = 0x04020100,
        ABGR = 0x04030201,
    }
}