using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

	class Pong : State {

		abstract class Player {
			public Vec2 size = new Vec2(5, 30);
			public Vec2 pos = new Vec2(0, 0);

			public Color color = Color.White;

			public double speed = 200;

			double direction = 0, needDirection = 0;

			public void Move(double dir) {
				needDirection = GMath.Clamp(dir, -1, 1);
			}

			public double accel = 10;
			public virtual void Update(double dt) {
				direction += (needDirection - direction) * GMath.Clamp(dt * accel, 0, 1);
				pos.Y += speed * direction * dt;
			}

			public virtual void Render() {
				RenderState.Push();
				RenderState.Color = color;
				Vec2 dv = size;
				dv.Y -= size.X;
				Draw.Rect(pos - dv, pos + dv);
				dv.X = 0;
				Draw.Circle(pos - dv, size.X);
				Draw.Circle(pos + dv, size.X);
				RenderState.Pop();
			}

			public abstract void Think(World world);
		}

		class EmptyPlayer : Player {
			public override void Think(World world) {}
		}

		class AIPlayer : Player {
			public override void Think(World world) {
				if (nextThink > 0) return;
				Ball ball = null;
				if (world.balls.Count > 0)
					ball = world.balls[0];
				if (ball == null){
					Move(0);
					return;
				}
				// ball.pos.x + ball.vel.x * t = pos.x
				if (ball.vel.X == 0) return;
				double tox = pos.X;
				if (Math.Sign(ball.vel.X) != Math.Sign(pos.X - ball.pos.X))
					tox = Math.Sign(ball.pos.X - pos.X) * world.size.X * 3;
				var t = (tox - ball.pos.X) / ball.vel.X;
				var y = ball.pos.Y + ball.vel.Y * t;
				while (Math.Abs(y) > world.size.Y) {
					if (y < -world.size.Y)
						y = -world.size.Y + (-world.size.Y - y);
					else
						y = world.size.Y - (y - world.size.Y);
				}
				this.targetY = y;
				Move((y - pos.Y) / 30);
				nextThink = GRandom.NextDouble(0.2, 0.3);
			}
			double targetY = 0;
			double nextThink = 0;
			public override void Update(double dt) {
				base.Update(dt);
				nextThink -= dt;
			}
			public override void Render() {
				base.Render();
				//draw.save();
				//draw.color(1, 0, 0, 0.2);
				//draw.circle(pos.x, targetY, size.x * 1.5);
				//draw.load();
			}
		}

		class ControlledPlayer : Player {
			Key keyDown, keyUp;
			public ControlledPlayer(Key keyDown, Key keyUp) {
				this.keyDown = keyDown;
				this.keyUp = keyUp;
			}
			public override void Think(World world) {
				double dir = 0;
				if (keyDown.Pressed()) dir -= 1;
				if (keyUp.Pressed()) dir += 1;
				Move(dir);
			}
		}

		class Ball {
			public Vec2 pos = new Vec2(0, 0);
			public double size = 6;

			public Color color = Color.White;

			public Vec2 vel;
			public Ball(double speed = 400) {
				this.setSpeed = speed;
				vel = Vec2.Rotate(new Vec2(speed, 0), GRandom.NextDouble(-Math.PI / 4, Math.PI / 4));
				if (GRandom.Coin()) vel = -vel;
			}

			double setSpeed;

			public void Update(double dt) {
				speed = GMath.Clamp(speed + accel * dt, 0, 1);
				if (Math.Abs(vel.Y) > Math.Abs(vel.X)) {
					vel.X += fixX * Math.Sign(vel.X) * dt;
					vel = vel.Unit * setSpeed;
				}
				pos += vel * speed * dt;
			}
			double fixX = 300;
			double speed = 0;
			double accel = 0.5;

			public void Render() {
				RenderState.Push();
				RenderState.Color = color;
				//draw.rect(pos - vec2(size, size), pos + vec2(size, size));
				Draw.Circle(pos, size);
				RenderState.Pop();
			}
		}

		class World {
			public List<Player> players = new List<Player>();
			public List<Ball> balls = new List<Ball>();

			public Vec2 size = new Vec2(180, 150);
			public double borderSize = 1;
			public Color borderColor = Color.White;

			public int scoreLeft = 0, scoreRight = 0;

			public void Update(double dt) {
				foreach (var player in players) {
					player.Think(this);
					player.Update(dt);
					player.pos.Y = GMath.Clamp(player.pos.Y, -size.Y + player.size.Y, size.Y - player.size.Y);
				}
				foreach (var ball in balls) {
					ball.Update(dt);
					if (Math.Abs(ball.pos.X) > size.X - ball.size) {
						if (ball.pos.X < 0) scoreRight++;
						else scoreLeft++;
						balls = new List<Ball>();
						balls.Add(new Ball());
						break;
						//ball.pos.x = (size.x - ball.size) * ball.pos.x.sign;
						//ball.vel.x = -ball.vel.x;
					}
					if (Math.Abs(ball.pos.Y) > size.Y - ball.size) {
						ball.pos.Y = (size.Y - ball.size) * Math.Sign(ball.pos.Y);
						ball.vel.Y = -ball.vel.Y;
					}
					foreach (var player in players) {
						if (Math.Abs(ball.pos.Y - player.pos.Y) > ball.size + player.size.Y)
							continue;
						if (Math.Abs(ball.pos.X - player.pos.X) < ball.size + player.size.X) {
							ball.pos.X = player.pos.X + (ball.size + player.size.X) * Math.Sign(ball.pos.X - player.pos.X);
							ball.vel.X = -ball.vel.X;
							const double ang = 0.3;
							ball.vel = Vec2.Rotate(ball.vel, GRandom.NextDouble(-ang, ang));
						}
					}
				}
			}
			public void Render() {
				RenderState.Push();
				var col = borderColor;
				col.A *= 0.5;
				RenderState.Color = (col);
//				draw.dashedLine(0, -size.y, 0, size.y, borderSize); TODO
				RenderState.Color = (borderColor);
				Draw.Frame(-size, size, borderSize);
				RenderState.Pop();

				RenderState.Push();
				RenderState.Color = (col);
				RenderState.Translate(-size.Y / 2, 0);
				RenderState.Scale(size.Y / 3);
				Draw.Text(scoreLeft.ToString(), 0.5, 0.5);
				RenderState.Pop();

				RenderState.Push();
				RenderState.Color = (col);
				RenderState.Translate(size.X / 2, 0);
				RenderState.Scale(size.Y / 3);
				Draw.Text(scoreRight.ToString(), 0.5, 0.5);
				RenderState.Pop();

				foreach (var player in players)
					player.Render();
				foreach (var ball in balls)
					ball.Render();
			}
		}

		class Game : State {
			Color backgroundColor = Color.Black;

			World world;
			public Game(bool player1, bool player2) {
				world = new World();
				const double playersPos = 0.9;
				Player leftPlayer;
				if (player1)
					leftPlayer = new ControlledPlayer(Key.S, Key.W);
				else
					leftPlayer = new AIPlayer();
				Player rightPlayer;
				if (player2)
					rightPlayer = new ControlledPlayer(Key.Down, Key.Up);
				else
					rightPlayer = new AIPlayer();
				leftPlayer.pos.X = -world.size.X * playersPos;
				rightPlayer.pos.X = +world.size.X * playersPos;
				world.players.Add(leftPlayer);
				world.players.Add(rightPlayer);
				world.balls.Add(new Ball());
			}
			public override void Update(double dt) {
				const int n = 10;
				for (int i = 0; i < n; i++)
					world.Update(dt / n);
			}
			public override void Render() {
				Draw.Clear(backgroundColor);
				RenderState.Push();
				RenderState.View2d(2 * world.size.Y + 50);
				world.Render();
				RenderState.Pop();
			}
			public override void KeyDown(Key key) { if (key == Key.Escape) Close(); }
		}

		Game back;
		Texture backTex;
		double backT = 0;
		public Pong() {
			//backTex = new Texture(160, 120);
			backTex = new Texture(640, 480);
			backTex.Smooth = true;
			back = new Game(false, false);
		}

		public override void Update(double dt) {
			base.Update(dt);
//			back.Update(dt / 5);
//			backT += dt / 10;
		}

		Color selColor = Color.Red, regColor = Color.White;
		bool player1 = false, player2 = true;
		public override void Render() {
			base.Render();
			Draw.Clear(Color.Black);

			RenderState.BeginTexture(backTex);
			back.Render();
			RenderState.EndTexture();

			RenderState.Push();
			RenderState.View2d(1);
			RenderState.Scale(1 + Math.Cos(backT) / 10);
			RenderState.Rotate(Math.Sin(backT) / 5);
			RenderState.Scale((double)backTex.Width / backTex.Height, 1);
			RenderState.Translate(-0.5, -0.5);
			RenderState.Color = new Color(1, 1, 1, 0.1);
			backTex.Render();
			RenderState.Pop();

			RenderState.Push();
			RenderState.View2d(20);
			RenderState.Translate(0, 3);
			RenderState.Push();
			RenderState.Scale(2);
			RenderState.Color = new Color(0.6, 0.6, 0.6);
			Draw.Text("VPE Pong Example", 0.5);
			RenderState.Pop();
			RenderState.Translate(0, -3);
			RenderState.Color = sel == 0 ? selColor : regColor;
			Draw.Text("Play!", 0.5);
			RenderState.Translate(0, -2);
			RenderState.Color = sel == 1 ? selColor : regColor;
			Draw.Text(string.Format("Left player : {0}", player1 ? "W/S" : "AI"), 0.5);
			RenderState.Translate(0, -1);
			RenderState.Color = sel == 2 ? selColor : regColor;
			Draw.Text(string.Format("Right player : {0}", player2 ? "Up/Down" : "AI"), 0.5);
			RenderState.Translate(0, -2);
			RenderState.Color = sel == 3 ? selColor : regColor;
			Draw.Text("Exit", 0.5);
			RenderState.Pop();
		}
		int sel = 0;
		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (key == Key.Escape)
				Close();
			else if (key == Key.Down)
				sel = (sel + 1) % 4;
			else if (key == Key.Up)
				sel = (sel + 3) % 4;
			else if (key == Key.Enter) {
				if (sel == 3)
					Close();
				else if (sel == 0) {
					PushState(new Game(player1, player2));
				} else if (sel == 1)
					player1 = !player1;
				else if (sel == 2)
					player2 = !player2;
			}
		}

	}

}
