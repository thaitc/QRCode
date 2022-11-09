using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace QR.Helper
{
    public static class Common
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
