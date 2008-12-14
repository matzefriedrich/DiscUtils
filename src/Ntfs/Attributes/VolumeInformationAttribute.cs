﻿//
// Copyright (c) 2008, Kenneth Bell
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

using System.IO;

namespace DiscUtils.Ntfs.Attributes
{
    internal class VolumeInformationAttribute : BaseAttribute
    {
        private byte _majorVersion;
        private byte _minorVersion;
        private VolumeInformationFlags _flags;

        public VolumeInformationAttribute(NtfsFileSystem fileSystem, FileAttributeRecord record)
            : base(fileSystem, record)
        {
            using (Stream s = Open(FileAccess.Read))
            {
                byte[] data = Utilities.ReadFully(s, (int)record.DataLength);
                _majorVersion = data[0x08];
                _minorVersion = data[0x09];
                _flags = (VolumeInformationFlags)Utilities.ToUInt16LittleEndian(data, 0x0A);
            }
        }

        public override void Dump(TextWriter writer, string indent)
        {
            writer.WriteLine(indent + "VOLUME INFORMATION ATTRIBUTE (" + (Name == null ? "No Name" : Name) + ")");
            writer.WriteLine(indent + "  Version: " + _majorVersion + "." + _minorVersion);
            writer.WriteLine(indent + "    Flags: " + _flags);
        }
    }

}
