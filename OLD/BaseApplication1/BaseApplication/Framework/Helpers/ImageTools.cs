using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MYB.BaseApplication.Framework.Helpers
{
   public static class ImageTools
   {
      public enum CroppedFromPosition
      {
         TOP_LEFT,
         TOP_CENTER,
         TOP_RIGHT,
         MIDDLE_LEFT,
         MIDDLE_CENTER,
         MIDDLE_RIGHT,
         BOTTOM_LEFT,
         BOTTOM_CENTER,
         BOTTOM_RIGHT
      }

      #region "Private Methods"

      /// <summary>
      ///   Given a rotate value you will get the Drawing Enum RotateFlipType 0~8
      /// </summary>
      private static RotateFlipType getRotateFlipType(int rotateValue)
      {
         RotateFlipType flipType = RotateFlipType.RotateNoneFlipNone;

         switch (rotateValue)
         {
            case 1:
               flipType = RotateFlipType.RotateNoneFlipNone;
               break;

            case 2:
               flipType = RotateFlipType.RotateNoneFlipX;
               break;

            case 3:
               flipType = RotateFlipType.Rotate180FlipNone;
               break;

            case 4:
               flipType = RotateFlipType.Rotate180FlipX;
               break;

            case 5:
               flipType = RotateFlipType.Rotate90FlipX;
               break;

            case 6:
               flipType = RotateFlipType.Rotate90FlipNone;
               break;

            case 7:
               flipType = RotateFlipType.Rotate270FlipX;
               break;

            case 8:
               flipType = RotateFlipType.Rotate270FlipNone;
               break;

            default:
               flipType = RotateFlipType.RotateNoneFlipNone;
               break;
         }

         return flipType;
      }

      /// <summary>
      ///   Return the encoder info given a mime type.
      /// </summary>
      private static ImageCodecInfo GetEncoder(string mimeType)
      {
         ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

         for (int j = 0; j < encoders.Length; ++j)
         {
            if (encoders[j].MimeType.ToLower() == mimeType.ToLower())
            {
               return encoders[j];
            }
         }

         return null;
      }

      /// <summary>
      ///   Return the encoder info given an image format.
      /// </summary>
      private static ImageCodecInfo GetEncoder(ImageFormat format)
      {
         ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
         foreach (ImageCodecInfo codec in codecs)
         {
            if (codec.FormatID == format.Guid)
            {
               return codec;
            }
         }
         return null;
      }

      #endregion "Private Methods"

      /// <summary>
      ///   Resize a new image type to the specific value.
      /// </summary>
      /// <param name="padImage"></param>
      /// <param name="useMinDimensionAsMinLimiter">True if you want to use the minimun current image size as the limiter for the minimun new image</param>
      /// <returns> A new image resized.</returns>
      public static Image ResizeImage(Image image, int maxWidth, int maxHeight, bool padImage, bool useMinDimensionAsMinLimiter = false)
      {
         int newWidth;
         int newHeight;
         Double sizeFactor;

         //set the resolution, 72 is usually good enough for displaying images on monitors
         float imageResolution = 72;

         //set the compression level. higher compression = better quality = bigger images
         long compressionLevel = 80L;

         Bitmap _image = new Bitmap(image);

         //first we check if the image needs rotating (eg phone held vertical when taking a picture for example)
         foreach (var prop in image.PropertyItems)
         {
            if (prop.Id == 0x0112)
            {
               int orientationValue = image.GetPropertyItem(prop.Id).Value[0];
               RotateFlipType rotateFlipType = getRotateFlipType(orientationValue);
               _image.RotateFlip(rotateFlipType);
               break;
            }
         }

         //apply the padding to make a square image
         if (padImage == true)
         {
            _image = (Bitmap)ApplyPaddingToImage(_image, Color.White);
         }

         //check if the with or height of the image exceeds the maximum specified, if so calculate the new dimensions
         if (_image.Width > maxWidth || _image.Height > maxHeight)
         {
            if (_image.Width / maxWidth >= _image.Height / maxHeight)
            {
               if (!useMinDimensionAsMinLimiter)
               {
                  sizeFactor = Convert.ToSingle(_image.Width) / maxWidth;
                  newHeight = Convert.ToInt32(_image.Height / sizeFactor);
                  newWidth = maxWidth;
               }
               else
               {
                  sizeFactor = Convert.ToSingle(_image.Height) / maxHeight;
                  newWidth = Convert.ToInt32(_image.Width / sizeFactor);
                  newHeight = maxHeight;
               }
            }
            else
            {
               if (!useMinDimensionAsMinLimiter)
               {
                  sizeFactor = Convert.ToSingle(_image.Height) / maxHeight;
                  newWidth = Convert.ToInt32(_image.Width / sizeFactor);
                  newHeight = maxHeight;
               }
               else
               {
                  sizeFactor = Convert.ToSingle(_image.Width) / maxWidth;
                  newHeight = Convert.ToInt32(_image.Height / sizeFactor);
                  newWidth = maxWidth;
               }
            }

         }
         else
         {
            newWidth = _image.Width;
            newHeight = _image.Height;
         }

         //start the resize with a new image
         Bitmap newImage = new Bitmap(newWidth, newHeight, image.PixelFormat);

         //set the new resolution
         newImage.SetResolution(imageResolution, imageResolution);

         //start the resizing
         using (var graphics = Graphics.FromImage(newImage))
         {
            //set some encoding specs
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graphics.DrawImage(_image, 0, 0, newWidth, newHeight);
         }

         if (_image != null)
         {
            _image.Dispose();
         }

         //save the image to a memorystream to apply the compression level
         using (MemoryStream ms = new MemoryStream())
         {
            newImage.Save(ms, image.RawFormat);
         }

         //return the image
         return newImage;
      }

      /// <summary>
      ///   Return a new copy of the image of type bitmap casted as image with the padding applied
      /// </summary>
      public static Image ApplyPaddingToImage(Image image, Color backColor)
      {
         //get the maximum size of the image dimensions
         int maxSize = Math.Max(image.Height, image.Width);
         Size squareSize = new Size(maxSize, maxSize);

         //create a new square image
         Bitmap squareImage = new Bitmap(squareSize.Width, squareSize.Height);

         using (Graphics graphics = Graphics.FromImage(squareImage))
         {
            //fill the new square with a color
            graphics.FillRectangle(new SolidBrush(backColor), 0, 0, squareSize.Width, squareSize.Height);

            //put the original image on top of the new square
            graphics.DrawImage(image, (squareSize.Width / 2) - (image.Width / 2), (squareSize.Height / 2) - (image.Height / 2), image.Width, image.Height);
         }

         //return the image
         return squareImage;
      }

      /// <summary>
      ///   Return a new copy of the image resized as thumbnail
      /// </summary>
      public static Image ThumbnailFromImage(Image img, int width, int height, ImageFormat format, bool padImage)
      {
         return ResizeImage(img, width, height, padImage);
      }

      /// <summary>
      ///   Resize to the shorter size keeping the scale and crop, using the crop form, the rest of the image at the biggest side. 
      ///   i.e. If the image is 1000x100 and the arguments is cropForm = TOP_CENTER, width = 50 and height = 50, it will resize the image to 500x50 and then crop 225 from left and 225 from right leaving a centered image of 50x50.
      /// </summary>
      public static Image CroppedThumbnailFromImage(Image img, int width, int height, ImageFormat format, CroppedFromPosition cropFrom)
      {
         Rectangle r;
         Image _img = ResizeImage(img, width, height, false, true);

         switch (cropFrom)
         {
            case CroppedFromPosition.TOP_LEFT:
               r = new Rectangle(0, 0, width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.TOP_CENTER:
               r = new Rectangle((_img.Width / 2) - (width / 2), 0, width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.TOP_RIGHT:
               r = new Rectangle((_img.Width - width), 0, width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.MIDDLE_LEFT:
               r = new Rectangle(0, (_img.Height / 2) - (height / 2), width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.MIDDLE_CENTER:
               r = new Rectangle((_img.Width / 2) - (width / 2), (_img.Height / 2) - (height / 2), width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.MIDDLE_RIGHT:
               r = new Rectangle((_img.Width - width), (_img.Height / 2) - (height / 2), width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.BOTTOM_LEFT:
               r = new Rectangle(0, (_img.Height - height), width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.BOTTOM_CENTER:
               r = new Rectangle((_img.Width / 2) - (width / 2), (_img.Height - height), width, height);
               _img = CropAtRect(_img, r);
               break;

            case CroppedFromPosition.BOTTOM_RIGHT:
               r = new Rectangle((_img.Width - width), (_img.Height - height), width, height);
               _img = CropAtRect(_img, r);
               break;
         }

         return _img;
      }

      /// <summary>
      ///   Return a new image casted as bitmap and cropped as the rectangle dimension
      /// </summary>
      public static Bitmap CropAtRect(Image img, Rectangle r)
      {
         Bitmap nb = new Bitmap(r.Width, r.Height, img.PixelFormat);
         nb.SetResolution(img.HorizontalResolution, img.VerticalResolution);

         using (Graphics g = Graphics.FromImage(nb))
         {
            g.DrawImage(img, -r.X, -r.Y);
         }
         return nb;
      }

      /// <summary>
      ///   Convert the image to a base64 string
      /// </summary>
      public static string ToImageToBase64(this Image image)
      {
         using (MemoryStream ms = new MemoryStream())
         {
            //convert the image to byte array
            image.Save(ms, ImageFormat.Jpeg);
            byte[] bin = ms.ToArray();

            //convert byte array to base64 string
            return Convert.ToBase64String(bin);
         }
      }

      /// <summary>
      ///   Get the image format. The bitmap will return MemoryBmp.
      /// </summary>
      public static ImageFormat GetImageFormat(this Image img)
      {
         if (img.RawFormat.Equals(ImageFormat.Jpeg))
            return ImageFormat.Jpeg;
         if (img.RawFormat.Equals(ImageFormat.Bmp))
            return ImageFormat.Bmp;
         if (img.RawFormat.Equals(ImageFormat.Png))
            return ImageFormat.Png;
         if (img.RawFormat.Equals(ImageFormat.Emf))
            return ImageFormat.Emf;
         if (img.RawFormat.Equals(ImageFormat.Exif))
            return ImageFormat.Exif;
         if (img.RawFormat.Equals(ImageFormat.Gif))
            return ImageFormat.Gif;
         if (img.RawFormat.Equals(ImageFormat.Icon))
            return ImageFormat.Icon;
         if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
            return ImageFormat.MemoryBmp;
         if (img.RawFormat.Equals(ImageFormat.Tiff))
            return ImageFormat.Tiff;
         else
            return ImageFormat.Wmf;
      }
   }
}