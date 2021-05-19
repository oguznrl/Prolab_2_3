using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab2_3Spo
{
    public class Album
    {
        public int albumId;
        public string albumName;
        public string type;
        public string albumDate;
        public List<Songs> albumSongs = new List<Songs>();
        public List<Singer> albumSinger = new List<Singer>();

        public Album()
        {
        }

        public Album(int albumId, string albumName, string type, string albumDate, List<Songs> albumSongs, List<Singer> albumSinger)
        {
            this.albumId = albumId;
            this.albumName = albumName;
            this.type = type;
            this.albumDate = albumDate;
            this.albumSongs = albumSongs;
            this.albumSinger = albumSinger;
        }
    }
}
