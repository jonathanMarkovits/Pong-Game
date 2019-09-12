using System.Drawing;

namespace PongGame
{
    public class BiggerSizeFaller : Faller
    {
        public BiggerSizeFaller(Vector i_position, int i_index) : base(i_position, i_index)
        {
            base.Color = Color.Green;
        }

        public override void MakeEffect(Game i_game)
        {
            i_game.Player.Width = i_game.Player.Width + 20;
        }
    }
}
