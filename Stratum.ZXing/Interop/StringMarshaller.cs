// Copyright (C) 2024 jmh
// SPDX-License-Identifier: GPL-3.0-only

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Stratum.ZXing.Interop
{
    [CustomMarshaller(typeof(string), MarshalMode.Default, typeof(StringMarshaller))]
    internal static class StringMarshaller
    {
        public static string ConvertToManaged(IntPtr unmanaged)
        {
            if (unmanaged == IntPtr.Zero)
            {
                return null;
            }
            
            var str = Marshal.PtrToStringUTF8(unmanaged);
            NativeMethods.Free(unmanaged);

            return str;
        }
    }
}