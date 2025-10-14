using Contemn.UI;

namespace Contemn
{
    public class WebUIContext : UIContext
    {
        public WebUIContext(int columns, int rows, int[] frameBuffer) : base(columns, rows, frameBuffer)
        {
        }

        public override bool Quit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override float SfxVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override float MuxVolume { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
