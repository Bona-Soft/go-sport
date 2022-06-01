using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MYB.BaseApplication.Infrastructure.FileManager
{
   public class FileManager : IFileManager
   {
      public FileManager()
      {
      }

      #region "Private"

      private Stream ImageToStream(Image img, ImageFormat format)
      {
         using (Stream stream = new MemoryStream())
         {
            img.Save(stream, format);
            stream.Position = 0;
            return stream;
         }
      }

      private string abc = "QWERTYUIOPASDFGHJKLZXCVBNM";

      #endregion "Private"

      #region "Public Methods"
      /// <summary>
      ///   Get a new random normalized file name string using the valid characteres of the originalFileName
      /// </summary>
      /// <param name="originalFileName">The original file name to be used to randomize</param>
      /// <param name="keepExtension">If true it will return the new string with the same extension as the original file name. i.e. "myfile.pdf" => "ymefli.pdf"</param>
      /// <returns></returns>
      public string GetRandomFileName(string originalFileName, bool keepExtension)
      {
         string newFileName = originalFileName.ToNormalized().Replace(".", "").Replace(" ", "").ToRandom();
         if (keepExtension)
         {
            newFileName += Path.GetExtension(originalFileName).ToLower();
         }

         return newFileName;
      }

      /// <summary>
      ///   Get a random normalized string to use as a random file name
      /// </summary>
      public string GetRandomFileName()
      {
         return abc.ToNormalized().ToRandom();
      }

      /// <summary>
      ///   Get a random file name using the extension of the parameter.
      /// </summary>
      /// <param name="fileName">The original file name to extract the extension. i.e. "file.doc", ".pdf" </param>
      /// <returns>A new random string with the same extension that the param fileName</returns>
      public string GetRandomFileName(string fileName)
      {
         return GetRandomFileName() + Path.GetExtension(fileName).ToLower();
      }

      /// <summary>
      ///   Save any kind of Image to disk.
      /// </summary>
      /// <param name="physicalDir">The dir path where save. i.e. "C:\YourDir\"</param>
      /// <param name="stream">The stream file to be saved</param>
      /// <param name="fileName">Name of the file with extension</param>
      /// <param name="overwrite">Overwrite file if exists, default: true</param>
      /// <returns>True if the file exists after save</returns>
      public bool SaveFile(string physicalDir, Image img, string fileName, bool overwrite = true)
      {
         // create folder directory info
         DirectoryInfo folderDir = new DirectoryInfo(physicalDir);

         // check if folder directory not exist
         if (!folderDir.Exists)
         {
            // create directory
            folderDir.Create();
         }

         // define file path
         string _filePath = Path.Combine(physicalDir, fileName);

         // check if file not exist
         if (!File.Exists(_filePath) || overwrite)
         {
            // save file into folder directory
            img.Save(_filePath, img.RawFormat);
         }

         return File.Exists(_filePath);
      }

      /// <summary>
      ///   Save the file to disk using a stream reader (not recommended for binary files)
      /// </summary>
      /// <param name="physicalDir">The dir path where save. i.e. "C:\YourDir\"</param>
      /// <param name="stream">The stream file to be saved</param>
      /// <param name="fileName">Name of the file with extension</param>
      /// <param name="overwrite">Overwrite file if exists, default: true</param>
      /// <returns>True if the file exists after save</returns>
      public bool SaveFile(string physicalDir, FileStream stream, string fileName, bool overwrite = true)
      {
         DirectoryInfo folderDir = new DirectoryInfo(physicalDir);

         // check if folder directory not exist
         if (!folderDir.Exists)
         {
            // create directory
            folderDir.Create();
         }

         // define file path
         string _filePath = Path.Combine(physicalDir, fileName);

         using (Stream fileStream = File.Create(_filePath))
         {
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
         }

         return File.Exists(_filePath);
      }

      /// <summary>
      ///   Save the file to disk using manual stream reader (recommended for binary files)
      /// </summary>
      /// <param name="physicalDir">The dir path where save. i.e. "C:\YourDir\"</param>
      /// <param name="stream">The stream file to be saved</param>
      /// <param name="fileName">Name of the file with extension</param>
      /// <param name="overwrite">Overwrite file if exists, default: true</param>
      /// <returns>True if the file exists after save</returns>
      public bool SaveBinaryFile(string physicalDir, FileStream stream, string fileName, bool overwrite = true)
      {
         DirectoryInfo folderDir = new DirectoryInfo(physicalDir);

         // check if folder directory not exist
         if (!folderDir.Exists)
         {
            // create directory
            folderDir.Create();
         }

         // define file path
         string _filePath = Path.Combine(physicalDir, fileName);

         using (Stream fileStream = File.Create(_filePath))
         {
            CopyStream(stream, fileStream);
         }

         return File.Exists(_filePath);
      }

      /// <summary>
      ///   Copies the contents of input to output. Doesn't close either stream. Avoid use StreamReader (stream.CopyTo) for binary files
      /// </summary>
      public static void CopyStream(Stream input, Stream output)
      {
         byte[] buffer = new byte[8 * 1024];
         int len;
         while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
         {
            output.Write(buffer, 0, len);
         }
      }

      public bool DeleteFile(string path)
      {
         if (File.Exists(path))
         {
            File.Delete(path);
         }

         return !File.Exists(path);
      }

      /*
      public bool UploadFile(string folderName, HttpPostedFile file) => UploadFile(folderName, file, file.FileName);

      public bool UploadFile(string physicalDir, Stream contentStream, string fileName, bool overwrite = true)
      {
         // make folder path
         string _folderPath = "~\\" + folderName;

         // create folder directory info
         DirectoryInfo FolderDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(_folderPath));

         // check if folder directory not exist
         if (!FolderDir.Exists)
         {
            // create directory
            FolderDir.Create();
         }

         // define file path
         string _filePath = Path.Combine(HttpContext.Current.Server.MapPath(_folderPath), fileName);

         // check if file not exist
         if (!File.Exists(_filePath) || overwrite)
         {
            // save file into folder directory
            file.SaveAs(_filePath);
         }

         return File.Exists(_filePath);
      }
      */

      #endregion "Public Methods"
   }
}