using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exif
{
    public class ExifImageFileDirectory : Dictionary<ExifTagId, ExifValue>
    {
        public ExifImageFileDirectory()
        {

        }

        public ExifImageFileDirectory(IDictionary<ExifTagId, ExifValue> dictionary) : base(dictionary)
        {

        }

        public ExifImageFileDirectory(IEnumerable<KeyValuePair<ExifTagId, ExifValue>> collection) : base(collection)
        {

        }

        public long OffsetValuesSize
        {
            get
            {
                var size = 0;

                foreach (var pair in this)
                {
                    var raw = new ExifRawEntry(pair);

                    if (raw.IsOffset == true)
                    {
                        size += raw.ValuesSize;
                    }

                }

                return size;
            }

        }

        public bool TryGetIntegers(ExifTagId id, out IExifValueIntegers integers)
        {
            if (this.TryGetValue(id, out var value) == true && value is IExifValueIntegers _integers)
            {
                integers = _integers;
                return true;
            }
            else
            {
                integers = null;
                return false;
            }

        }

        public int GetSigned(ExifTagId id, int fallback = default)
        {
            if (this.TryGetIntegers(id, out var integers) == true)
            {
                return integers.AsSigned;
            }
            else
            {
                return fallback;
            }

        }

        public uint GetUnsigned(ExifTagId id, uint fallback = default)
        {
            if (this.TryGetIntegers(id, out var integers) == true)
            {
                return integers.AsUnsigned;
            }
            else
            {
                return fallback;
            }

        }

        public int[] GetSigneds(ExifTagId id)
        {
            if (this.TryGetIntegers(id, out var integers) == true)
            {
                return integers.AsSigneds.ToArray();
            }
            else
            {
                return new int[0];
            }

        }

        public uint[] GetUnsigneds(ExifTagId id)
        {
            if (this.TryGetIntegers(id, out var integers) == true)
            {
                return integers.AsUnsigneds.ToArray();
            }
            else
            {
                return new uint[0];
            }

        }

    }

}
