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
            Console.WriteLine("Use SHIFT + INSERT to paste path and press ENTER.\n");

            // Gets the directory where all the photos to convert are located.
            string photos = Console.ReadLine();
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
    }
}
