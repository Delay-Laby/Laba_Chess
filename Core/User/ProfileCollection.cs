using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chess.Core.User
{
    public class ProfileCollection : ICollection<Profile>
    {
        private int currentIndex = -1;
        public Profile Current {
            get {
                if (currentIndex >= 0) return innerCol[currentIndex];
                else return null;
            }
        }
        public IEnumerator<Profile> GetEnumerator()
        {
            return new ProfileEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ProfileEnumerator(this);
        }

        // The inner collection to store objects. 
        private List<Profile> innerCol;

        // For IsReadOnly 
        private bool isRO = false;

        public ProfileCollection()
        {
            innerCol = new List<Profile>();
        }
        public ProfileCollection(String path)
        {
            innerCol = new List<Profile>();
            Load(path);
        }

        // Adds an index to the collection. 
        public Profile this[int index]
        {
            get { return (Profile)innerCol[index]; }
            set { innerCol[index] = value; }
        }

        public bool Contains(Profile item)
        {
            bool found = false;

            foreach (Profile bx in innerCol)
            {
                if (bx.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }

        public bool Contains(Profile item, EqualityComparer<Profile> comp)
        {
            bool found = false;
            foreach (Profile bx in innerCol)
            {
                if (comp.Equals(bx, item))
                {
                    found = true;
                }
            }
            return found;
        }

        public void Add(Profile item)
        {
            innerCol.Add(item);
        }

        public void Clear()
        {
            innerCol.Clear();
        }

        public void CopyTo(Profile[] array, int arrayIndex)
        {
            for (int i = 0; i < innerCol.Count; i++)
            {
                array[i] = (Profile)innerCol[i];
            }
        }

        public int Count {
            get { return innerCol.Count; }
        }

        public bool IsReadOnly {
            get { return isRO; }
        }

        public bool Remove(Profile item)
        {
            return innerCol.Remove(item);
        }

        public void Load(String filename)
        {
            innerCol.Clear();
            if (File.Exists(filename))
            {
                Stream fs = File.OpenRead(filename);
                BinaryFormatter bf = new BinaryFormatter();
                innerCol = (List<Profile>)bf.Deserialize(fs);
                fs.Close();
            }
            currentIndex = -1;
        }

        public void Save(String filename)
        {
            Stream fs = File.Create(filename);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, innerCol);
            fs.Close();
        }
    }


    public class ProfileEnumerator : IEnumerator<Profile>
    {
        private ProfileCollection _collection;
        private int curIndex;
        private Profile curProfile;


        public ProfileEnumerator(ProfileCollection collection)
        {
            _collection = collection;
            curIndex = -1;
            curProfile = default(Profile);

        }

        public bool MoveNext()
        {
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                curProfile = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        void IDisposable.Dispose() { }

        public Profile Current {
            get { return curProfile; }
        }

        object IEnumerator.Current {
            get { return Current; }
        }

    }
}
