using Exam.RePlay;
using NUnit.Framework;
using System;
using System.Collections.Generic;

public class RePlayerTests
{
    private IRePlayer rePlayer;

    private Track GetRandomTrack()
    {
        Random random = new Random();

        return new Track(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            random.Next(1, 1_000_000_000),
            random.Next(10, 1000));
    }

    [SetUp]
    public void Setup()
    {
        this.rePlayer = new RePlayer();
    }

    [Test]
    [Category("Correctness")]
    public void TestAddTrack_WithExistentAlbum_ShouldSuccessfullyAddTrack()
    {
        this.rePlayer.AddTrack(this.GetRandomTrack(), "randomAlbum");
        this.rePlayer.AddTrack(this.GetRandomTrack(), "randomAlbum");

        Assert.AreEqual(2, this.rePlayer.Count);
    }

    [Test]
    [Category("Correctness")]
    public void TestAddTrack_WithNonexistentAlbum_ShouldSuccessfullyAddTrack()
    {
        this.rePlayer.AddTrack(this.GetRandomTrack(), "randomAlbum");

        Assert.AreEqual(1, this.rePlayer.Count);
    }

    [Test]
    [Category("Correctness")]
    public void TestContains_WithExistentTrack_ShouldReturnTrue()
    {
        Track randomTrack = this.GetRandomTrack();

        this.rePlayer.AddTrack(randomTrack, "randomAlbum");

        Assert.IsTrue(this.rePlayer.Contains(randomTrack));
    }

    [Test]
    [Category("Correctness")]
    public void TestGetTracksOrderedByMultiCriteria_WithCorrectData_ShouldReturnCorrectResults()
    {
        Track track = new Track("asd", "bsd", "csd", 4000, 400);
        Track track2 = new Track("dsd", "esd", "fsd", 5000, 400);
        Track track3 = new Track("hsd", "isd", "jsd", 5000, 500);
        Track track4 = new Track("ksd", "lsd", "msd", 5000, 600);
        Track track5 = new Track("nsd", "osd", "psd", 6000, 100);

        this.rePlayer.AddTrack(track, "randomAlbum");
        this.rePlayer.AddTrack(track2, "bandomAlbum");
        this.rePlayer.AddTrack(track3, "aandomAlbum2");
        this.rePlayer.AddTrack(track4, "aandomAlbum2");
        this.rePlayer.AddTrack(track5, "aandomAlbum2");

        List<Track> list = new List<Track>(this.rePlayer.GetTracksOrderedByAlbumNameThenByPlaysDescendingThenByDurationDescending());

        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(track5, list[0]);
        Assert.AreEqual(track4, list[1]);
        Assert.AreEqual(track3, list[2]);
        Assert.AreEqual(track2, list[3]);
        Assert.AreEqual(track, list[4]);
    }
}