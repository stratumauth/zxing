// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

namespace Stratum.ZXing
{
    public class QrCodeException : ZXingException
    {
        public readonly ErrorType Type;
        
        public QrCodeException(string message, ErrorType type) : base(message)
        {
            Type = type;
        }
    }
}