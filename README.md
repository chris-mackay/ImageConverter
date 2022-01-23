# ImageConverter
Console application to batch convert image file formats using [ImageMagick](https://github.com/dlemstra/Magick.NET)

## Console output
![Console](image_converter.png?raw=true "Console")

## Using the code
This particular implentation is converting HEIC files to Jpeg so you can view them in Microsoft Windows

```csharp
static void Main(string[] args)
    {
        Console.WriteLine("Use SHIFT + INSERT to paste path and press ENTER.\n");

        // Gets the directory where all the photos to convert are located.
        string d = Console.ReadLine();
        DirectoryInfo dInfo = new DirectoryInfo(d);

        // Gets all the HEIC files in the directory
        string[] photos = Directory.GetFiles(dInfo.ToString(), "*.HEIC");

        // Counter to tell the user the current file that is being processed.
        int counter = 1;

        // Gets a total count of files that will be processed.
        int count = photos.Count();

        // Add a blank line for clarity.
        Console.WriteLine();

        // Define the directory to store the HEIC files.
        string dHEIC = Path.Combine(d, "HEIC");

        // Create an archive directory to store all the HEIC files.
        if (!Directory.Exists(dHEIC))
        {
            Directory.CreateDirectory(dHEIC);
        }
        else
        {
            Directory.Delete(dHEIC, true);
            Directory.CreateDirectory(dHEIC);
        }

        foreach (string f in photos)
        {
            // Get the old file that we want to convert.
            FileInfo oldFile = new FileInfo(f);
            string name = oldFile.Name;

            // Create a new MagickImage object so we can set the new format.
            MagickImage image = new MagickImage(f);

            // Move the HEIC file to the archive directory
            File.Move(oldFile.ToString(), Path.Combine(dHEIC, name));

            // Set the desired image format to convert to.
            image.Format = MagickFormat.Jpeg;

            // Create an array of bytes. We write these bytes to a file below.
            byte[] data = image.ToByteArray();

            // Remove the old file extension.
            name = name.Replace("HEIC", "");

            // Set the new file name.
            string file = dInfo + "\\" + name + "jpg";

            FileInfo newFile = new FileInfo(file);

            // Create a file stream so we can write the bytes to our new file.
            FileStream fs = new FileStream(newFile.FullName, FileMode.Create, FileAccess.Write);

            // Write all the bytes to the file.
            foreach (byte item in data) fs.WriteByte(item);

            // Display the current file that was converted.
            Console.WriteLine(newFile.Name + " processed (" + counter + "/" + count + ")");
            counter++;

            // Dispose of the file stream and start all over again for the next image.
            fs.Close();
            fs = null;
        }

        Console.WriteLine(); // Add a blank line for clarity.
        Console.WriteLine("Image conversions complete");
        Console.WriteLine("Press Enter to close window");
        Console.ReadLine();
    }
}
```


