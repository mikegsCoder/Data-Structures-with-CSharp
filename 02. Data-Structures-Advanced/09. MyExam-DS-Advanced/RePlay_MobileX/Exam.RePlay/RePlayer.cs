using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.RePlay
{
    public class RePlayer : IRePlayer
    {
        private SortedDictionary<string, HashSet<Track>> byAlbumName;
        private Dictionary<string, Track> byId;
        private Queue<Track> queue;

        public RePlayer()
        {
            this.byAlbumName = new SortedDictionary<string, HashSet<Track>>();
            this.byId = new Dictionary<string, Track>();
            this.queue = new Queue<Track>();
        }

        public int Count => this.byId.Count;

        public void AddToQueue(string trackName, string albumName)
        {
            var track = this.GetTrack(trackName, albumName);

            this.queue.Enqueue(track);
        }

        public void AddTrack(Track track, string album)
        {
            if (this.Contains(track))
            {
                return;
            }

            this.byId.Add(track.Id, track);

            if (!this.byAlbumName.ContainsKey(album))
            {
                this.byAlbumName.Add(album, new HashSet<Track>());
            }

            this.byAlbumName[album].Add(track);
        }

        public bool Contains(Track track)
        {
            return this.byId.ContainsKey(track.Id);
        }

        public IEnumerable<Track> GetAlbum(string albumName)
        {
            if (!this.byAlbumName.ContainsKey(albumName) || this.byAlbumName[albumName].Count == 0)
            {
                throw new ArgumentException();
            }

            return this.byAlbumName[albumName].OrderByDescending(x => x.Plays);
        }

        public Dictionary<string, List<Track>> GetDiscography(string artistName)
        {
            Dictionary<string, List<Track>> result = new Dictionary<string, List<Track>>();

            foreach (var album in this.byAlbumName.Keys)
            {
                if (this.byAlbumName[album].Any(x => x.Artist == artistName))
                {
                    result.Add(album, new List<Track>(this.byAlbumName[album].Where(x => x.Artist == artistName)));
                }
            }

            if (result.Count == 0)
            {
                throw new ArgumentException();
            }

            return result;
        }

        public Track GetTrack(string title, string albumName)
        {
            if (!this.byAlbumName.ContainsKey(albumName) || this.byAlbumName[albumName].Count == 0)
            {
                throw new ArgumentException();
            }

            var track = this.byAlbumName[albumName].FirstOrDefault(x => x.Title == title);

            if (track == null)
            {
                throw new ArgumentException();
            }

            return track;
        }

        public IEnumerable<Track> GetTracksInDurationRangeOrderedByDurationThenByPlaysDescending(int lowerBound, int upperBound)
        {
            return this.byId.Values
                .Where(x => x.DurationInSeconds >= lowerBound && x.DurationInSeconds <= upperBound)
                .OrderBy(x => x.DurationInSeconds)
                .ThenByDescending(x => x.Plays);
        }

        public IEnumerable<Track> GetTracksOrderedByAlbumNameThenByPlaysDescendingThenByDurationDescending()
        {
            var result = new List<Track>();

            foreach (var album in this.byAlbumName.Keys)
            {
                byAlbumName[album].OrderByDescending(x => x.Plays).ThenByDescending(x => x.DurationInSeconds).ToList().ForEach(x => { result.Add(x); });
            }

            return result;
        }

        public Track Play()
        {
            if (this.queue.Count == 0)
            {
                throw new ArgumentException();
            }

            var track = this.queue.Dequeue();

            track.Plays++;

            return track;
        }

        public void RemoveTrack(string trackTitle, string albumName)
        {
            var toRemove = this.GetTrack(trackTitle, albumName);

            this.byId.Remove(toRemove.Id);
            this.byAlbumName[albumName].Remove(toRemove);

            Queue<Track> temp = new Queue<Track>();

            this.queue.Where(x => x.Id != toRemove.Id).ToList().ForEach(x => { temp.Enqueue(x); });

            this.queue = temp;
        }
    }
}
