# Exif
Study solution about exif/tiff file format

## Read exif data from stream
### Code Example
```CS
using (var input = new FileStream(inputFile, FileMode.Open))
{
	var exif = new ExifContainer(input);
	
	// Print all entries in all directories
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
	
	// Read extra data
	// Do stub
}
```
### Output Example
```
===== Directory 1/1 =====
Id: "NewSubfileType", Value: [0]
Id: "ImageWidth", Value: [1064]
Id: "ImageLength", Value: [912]
Id: "BitsPerSample", Value: [1]
Id: "Compression", Value: [5]
Id: "PhotometricInterpretation", Value: [3]
Id: "StripOffsets", Value: [8, 637, 1934, 4982, 11366, 19007, 25247, 31029, 36427, 41339]
Id: "SamplesPerPixel", Value: [1]
Id: "RowsPerStrip", Value: [92]
Id: "StripByteCounts", Value: [629, 1297, 3048, 6384, 7641, 6240, 5782, 5398, 4912, 2739]
Id: "XResolution", Value: [72000 / 1000 => 72]
Id: "YResolution", Value: [72000 / 1000 => 72]
Id: "ResolutionUnit", Value: [2]
Id: "Software", Value: paint.net 4.3.10
Id: "Predictor", Value: [1]
Id: "ColorMap", Value: [9252, 60138, 9252, 59367, 8738, 59110]
```

## Write exif data to stream

### Code Example
```CS
using (var output = new FileStream(outputFile, FileMode.Create))
{
	exif.Write(output);

	// Write extra data
	// Do stub
}
```

## Read and rewrite tiff file

[Exif.Test\Program.cs](https://github.com/gisellevonbingen/Exif/blob/main/Exif.Test/Program.cs)
