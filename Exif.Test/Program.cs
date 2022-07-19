using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exif.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var testDirectory = @"C:\Users\Seil\Desktop\Test\Exif\";
            var exif = new ExifContainer();
            var strips = new Dictionary<ExifImageFileDirectory, byte[][]>();

            using (var input = new FileStream($"{testDirectory}32.tiff", FileMode.Open))
            {
                exif.Read(input);

                for (var i = 0; i < exif.Directories.Count; i++)
                {
                    var directory = exif.Directories[i];
                    Console.WriteLine($"===== Directory {i + 1}/{exif.Directories.Count} =====");

                    foreach (var entry in directory.Entries)
                    {
                        Console.WriteLine(entry);
                    }

                    strips[directory] = ReadTiffStrips(input, directory);
                }

            }

            RearrangeTiffStripOffsets(exif);

            using (var output = new FileStream($"{testDirectory}output.tiff", FileMode.Create))
            {
                exif.Write(output);

                for (var i = 0; i < exif.Directories.Count; i++)
                {
                    var directory = exif.Directories[i];
                    var strip = strips[directory];

                    for (var j = 0; j < strip.Length; j++)
                    {
                        var bytes = strip[j];
                        output.Write(bytes, 0, bytes.Length);
                    }

                }

            }

        }

        public static byte[][] ReadTiffStrips(Stream input, ExifImageFileDirectory directory)
        {
            var stripOffsetsEntry = directory.Entries.FirstOrDefault(e => e.TagId == ExifTagId.StripOffsets);
            var stripByteCountsEntry = directory.Entries.FirstOrDefault(e => e.TagId == ExifTagId.StripByteCounts);
            var stripOffsetsValues = stripOffsetsEntry.Value.AsNumbers().AsSigneds.ToArray();
            var stripByteCountsValues = stripByteCountsEntry.Value.AsNumbers().AsSigneds.ToArray();
            var strips = new byte[stripOffsetsValues.Length][];

            for (var i = 0; i < stripOffsetsValues.Length; i++)
            {
                strips[i] = new byte[stripByteCountsValues[i]];
                input.Position = stripOffsetsValues[i];
                input.Read(strips[i], 0, strips[i].Length);
            }

            return strips;
        }

        public static void RearrangeTiffStripOffsets(ExifContainer exif)
        {
            var stripCursor = exif.InfoWithValuesSize;

            foreach (var directory in exif.Directories)
            {
                var stripOffsetsEntry = directory.Entries.FirstOrDefault(e => e.TagId == ExifTagId.StripOffsets);
                var stripByteCountsEntry = directory.Entries.FirstOrDefault(e => e.TagId == ExifTagId.StripByteCounts);
                var stripByteCountsValues = stripByteCountsEntry.Value.AsNumbers().AsSigneds.ToArray();

                if (stripOffsetsEntry.Value is ExifValueLongs longs)
                {
                    for (var i = 0; i < longs.Values.Length; i++)
                    {
                        longs.Values[i] = (uint)stripCursor;
                        stripCursor += stripByteCountsValues[i];
                    }

                }

            }

        }

    }

}
