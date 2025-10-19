using Contemn.Business;
using Contemn.UI;

namespace Contemn
{
    public class WebUIContext(int columns, int rows, int[] frameBuffer) : UIContext(columns, rows, frameBuffer)
    {
        public override bool Quit { get => false; set { } }
        public override float SfxVolume { get => 1.0f; set { } }
        public override float MuxVolume { get => 1.0f; set { } }
        public override bool HasSettings => false;
        public override int Zoom { get => 1; set { } }

        public override int ScreenWidth => ViewWidth;

        public override int ScreenHeight => ViewHeight;

        public override int ViewWidth => 320;

        public override int ViewHeight => 200;

        public override bool FullScreen { get => false; set { } }

        public override IEnumerable<string> Commands => [];

        public override IEnumerable<string> UnboundKeys => [];

        public override IKeyBindings KeyBindings => throw new NotImplementedException();

        public override void Bind(string command, string identifier)
        {
            //nothing
        }

        public override IEnumerable<string> BoundKeys(string command)
        {
            return [];
        }

        public override void Unbind(string key)
        {
            //nothing!
        }
    }
}
