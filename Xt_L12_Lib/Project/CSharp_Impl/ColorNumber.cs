﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Xenon.Lib
{

    /// <summary>
    /// 色
    /// </summary>
    public class ColorNumber
    {


        #region 生成と破棄
        //────────────────────────────────────────

        public static ColorNumber FromRgb(int r, int g, int b)
        {
            ColorNumber result = new ColorNumber();
            result.Red = r;
            result.Green = g;
            result.Blue = b;

            return result;
        }

        public static ColorNumber FromColor(Color color)
        {
            ColorNumber result = new ColorNumber();
            result.Red = color.R;
            result.Green = color.G;
            result.Blue = color.B;

            return result;
        }

        public static ColorNumber FromWeb(string s)
        {
            ColorNumber result = new ColorNumber();

            //ystem.Console.WriteLine("ウェブカラー s=[" + s + "] s.Length=[" + s.Length + "]");

            if (!s.StartsWith("#"))
            {

            }
            else if (s.Length == 7)
            {
                string r = s.Substring(1, 2);
                string g = s.Substring(3, 2);
                string b = s.Substring(5, 2);
                result.Red = Convert.ToInt32(r, 16);
                result.Green = Convert.ToInt32(g, 16);
                result.Blue = Convert.ToInt32(b, 16);
            }
            else if (s.Length == 4)
            {
                string[] rgb = new string[]{
                        s.Substring(1, 1),
                        s.Substring(2, 1),
                        s.Substring(3, 1)
                    };

                for (int i = 0; i < 3; i++)
                {
                    string color = rgb[i];
                    System.Console.WriteLine("ウェブカラー[" + i + "] color=[" + color + "]");
                    int n;

                    switch (color.ToUpper())
                    {
                        case "0":
                            n = Convert.ToInt32("00", 16);
                            break;
                        case "1":
                            n = Convert.ToInt32("11", 16);
                            break;
                        case "2":
                            n = Convert.ToInt32("22", 16);
                            break;
                        case "3":
                            n = Convert.ToInt32("33", 16);
                            break;
                        case "4":
                            n = Convert.ToInt32("44", 16);
                            break;
                        case "5":
                            n = Convert.ToInt32("55", 16);
                            break;
                        case "6":
                            n = Convert.ToInt32("66", 16);
                            break;
                        case "7":
                            n = Convert.ToInt32("77", 16);
                            break;
                        case "8":
                            n = Convert.ToInt32("88", 16);
                            break;
                        case "9":
                            n = Convert.ToInt32("99", 16);
                            break;
                        case "A":
                            n = Convert.ToInt32("AA", 16);
                            break;
                        case "B":
                            n = Convert.ToInt32("BB", 16);
                            break;
                        case "C":
                            n = Convert.ToInt32("CC", 16);
                            break;
                        case "D":
                            n = Convert.ToInt32("DD", 16);
                            break;
                        case "E":
                            n = Convert.ToInt32("EE", 16);
                            break;
                        case "F":
                        default:
                            n = Convert.ToInt32("FF", 16);
                            break;
                    }

                    switch (i)
                    {
                        case 0:
                            result.Red = n;
                            break;
                        case 1:
                            result.Green = n;
                            break;
                        default:
                            result.Blue = n;
                            break;
                    }
                }
            }

            return result;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public Color ToColor()
        {
            return Color.FromArgb(this.Red, this.Green, this.Blue);
        }

        /// <summary>
        /// 色のWebカラー表記
        /// </summary>
        public string ToWeb()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("#");
            sb.Append(this.Red.ToString("X2"));
            sb.Append(this.Green.ToString("X2"));
            sb.Append(this.Blue.ToString("X2"));

            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 赤
        /// </summary>
        private int red;
        public int Red
        {
            get
            {
                int n = red;

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                red = value;
            }
        }

        /// <summary>
        /// 緑
        /// </summary>
        private int green;
        public int Green
        {
            get
            {
                int n = green;

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                green = value;
            }
        }

        /// <summary>
        /// 青
        /// </summary>
        private int blue;
        public int Blue
        {
            get
            {
                int n = blue;

                if (n < 0)
                {
                    n = 0;
                }
                else if (255 < n)
                {
                    n = 255;
                }

                return n;
            }
            set
            {
                blue = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }

}
