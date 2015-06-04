using System;
using System.Collections.Generic;
using System.Drawing;
using SFont = System.Drawing.Font;
using log4net;
using System.Runtime.InteropServices;

namespace VitPro.Engine {

    /// <summary>
    /// Font.
    /// </summary>
    public class Font : IFont {

        static ILog log = LogManager.GetLogger(typeof(Font));

        static Font() {
            App.Init();
        }

        System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
        SFont font;
        bool autoAdjustSize;

        private Font() {
            Smooth = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.Font"/> class.
        /// </summary>
        /// <param name="family">Font family.</param>
        /// <param name="emSize">Font size.</param>
        /// <param name="style">Font style.</param>
        public Font(string family, double emSize, Style style = Style.Regular) : this() {
            if (emSize < 0)
                log.Info(string.Format("Loading {0} {1}", family, style));
            else
                log.Info(string.Format("Loading {0} {1} {2}", family, emSize, style));
            autoAdjustSize = emSize < 0;
            if (emSize < 0)
                emSize = 20;
            font = new SFont(family, (float)emSize, (FontStyle)style);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.Font"/> class.
        /// </summary>
        /// <param name="family">Font family.</param>
        /// <param name="style">Font style.</param>
        public Font(string family, Style style = Style.Regular) : this(family, -1, style) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.Font"/> class.
        /// </summary>
        /// <param name="stream">Stream to load font from.</param>
        /// <param name="emSize">Font size.</param>
        /// <param name="style">Font style.</param>
        public Font(System.IO.Stream stream, double emSize, Style style = Style.Regular) : this() {
            autoAdjustSize = emSize < 0;
            byte[] fontBytes = stream.ReadFully();
            var handle = GCHandle.Alloc(fontBytes, GCHandleType.Pinned);
            IntPtr pointer = handle.AddrOfPinnedObject();
            try {
                pfc.AddMemoryFont(pointer, fontBytes.Length);
            } finally {
                handle.Free();
            }
            var family = pfc.Families[0];
            if (emSize < 0)
                emSize = 20;
            font = new SFont(family, (float)emSize, (System.Drawing.FontStyle)style);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.Font"/> class.
        /// </summary>
        /// <param name="stream">Stream to load font from.</param>
        /// <param name="style">Font style.</param>
        public Font(System.IO.Stream stream, Style style = Style.Regular) : this(stream, -1, style) { }

        /// <summary>
        /// Measure the specified text.
        /// </summary>
        /// <param name="text">Text.</param>
        public double Measure(string text) {
            if (text.Length == 0)
                return 0;
            var size = helpGfx.MeasureString(text, font).ToSize();
            return (double)size.Width / size.Height;
        }

        /// <summary>
        /// Render the specified text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void Render(string text) {
            if (text.Length == 0)
                return;
            var texture = GetCachedTexture(text);
            RenderState.Push();
            RenderState.Scale(Measure(text), 1);
            texture.Render();
            RenderState.Pop();
            //texture.Delete();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VitPro.Engine.Font"/> is smooth.
        /// </summary>
        /// <value><c>true</c> if smooth; otherwise, <c>false</c>.</value>
        public bool Smooth { get; set; }

        /// <summary>
        /// Font style.
        /// </summary>
        [Flags]
        public enum Style {
            Bold = FontStyle.Bold,
            Italic = FontStyle.Italic,
            Regular = FontStyle.Regular,
            Strikeout = FontStyle.Strikeout,
            Underline = FontStyle.Underline,
        }

        static Graphics helpGfx = Graphics.FromImage(new Bitmap(1, 1));

        /// <summary>
        /// Make the texture from the text.
        /// </summary>
        /// <returns>The texture.</returns>
        /// <param name="text">Text.</param>
        public Texture MakeTexture(string text) {
            return InternalMakeTexture(text);
        }

        Texture GetCachedTexture(string text) {
            if (text.Length == 0)
                return null;
            int fontSize = (int)(autoAdjustSize ? RenderState.FontSize : this.font.Size);
            Texture cachedTexture;
            if (cache.TryGetValue(Tuple.Create(fontSize, text), out cachedTexture))
                return cachedTexture;
            return cache[Tuple.Create(fontSize, text)] = InternalMakeTexture(text);
        }

        internal virtual Texture InternalMakeTexture(string text) {
            SFont font = this.font;
            if (autoAdjustSize)
                font = new SFont(this.font.FontFamily, (float)RenderState.FontSize, this.font.Style);
            var size = helpGfx.MeasureString(text, font).ToSize();
            var bitmap = new Bitmap(size.Width, size.Height);
            var gfx = Graphics.FromImage(bitmap);
            gfx.Clear(System.Drawing.Color.FromArgb(0, 0xff, 0xff, 0xff));
            gfx.TextRenderingHint = Smooth ?
                System.Drawing.Text.TextRenderingHint.AntiAlias :
                            System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            gfx.DrawString(text, font, Brushes.White, 0, 0);
            var texture = new Texture(1, 1);
            texture.Smooth = Smooth;
            texture.SetBitmap(bitmap);
            return texture;
        }

        Dictionary<Tuple<int, string>, Texture> cache = new Dictionary<Tuple<int,string>,Texture>();

    }

}
