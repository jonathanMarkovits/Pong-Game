using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aaa
{
    public interface Entity
    {
        void Update(Game i_game);
        void Display(Graphics i_g);
        void CollisionDetection(Game i_game);
    }
}
