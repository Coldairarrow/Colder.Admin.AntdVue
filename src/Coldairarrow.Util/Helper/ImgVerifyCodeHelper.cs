using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Coldairarrow.Util
{
    public static class ImgVerifyCodeHelper
    {
        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <param name="length">验证码字符数</param>
        /// <returns>图片byte[]和code</returns>
        public static (byte[] imgBytes, string code) BuildVerifyCode(int length)
        {
            VerifyCodeFactory vc = new VerifyCodeFactory();
            string code = vc.CreateValidateCode(length);
            byte[] bytes = vc.CreateValidateGraphic(code);

            return (bytes, code);
        }

        public class VerifyCodeFactory
        {
            /// <summary>  
            /// 验证码的字符集，去掉了一些容易混淆的字符  
            /// </summary>  
            private char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

            /// <summary>
            /// 
            /// </summary>
            /// <param name="codeType">验证码类型(0-字母数字混合,1-数字,2-字母)</param>
            /// <param name="codeCount">验证码字符个数</param>
            /// <returns></returns>
            public string CreateValidateCode(int codeType, int codeCount)
            {
                char code;
                string checkCode = String.Empty;
                System.Random random = new Random();

                for (int i = 0; i < codeCount; i++)
                {
                    code = character[random.Next(character.Length)];

                    // 要求全为数字或字母  
                    if (codeType == 1)
                    {
                        if ((int)code < 48 || (int)code > 57)
                        {
                            i--;
                            continue;
                        }
                    }
                    else if (codeType == 2)
                    {
                        if ((int)code < 65 || (int)code > 90)
                        {
                            i--;
                            continue;
                        }
                    }
                    checkCode += code;
                }

                return checkCode;
            }

            /// <summary>     
            /// 生成验证码     
            /// </summary>     
            /// <param name="length">指定验证码的长度</param>     
            /// <returns></returns>     
            public string CreateValidateCode(int length)
            {
                int[] randMembers = new int[length];
                int[] validateNums = new int[length];
                string validateNumberStr = "";
                //生成起始序列值     
                int seekSeek = unchecked((int)DateTime.Now.Ticks);
                Random seekRand = new Random(seekSeek);
                int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
                int[] seeks = new int[length];
                for (int i = 0; i < length; i++)
                {
                    beginSeek += 10000;
                    seeks[i] = beginSeek;
                }
                //生成随机数字     
                for (int i = 0; i < length; i++)
                {
                    Random rand = new Random(seeks[i]);
                    int pownum = 1 * (int)Math.Pow(10, length);
                    randMembers[i] = rand.Next(pownum, Int32.MaxValue);
                }
                //抽取随机数字     
                for (int i = 0; i < length; i++)
                {
                    string numStr = randMembers[i].ToString();
                    int numLength = numStr.Length;
                    Random rand = new Random();
                    int numPosition = rand.Next(0, numLength - 1);
                    validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
                }
                //生成验证码     
                for (int i = 0; i < length; i++)
                {
                    validateNumberStr += validateNums[i].ToString();
                }
                return validateNumberStr;
            }

            public byte[] CreateValidateGraphic(string validateCode)
            {
                return CreateValidateGraphic(validateCode, 12, 22);
            }

            /// <summary>     
            /// 创建验证码的图片     
            /// </summary>        
            /// <param name="validateCode">验证码</param>  
            /// <param name="fontsize"></param>
            /// <param name="height"></param>
            public byte[] CreateValidateGraphic(string validateCode, float fontsize, int height)
            {
                Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * (fontsize + 1)), height);
                Graphics g = Graphics.FromImage(image);
                try
                {
                    //生成随机生成器     
                    Random random = new Random();
                    //清空图片背景色     
                    g.Clear(Color.White);
                    //画图片的干扰线     
                    for (int i = 0; i < 25; i++)
                    {
                        int x1 = random.Next(image.Width);
                        int x2 = random.Next(image.Width);
                        int y1 = random.Next(image.Height);
                        int y2 = random.Next(image.Height);
                        g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                    }
                    //画图片验证码
                    Rectangle layoutRectange = new Rectangle(0, 0, image.Width, image.Height);
                    Font font = new Font("Arial", fontsize, (FontStyle.Bold | FontStyle.Italic));
                    LinearGradientBrush brush = new LinearGradientBrush(layoutRectange, Color.Blue, Color.DarkRed, 1.2f, true);
                    StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(validateCode, font, brush, layoutRectange, format);

                    //画图片的前景干扰点     
                    for (int i = 0; i < 100; i++)
                    {
                        int x = random.Next(image.Width);
                        int y = random.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(random.Next()));
                    }
                    //画图片的边框线     
                    g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                    //保存图片数据     
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Jpeg);
                    //输出图片流     
                    return stream.ToArray();
                }
                finally
                {
                    g.Dispose();
                    image.Dispose();
                }
            }

            /// <summary>     
            /// 得到验证码图片的长度     
            /// </summary>     
            /// <param name="validateNumLength">验证码的长度</param>     
            /// <returns></returns>     
            public static int GetImageWidth(int validateNumLength)
            {
                return (int)(validateNumLength * 12.0);
            }

            /// <summary>     
            /// 得到验证码的高度     
            /// </summary>     
            /// <returns></returns>     
            public static double GetImageHeight()
            {
                return 22.5;
            }
        }
    }
}
