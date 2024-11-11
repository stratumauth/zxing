// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using Stratum.ZXing.Interop;

namespace Stratum.ZXing
{
    internal class QrCode : IDisposable
    {
        private readonly BarcodeSafeHandle _handle;

        internal QrCode(IntPtr handle)
        {
            Guard.ThrowIfNullPointer(handle);
            _handle = new BarcodeSafeHandle(handle);
        }
        
        public bool IsValid => NativeMethods.Barcode_IsValid(_handle);

        public string Text => NativeMethods.Barcode_Text(_handle);

        public string ErrorMessage => NativeMethods.Barcode_ErrorMessage(_handle);
        
        public ErrorType ErrorType => NativeMethods.Barcode_ErrorType(_handle);

        public void Dispose()
        {
            if (_handle is { IsInvalid: false })
            {
                _handle.Dispose();
            }
            
            GC.SuppressFinalize(this);
        }

        internal class BarcodeSafeHandle : NonNullSafeHandle
        {
            public BarcodeSafeHandle(IntPtr handle) : base(handle)
            {
            }

            protected override bool ReleaseHandle()
            {
                NativeMethods.Barcode_delete(handle);
                return true;
            }
        }
    }
}