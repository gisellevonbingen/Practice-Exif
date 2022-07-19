using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exif
{
    public class ExifValueSBytes : ExifValueIntegers<sbyte>
    {
        public ExifValueSBytes()
        {

        }

        public override ExifValueType Type => ExifValueType.SByte;

        public override sbyte ReadElement(DataProcessor processor) => processor.ReadSByte();

        public override void WriteElement(sbyte element, DataProcessor processor) => processor.WriteSByte(element);
    }

}
