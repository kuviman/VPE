using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

    class Chat : UI.State {

        [Serializable]
        class Message : Net.Message {
            public string text;
            public Message(string text) {
                this.text = text;
            }
        }

        class Server : Net.Server<Message> {
            public Server() : base(7878) {
                App.OnUpdate += Update;
            }

            public override void Stop() {
                base.Stop();
                App.OnUpdate -= Update;
            }

            void Update(double dt) {
                Handle();
            }

            protected override IEnumerable<Message> Handle(Message message) {
                Broadcast(message);
                return null;
            }
        }

        class Client : Net.Client<Message> {
            public List<Message> messages = new List<Message>();
            public Client(string ip) : base(ip, 7878) { }
            protected override IEnumerable<Message> Handle(Message message) {
                messages.Add(message);
                return null;
            }
        }

        class ClientState : UI.State {
            Client client;
            UI.Label text = new UI.Label("");
            public ClientState(string ip) {
                if (ip == null) {
                    ip = "127.0.0.1";
                    var server = new Server();
                    OnClose += server.Stop;
                }
                client = new Client(ip);
                OnClose += client.Stop;

                var input = new UI.TextInput(300);
                input.OnKeyDown += (key) => {
                    if (key == Key.Enter || key == Key.KeypadEnter) {
                        Send(input.Value);
                        input.Value = "";
                    }
                };
                input.Anchor = input.Origin = new Vec2(0.5, 0);
                input.Offset = new Vec2(0, 30);
                Frame.Add(input);
                text.Anchor = text.Origin = new Vec2(0.5, 0.5);
                text.TextColor = Color.Black;
                Frame.Add(text);
            }
            void Send(string text) {
                client.Send(new Message(text));
            }
            public override void Render() {
                base.Render();
                string text = "";
                foreach (var msg in client.messages)
                    text += msg.text + "\n";
                this.text.Text = text;
            }
            public override void Update(double dt) {
                base.Update(dt);
                client.Handle();
            }
        }

        public Chat() {
            Zoom = Settings.ZoomUI;
            var list = new UI.ElementList();
            list.Add(new UI.Button("Server", () => {
                PushState(new ClientState(null));
            }));
            var list2 = new UI.ElementList();
            list2.Horizontal = true;
            var ipEntry = new UI.TextInput(150);
            list2.Add(ipEntry);
            list2.Add(new UI.Button("Join", () => {
                PushState(new ClientState(ipEntry.Value));
            }));
            list.Add(list2);
            list.Anchor = list.Origin = new Vec2(0.5, 0.5);
            Frame.Add(list);
        }

    }

}