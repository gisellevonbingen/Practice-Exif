using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exif
{
    public class ExifEntry
    {
        public ExifTagId TagId { get; set; }
        public ExifValue Value { get; set; }

        public ExifEntry()
        {

        }

        public override string ToString()
        {
            return $"Id: \"{this.TagId}\", Value: {this.Value}";
        }

    }

}
