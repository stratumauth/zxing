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
            var barcodes = NativeMethods.ReadBarcodes(imageView.Handle, _options.Handle);

            if (barcodes == IntPtr.Zero)
            {
                return null;
            }

            var size = NativeMethods.Barcodes_Size(barcodes);

            if (size < 1)
            {
                return null;
            }

            var barcode = NativeMethods.Barcodes_Move(barcodes, 0);
            Guard.ThrowIfNullPointer(barcode);
            
            using var qrCode = new QrCode(barcode);
            
            if (!qrCode.IsValid || qrCode.ErrorType != ErrorType.None)
            {
                throw new QrCodeException(qrCode.ErrorMessage, qrCode.ErrorType);
            }

            return qrCode.Text;
        }
    }
}