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
            
            NativeMethods.ReaderOptions_SetFormats(handle, BarcodeFormats.QrCode);
            NativeMethods.ReaderOptions_SetMaxNumberOfSymbols(handle, 1);
            
            Handle = new ReaderOptionsSafeHandle(handle);
        }

        private bool _tryHarder;
        private bool _tryRotate;
        private bool _tryInvert;
        private Binarizer _binarizer = Binarizer.FixedThreshold;
        
        public bool TryHarder
        {
            get => _tryHarder;
            set
            {
                _tryHarder = value;
                NativeMethods.ReaderOptions_SetTryHarder(Handle, value);
            }
        }
        
        public bool TryRotate
        {
            get => _tryRotate;
            set
            {
                _tryRotate = value;
                NativeMethods.ReaderOptions_SetTryRotate(Handle, value);
            }
        }
        
        public bool TryInvert
        {
            get => _tryInvert;
            set
            {
                _tryInvert = value;
                NativeMethods.ReaderOptions_SetTryInvert(Handle, value);
            }
        }

        public Binarizer Binarizer
        {
            get => _binarizer;
            set
            {
                _binarizer = value;
                NativeMethods.ReaderOptions_SetBinarizer(Handle, value);
            }
        }

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
            }

            protected override bool ReleaseHandle()
            {
                NativeMethods.ReaderOptions_Delete(handle);
                return true;
            }
        }
    }
}