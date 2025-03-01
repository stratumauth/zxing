// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using Stratum.ZXing.Interop;

namespace Stratum.ZXing
{
    public class ImageView : IDisposable
    {
        internal readonly ImageViewSafeHandle Handle;

        internal ImageView(IntPtr handle)
        {
            Guard.ThrowIfNullPointer(handle);
            Handle = new ImageViewSafeHandle(handle);
        }

        public ImageView(ReadOnlySpan<byte> data, int width, int height, ImageFormat format, int rowStride = 0, int pixelStride = 0)
        {
            var handle = NativeMethods.ImageView_NewChecked(data, data.Length, width, height, format, rowStride, pixelStride);
            Guard.ThrowIfNullPointer(handle);
            Handle = new ImageViewSafeHandle(handle);
        }

        public void Rotate(int degrees)
        {
            NativeMethods.ImageView_Rotate(Handle, degrees);
        }
        
        public void Crop(int left, int top, int width, int height)
        {
            NativeMethods.ImageView_Crop(Handle, left, top, width, height);
        }
        
        public void Dispose()
        {
            if (Handle is { IsInvalid: false })
            {
                Handle.Dispose();
            }
            
            GC.SuppressFinalize(this);
        }
        
        internal class ImageViewSafeHandle : NonNullSafeHandle
        {
            public ImageViewSafeHandle(IntPtr handle) : base(handle)
            {
                this.handle = handle;
            }

            protected override bool ReleaseHandle()
            {
                NativeMethods.ImageView_Delete(handle);
                return true;
            }
        }
    }
}