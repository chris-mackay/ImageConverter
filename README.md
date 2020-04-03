# ImageConverter
Console application to batch converter image file formats using [ImageMagick](https://github.com/dlemstra/Magick.NET)

## Console output
![Console](image_converter.png?raw=true "Console")

## Using the code
```csharp
static void Main(string[] args)
{
    Console.WriteLine("Copy and Paste the photos directory below. Use SHIFT + INSERT to paste path and Press ENTER.\n");
    string photos = Console.ReadLine(); // Gets the directory where all the photos to convert are located.
    DirectoryInfo dInfo = new DirectoryInfo(photos);
    
    // Counter the tell the user the current file that is being processed.
    int counter = 1;

    // Gets a total count of files that will be processed.
    int count = Directory.GetFiles(dInfo.ToString()).Count(); 
    
    // Add a blank line for clarity.
    Console.WriteLine(); 

    foreach (string f in Directory.GetFiles(dInfo.ToString()))
    {
        // Get the old file that we want to convert.
        FileInfo oldFile = new FileInfo(f); 
        string name = oldFile.Name;

        // Create a new MagickImage object so we can set the new format.
        MagickImage image = new MagickImage(f); 
        
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
```


