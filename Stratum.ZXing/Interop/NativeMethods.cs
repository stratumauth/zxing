// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using System.Runtime.InteropServices;

namespace Stratum.ZXing.Interop
{
    internal static partial class NativeMethods
    {
        private const string SharedObject = "ZXing";

        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReadBarcodes")]
        internal static partial IntPtr ReadBarcodes(ImageView.ImageViewSafeHandle imageView, ReaderOptions.ReaderOptionsSafeHandle readerOptions);

        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcodes_size")]
        internal static partial int Barcodes_Size(IntPtr barcodes);

        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcodes_move")]
        internal static partial IntPtr Barcodes_Move(IntPtr barcodes, int index);

        [LibraryImport(SharedObject, EntryPoint = "ZXing_LastErrorMsg", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(StringMarshaller))]
        internal static partial string LastErrorMessage();
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_free")]
        internal static partial void Free(IntPtr handle);
       
        // Barcode
        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcode_isValid")]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static partial bool Barcode_IsValid(QrCode.BarcodeSafeHandle barcode);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcode_text", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(StringMarshaller))]
        internal static partial string Barcode_Text(QrCode.BarcodeSafeHandle barcode);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcode_errorMsg", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(StringMarshaller))]
        internal static partial string Barcode_ErrorMessage(QrCode.BarcodeSafeHandle barcode);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcode_errorType")]
        internal static partial ErrorType Barcode_ErrorType(QrCode.BarcodeSafeHandle barcode);

        [LibraryImport(SharedObject, EntryPoint = "ZXing_Barcode_delete")]
        internal static partial void Barcode_delete(IntPtr barcode);
        
        // ImageView
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ImageView_new_checked")]
        internal static partial IntPtr ImageView_NewChecked(ReadOnlySpan<byte> data, int size, int width, int height, ImageFormat format, int rowStride, int pixelStride);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ImageView_delete")]
        internal static partial void ImageView_Delete(IntPtr handle);
        
        // ReaderOptions
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_new")]
        internal static partial IntPtr ReaderOptions_New();
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_delete")]
        internal static partial IntPtr ReaderOptions_Delete(IntPtr handle);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_setTryHarder")]
        internal static partial void ReaderOptions_SetTryHarder(ReaderOptions.ReaderOptionsSafeHandle handle, [MarshalAs(UnmanagedType.I1)] bool value);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_setTryRotate")]
        internal static partial void ReaderOptions_SetTryRotate(ReaderOptions.ReaderOptionsSafeHandle handle, [MarshalAs(UnmanagedType.I1)] bool value);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_setTryInvert")]
        internal static partial void ReaderOptions_SetTryInvert(ReaderOptions.ReaderOptionsSafeHandle handle, [MarshalAs(UnmanagedType.I1)] bool value);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_setBinarizer")]
        internal static partial void ReaderOptions_SetBinarizer(ReaderOptions.ReaderOptionsSafeHandle handle, Binarizer binarizer);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_setFormats")]
        internal static partial void ReaderOptions_SetFormats(IntPtr handle, BarcodeFormats formats);
        
        [LibraryImport(SharedObject, EntryPoint = "ZXing_ReaderOptions_setMaxNumberOfSymbols")]
        internal static partial void ReaderOptions_SetMaxNumberOfSymbols(IntPtr handle, byte value);
    }
}