using System.Drawing;

namespace aaa
{
    public class SmallerSizeFaller : Faller
    {
        public SmallerSizeFaller(Vector i_position, int i_index) : base(i_position, i_index)
        {
            base.Color = Color.Red;
        }

        public override void MakeEffect(Game i_game)
        {
            i_game.Player.Width = i_game.Player.Width - 20;
        }
    }
}
