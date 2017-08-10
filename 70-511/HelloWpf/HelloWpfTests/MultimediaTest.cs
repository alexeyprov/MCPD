using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWpfTests
{
    [TestClass]
    public class MultimediaTest
    {
        [TestMethod]
        public void TestMediaPlayer()
        {
            MediaPlayer player = new MediaPlayer();

            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            player.Open(new Uri(Path.Combine(rootPath, @"Data\Long.mp3")));
            player.Open(new Uri(Path.Combine(rootPath, @"Data\Short.m4a")));

            player.Play();

            Thread.Sleep(30000);
        }
    }
}
