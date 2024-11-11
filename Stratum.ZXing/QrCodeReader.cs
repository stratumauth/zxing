// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using Stratum.ZXing.Interop;

namespace Stratum.ZXing
{
    public class QrCodeReader
    {
        private readonly ReaderOptions _options;

        public QrCodeReader(ReaderOptions options)
        {
            _options = options;
        }

        public QrCodeReader()
        {
            _options = new ReaderOptions();
        }
        
        public string Read(ImageView imageView)
        {
            var barcode = NativeMethods.ReadBarcode(imageView.Handle, _options.Handle);

            if (barcode == IntPtr.Zero)
            {
                return null;
            }
            
            using var qrCode = new QrCode(barcode);
            
            if (!qrCode.IsValid || qrCode.ErrorType != ErrorType.None)
            {
                throw new QrCodeException(qrCode.ErrorMessage, qrCode.ErrorType);
            }

            return qrCode.Text;
        }
    }
}