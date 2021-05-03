using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab2_3Spo
{
    public class Songs
    {
        public int songsId;
        public string songName;
        public List<Singer> singerSong=new List<Singer>();
        public Album albumSong=new Album();
        public string songType;
        public float songLong;
        public int countListening;
        public string date;

        public Songs(int songsId, string songName, List<Singer> singerSong, Album albumSong, string songType, float songLong, int countListening, string date)
        {
            this.songsId = songsId;
            this.songName = songName;
            this.singerSong = singerSong;
            this.albumSong = albumSong;
            this.songType = songType;
            this.songLong = songLong;
            this.countListening = countListening;
            this.date = date;
        }

        public Songs()
        {
        }

       
    }
}
