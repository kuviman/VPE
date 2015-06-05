using System;

namespace VitPro.Engine {

    public class OutlinedFont : Font {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.OutlinedFont"/> class.
        /// </summary>
        /// <param name="family">Font family.</param>
        /// <param name="emSize">Font size.</param>
        /// <param name="style">Font style.</param>
        public OutlinedFont(string family, double emSize, Style style = Style.Regular) : base(family, emSize, style) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.OutlinedFont"/> class.
        /// </summary>
        /// <param name="family">Font family.</param>
        /// <param name="style">Font style.</param>
        public OutlinedFont(string family, Style style = Style.Regular) : base(family, style) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.OutlinedFont"/> class.
        /// </summary>
        /// <param name="stream">Stream to load font from.</param>
        /// <param name="emSize">Font size.</param>
        /// <param name="style">Font style.</param>
        public OutlinedFont(System.IO.Stream stream, double emSize, Style style = Style.Regular) : base(stream, emSize, style) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="VitPro.Engine.OutlinedFont"/> class.
        /// </summary>
        /// <param name="stream">Stream to load font from.</param>
        /// <param name="style">Font style.</param>
        public OutlinedFont(System.IO.Stream stream, Style style = Style.Regular) : base(stream, style) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.OutlinedFont"/> class.
		/// </summary>
		/// <param name="font">Font.</param>
		public OutlinedFont(Font font) : base(font) { }

        Color _outlineColor = Color.Black;

        /// <summary>
        /// Gets or sets font outline color.
        /// </summary>
        public Color OutlineColor { get { return _outlineColor; } set { _outlineColor = value; } }

		Color _color = Color.White;

		/// <summary>
		/// Gets or sets the font color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color { get { return _color; } set { _color = value; } }

        double _outlineWidth = 0.05;

        /// <summary>
        /// Gets or sets font outline width;
        /// </summary>
        public double OutlineWidth { get { return _outlineWidth; } set { _outlineWidth = value; } }

        static Shader shader = new Shader(Resource.String("OutlineFont.glsl"));

        internal override Texture InternalMakeTexture(string text) {
            Texture tex = base.InternalMakeTexture(text);
			int border = GMath.Ceil(tex.Height * OutlineWidth);
            Texture result = new Texture(tex.Width + 2 * border, tex.Height + 2 * border);
            RenderState.BeginTexture(result);
            Draw.Clear(1, 1, 1, 0);
            RenderState.Set("texture", tex);
            RenderState.Set("textureSize", new Vec2(tex.Width, tex.Height));
            RenderState.Set("outlineWidth", OutlineWidth);
            RenderState.Set("outlineColor", OutlineColor);
			RenderState.Set("borderSize", border);
			RenderState.Set("resultSize", result.Size);
			RenderState.Color = Color;
			RenderState.View2d(0, 1, 0, 1);
            shader.RenderQuad();
            RenderState.EndTexture();
            result.Smooth = Smooth;
            return result;
        }

    }

}