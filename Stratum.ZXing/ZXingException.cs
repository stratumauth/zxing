// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;

namespace Stratum.ZXing
{
    public class ZXingException : Exception
    {
        public ZXingException(string message) : base(message)
        {
        } 
    }
}