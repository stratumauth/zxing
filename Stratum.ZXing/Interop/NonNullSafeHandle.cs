// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using System.Runtime.InteropServices;

namespace Stratum.ZXing.Interop
{
    internal abstract class NonNullSafeHandle : SafeHandle
    {
        protected NonNullSafeHandle(IntPtr handle) : base(IntPtr.Zero, true)
        {
            this.handle = handle;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}