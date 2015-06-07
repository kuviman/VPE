using System;

namespace VitPro.Engine.Examples {

	class Test : State {

		Shader shader = new Shader(Resource.String("test.glsl"));
		Texture name = new Texture(Resource.Stream("test.png"));
		Texture temp, temp2;

		public override void Render() {
			base.Render();
			Draw.Clear(Settings.BackgroundColor);
			if (temp == null)
				temp = new Texture(name.Width, name.Height);
			if (temp2 == null)
				temp2 = new Texture(name.Width, name.Height);
			temp2.Smooth = true;
			
			RenderState.BeginTexture(temp);
			Draw.Clear(0, 0, 0, 0);
			RenderState.Translate(-1, -1);
			RenderState.Scale(2);
			RenderState.Set("texture", name);
			RenderState.Set("size", new Vec2(name.Width, name.Height));
			RenderState.Set("doX", 1);
			shader.RenderQuad();
			RenderState.EndTexture();

			RenderState.BeginTexture(temp2);
			Draw.Clear(0, 0, 0, 0);
			RenderState.Translate(-1, -1);
			RenderState.Scale(2);
			RenderState.Set("texture", temp);
			RenderState.Set("size", new Vec2(name.Width, name.Height));
			RenderState.Set("doX", 0);
			shader.RenderQuad();
			RenderState.EndTexture();

			RenderState.Push();
			RenderState.View2d(5);

			RenderState.Scale((double)name.Width / name.Height, 1);
			RenderState.Scale(2);
			RenderState.Origin(0.5, 0.5);

			RenderState.Color = Color.Black;
			Draw.Quad();

			RenderState.Color = Color.White;
			temp2.Render();

			RenderState.Pop();
		}
		
	}

}
