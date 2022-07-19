using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exif
{
    public class ExifImageFileDirectory
    {
        public List<ExifEntry> Entries { get; } = new List<ExifEntry>();

        public long OffsetValuesSize
        {
            get
            {
                var size = 0;
                var entryCount = (short)this.Entries.Count;

                for (var i = 0; i < entryCount; i++)
                {
                    var entry = this.Entries[i];
                    var raw = new ExifRawEntry(entry);

                    if (raw.IsOffset == true)
                    {
                        size += raw.ValuesSize;
                    }

                }

                return size;
            }

        }

    }

}
