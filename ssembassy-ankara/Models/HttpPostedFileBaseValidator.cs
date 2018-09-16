using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace ssembassy_ankara.Models
{
    public static class HttpPostedFileBaseValidator
    {
        private const int ImageMinimumSize = 5 * 1024 * 1024;
        public static bool IsImage(this HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            try
            {
                //-------------------------------------------
                //  Check the image extension
                //-------------------------------------------
                if (Path.GetExtension(postedFile.FileName)?.ToLower() != ".jpg"
                    && Path.GetExtension(postedFile.FileName)?.ToLower() != ".png"
                    && Path.GetExtension(postedFile.FileName)?.ToLower() != ".jpeg")
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }
            //------------------------------------------
            //check whether the image size exceeding the limit or not
            //------------------------------------------ 
                if (postedFile.ContentLength > ImageMinimumSize)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumSize];
                postedFile.InputStream.Read(buffer, 0, ImageMinimumSize);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                 postedFile.InputStream.Position = 0;
            }

            return true;
        }
        }
}