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

    }

}
