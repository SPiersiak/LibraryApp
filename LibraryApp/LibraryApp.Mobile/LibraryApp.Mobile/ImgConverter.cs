using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace LibraryApp.Mobile
{
    public class ImgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parametr, CultureInfo culture)
        {
            {
                byte[] bytes = value as byte[];
                if (bytes != null && bytes.Length != 0)
                {
                    return BytesToImageSource(bytes);
                }
                return "no_profile";
            }
        }

        private ImageSource BytesToImageSource(byte[] bytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
