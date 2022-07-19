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

    }

}
