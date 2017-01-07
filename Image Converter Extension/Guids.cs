// Guids.cs
// MUST match guids.h
using System;

namespace GuusBeltman.Image_Converter_Extension
{
    static class PkgCmdIDList
    {
        public const uint cmdCreateThumbnail = 0x101;
        public const uint cmdRecompressImg = 0x102;
        public const uint cmdConvertToJpeg = 0x103;
        public const uint cmdRotateLeft = 0x104;
        public const uint cmdRotateRight = 0x105;
        public const uint cmdAdvancedResize = 0x106;
        public const uint cmdConvertToPng = 0x107;
        public const uint cmdConvertToGif = 0x108;
    };

    static class GuidList
    {
        public const string guidImage_Converter_ExtensionPkgString = "0f71e5c4-2790-4a69-90a4-8e0145cadc8c";
        public const string guidImage_Converter_ExtensionCmdSetString = "5cf7eeef-223e-4b30-9104-3a0826718d70";

        //Submenu tasks:
        public const string guidImage_Converter_guidCreateThumbnail = "5cf7eeef-223e-4b30-9104-3a0826718d71";
        public const string guidImage_Converter_guidRecompress = "5cf7eeef-223e-4b30-9104-3a0826718d72";
        public const string guidImage_Converter_guidConvertToJpeg = "5cf7eeef-223e-4b30-9104-3a0826718d73";
        public const string guidImage_Converter_guidRotateLeft = "5cf7eeef-223e-4b30-9104-3a0826718d74";
        public const string guidImage_Converter_guidRotateRight = "5cf7eeef-223e-4b30-9104-3a0826718d75";
        public const string guidImage_Converter_guidAdvancedResize = "5cf7eeef-223e-4b30-9104-3a0826718d76";
        public const string guidImage_Converter_guidConvertToPng = "5cf7eeef-223e-4b30-9104-3a0826718d77";
        public const string guidImage_Converter_guidConvertToGif = "5cf7eeef-223e-4b30-9104-3a0826718d78";

        public static readonly Guid guidImage_Converter_ExtensionCmdSet = new Guid(guidImage_Converter_ExtensionCmdSetString);
        public static readonly Guid guidCreateThumbnail = new Guid(guidImage_Converter_guidCreateThumbnail);
        public static readonly Guid guidRecompress = new Guid(guidImage_Converter_guidRecompress);
        public static readonly Guid guidConvertToJpeg = new Guid(guidImage_Converter_guidConvertToJpeg);
        public static readonly Guid guidRotateLeft = new Guid(guidImage_Converter_guidRotateLeft);
        public static readonly Guid guidRotateRight = new Guid(guidImage_Converter_guidRotateRight);
        public static readonly Guid guidAdvancedResize = new Guid(guidImage_Converter_guidAdvancedResize);
        public static readonly Guid guidConvertToPng = new Guid(guidImage_Converter_guidConvertToPng);
        public static readonly Guid guidConvertToGif = new Guid(guidImage_Converter_guidConvertToGif);
    };
}