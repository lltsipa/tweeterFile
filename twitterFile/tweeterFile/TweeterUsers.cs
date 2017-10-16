using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tweeterFile
{
    public class TweeterUsers
    {
        public string Username { get; set; }
        public List<string> Follows { get; set; }
        public List<string> Followers { get; set; }
        public List<string> UserFeeds { get; set; }
        public override string ToString() {
            string toString = "UserName: " + Username + "Followers: ";

            foreach(var follower in Followers)
            {
                toString = String.Concat(toString, follower);
            }
            return toString.ToString();
        }
    }
}
