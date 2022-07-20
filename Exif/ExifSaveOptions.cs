﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exif
{
    public class ExifSaveOptions : SaveOptions
    {
        public bool IsLittleEndian { get; set; } = false;

        public ExifSaveOptions()
        {
        }

        public ExifSaveOptions(ExifSaveOptions other) : base(other)
        {
            this.IsLittleEndian = other.IsLittleEndian;
        }

    }

}
