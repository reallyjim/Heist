using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Heist
{
    // borrowed from https://dejanstojanovic.net/aspnet/2014/november/adding-extra-info-to-an-image-file/  to try to add additional properties
    public enum MetaProperty
    {
        Title = 40091,
        Comment = 40092,
        Author = 40093,
        Keywords = 40094,
        Subject = 40095,
        Copyright = 33432,
        Software = 11,
        DateTime = 36867
    }
    public static class Extensions
    {
        public static Image SetMetaValue(this Image sourceBitmap, MetaProperty property, string value)
        {
            var prop = sourceBitmap.PropertyItems[0];
            int iLen = value.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)value[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = (int)property;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
            sourceBitmap.SetPropertyItem(prop);
            return sourceBitmap;
        }

        public static void AddMetaProperty(this Image image, MetaProperty property, string value)
        {
            var prop = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
            prop.Id = (int)property;
            prop.Type = 2;
            prop.Value = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            prop.Len = prop.Value.Length;
            image.SetPropertyItem(prop);
        }


        public static string GetMetaValue(this Image sourceBitmap, MetaProperty property)
        {
            var propItems = sourceBitmap.PropertyItems;
            var prop = propItems.FirstOrDefault(p => p.Id == (int)property);
            if (prop != null)
            {
                return Encoding.UTF8.GetString(prop.Value);
            }
            else
            {
                return null;
            }
        }

    }
}
