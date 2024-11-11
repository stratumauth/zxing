using SkiaSharp;
using Xunit;

namespace Stratum.ZXing.Test
{
    public class ReadTest
    {
        private static ImageView ReadImage(string path)
        {
            var image = SKBitmap.Decode(path);
            var pixels = image.GetPixelSpan();
            return new ImageView(pixels, image.Width, image.Height, ImageFormat.BGRA);
        }
        
        [Theory]
        [InlineData(Binarizer.LocalAverage)]
        [InlineData(Binarizer.GlobalHistogram)]
        [InlineData(Binarizer.FixedThreshold)]
        [InlineData(Binarizer.BoolCast)]
        public void Read_ok(Binarizer binarizer)
        {
            var reader = new QrCodeReader(new ReaderOptions
            {
                Binarizer = binarizer
            });
            
            using var image = ReadImage("data/qr.png");
            var text = reader.Read(image);
            Assert.Equal("otpauth://totp/Standard%3ATotp?secret=54VEGZFJHM3BGDONFERMPUOKNGDJETGM&issuer=Standard", text);
        }
        
        [Fact]
        public void Read_inverted()
        {
            var reader = new QrCodeReader(new ReaderOptions
            {
                TryInvert = true
            });

            using var image = ReadImage("data/qr-inverted.png");
            var code = reader.Read(image);
            Assert.Equal("otpauth://totp/Standard%3ATotp?secret=54VEGZFJHM3BGDONFERMPUOKNGDJETGM&issuer=Standard", code);
        }
        
        [Fact]
        public void Read_rotated()
        {
            var reader = new QrCodeReader(new ReaderOptions
            {
                TryRotate = true
            });

            var image = ReadImage("data/qr-rotated.png");
            var text = reader.Read(image);
            Assert.Equal("otpauth://totp/Standard%3ATotp?secret=54VEGZFJHM3BGDONFERMPUOKNGDJETGM&issuer=Standard", text);
        }
        
        [Fact]
        public void Read_wrongType()
        {
            var reader = new QrCodeReader();
            using var image = ReadImage("data/barcode.png");
            Assert.Null(reader.Read(image));
        }
        
        [Fact]
        public void Read_notFound()
        {
            var reader = new QrCodeReader();
            using var image = ReadImage("data/smile.png");
            Assert.Null(reader.Read(image));
        }
    }
}