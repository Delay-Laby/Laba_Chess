using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Chess.Core.User
{
    [Serializable()]
    public class Profile : IEquatable<Profile>
    {
        private String _nickname;
        private String _email;
        private UInt64 _score;
        private UInt64 _win_count;
        private UInt64 _loss_count;
        private UInt64 _playtime;

        public string NickName { get { return _nickname; } }
        public string eMail { get { return _email; } }
        public UInt64 Score { get { return _score; } }
        public UInt64 WinCount { get { return _win_count; } }
        public UInt64 LossCount { get { return _loss_count; } }
        public DateTime PlayTime { get { return new DateTime((long)_playtime*(long)Math.Pow(10, 7)); } }

        public Profile( string NickName, string eMail )
        {
            _nickname = NickName;
            _email = eMail;
            _playtime = 0;
            _score = 0;
            _win_count = 0;
            _loss_count = 0;
        }

        public void IncrementScore()
        {
            _score++;
        }

        public void GameWon(uint time)
        {
            _playtime += time;
            _win_count++;
        }

        public void GameLost(uint time)
        {
            _playtime += time;
            _loss_count++;
        }

        public bool Equals(Profile other)
        {
            return this.ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            return _nickname + " [" + _email + ']';
        }
    }
}
