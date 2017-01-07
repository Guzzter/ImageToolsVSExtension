using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuusBeltman.Image_Converter_Extension.Forms;
using GuusBeltman.Image_Converter_Extension.Resize;

namespace GuusBeltman.Image_Converter_Extension.Helpers
{
    public class FileUtil
    {
        /*
        public enum FileCreateAction
        {
            CopyAsThumbnail,
            BackupOriginal
        }*/
        public static string GenerateNewFileName(string origFileName, string suffix)
        {
            return origFileName.Substring(0, origFileName.LastIndexOf(".")) + suffix + Path.GetExtension(origFileName);
        }

        public static string CreateActionMessage(string filename, ProcessingCommand cmd)
        {
            string msg;
            switch (cmd.ActionToPerform)
            {
                case ManipulateAction.Rotate:
                    msg = "is rotated";
                    break;
                case ManipulateAction.CopyAsThumbnail:
                    msg = string.Format("is created as thumbnail image (width {0}px and height {1}px).", cmd.Width, cmd.Height);
                    break;
                case ManipulateAction.ReCompressWithQuality:
                    msg = string.Format("is recompressed with quality level of {0}%.", cmd.QualityLevel);
                    break;
                case ManipulateAction.AdvancedConvert:
                    msg = string.Format("{0} is converted (width {1}px and height {2}px).", filename, cmd.Width, cmd.Height);
                    break;
                case ManipulateAction.ConvertToJpeg:
                    msg = string.Format("{0} is created as new jpeg image.", filename);
                    break;
                case ManipulateAction.ConvertToPng:
                    msg = string.Format("{0} is created as new png image.", filename);
                    break;
                case ManipulateAction.ConvertToGif:
                    msg = string.Format("{0} is created as new gif image.", filename);
                    break;
                default:
                    msg = "Error: unknown action";
                    break;
            }
            return msg;
        }

        public static string ConvertSize(double bytes)
        {
            const int CONVERSION_VALUE = 1024;
            if (bytes < CONVERSION_VALUE)
                return Math.Round(ConvertSize(bytes, "BY"),2) + " bytes";
            else if (bytes < Math.Pow(CONVERSION_VALUE, 2))
                return Math.Round(ConvertSize(bytes, "KB"),2) + " kb";
            else if (bytes < Math.Pow(CONVERSION_VALUE, 3))
                return Math.Round(ConvertSize(bytes, "MB"),2) + " mb";
            else
                return Math.Round(ConvertSize(bytes, "GB"), 2) + " gb";
        }

        /// <summary>
        /// Function to convert the given bytes to either Kilobyte, Megabyte, or Gigabyte
        /// </summary>
        /// <param name="bytes">Double -> Total bytes to be converted</param>
        /// <param name="type">String -> Type of conversion to perform</param>
        /// <returns>Int32 -> Converted bytes</returns>
        /// <remarks></remarks>
        public static double ConvertSize(double bytes, string type)
        {
            try
            {
                const int CONVERSION_VALUE = 1024;
                //determine what conversion they want
                switch (type.ToUpperInvariant())
                {
                    case "BY":
                        //convert to bytes (default)
                        return bytes;
                    case "KB":
                        //convert to kilobytes
                        return (bytes / CONVERSION_VALUE);
                    case "MB":
                        //convert to megabytes
                        return (bytes / Math.Pow(CONVERSION_VALUE, 2));
                    case "GB":
                        //convert to gigabytes
                        return (bytes / Math.Pow(CONVERSION_VALUE, 3));
                    default:
                        //default
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
