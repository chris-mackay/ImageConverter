using System;
using System.IO;
using System.Linq;
using ImageMagick;

namespace ImageConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Copy and Paste the photos directory below. Use SHIFT + INSERT to paste path and Press ENTER.\n");
            string photos = Console.ReadLine(); // Gets the directory where all the photos to convert are located.
            DirectoryInfo dInfo = new DirectoryInfo(photos);

            int counter = 1; // Counter the tell the user the current file that is being processed.
            int count = Directory.GetFiles(dInfo.ToString()).Count(); // Gets a total count of files that will be processed.

            Console.WriteLine(); // Add a blank line for clarity.

            foreach (string f in Directory.GetFiles(dInfo.ToString()))
            {
                FileInfo oldFile = new FileInfo(f); // Get the old file that we want to convert.
                string name = oldFile.Name;
                
                MagickImage image = new MagickImage(f); // Create a new MagickImage object so we can set the new format.
                image.Format = MagickFormat.Jpeg; // Set the desired image format to convert to.
                
                byte[] data = image.ToByteArray(); // Create an array of bytes. We write these bytes to a file below.

                name = name.Replace("HEIC", ""); // Remove the old file extension.
                string file = dInfo + "\\" + name + "jpg"; // Set the new file name.

                FileInfo newFile = new FileInfo(file);
                FileStream fs = new FileStream(newFile.FullName, FileMode.Create, FileAccess.Write); // Create a file stream so we can write the bytes to our new file.
                
                foreach (byte item in data) fs.WriteByte(item); // Write all the bytes to the file.

                Console.WriteLine(newFile.Name + " processed (" + counter + "/" + count + ")"); // Display the current file that was converted.
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
}
