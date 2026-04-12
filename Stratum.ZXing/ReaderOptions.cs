// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using Stratum.ZXing.Interop;

namespace Stratum.ZXing
{
    public class ReaderOptions : IDisposable
    {
        internal readonly ReaderOptionsSafeHandle Handle;

        public ReaderOptions()
        {
            var handle = NativeMethods.ReaderOptions_New();
            Guard.ThrowIfNullPointer(handle);
            
            Handle = new ReaderOptionsSafeHandle(handle);
            NativeMethods.ReaderOptions_SetFormats(Handle, [BarcodeFormats.QrCode], 1);
            NativeMethods.ReaderOptions_SetMaxNumberOfSymbols(Handle, 1);
        }

        public bool TryHarder
        {
            get;
            set
            {
                field = value;
                NativeMethods.ReaderOptions_SetTryHarder(Handle, value);
            }
        }

        public bool TryRotate
        {
            get;
            set
            {
                field = value;
                NativeMethods.ReaderOptions_SetTryRotate(Handle, value);
            }
        }

        public bool TryInvert
        {
            get;
            set
            {
                field = value;
                NativeMethods.ReaderOptions_SetTryInvert(Handle, value);
            }
        }

        public Binarizer Binarizer
        {
            get;
            set
            {
                field = value;
                NativeMethods.ReaderOptions_SetBinarizer(Handle, value);
            }
        } = Binarizer.FixedThreshold;

        public void Dispose()
        {
            if (Handle is { IsInvalid: false })
            {
                Handle.Dispose();
            }
            
            GC.SuppressFinalize(this);
        }

        internal class ReaderOptionsSafeHandle : NonNullSafeHandle
        {
            public ReaderOptionsSafeHandle(IntPtr handle) : base(handle)
            {
                this.handle = handle;
            }

            protected override bool ReleaseHandle()
            {
                NativeMethods.ReaderOptions_Delete(handle);
                return true;
            }
        }
    }
}